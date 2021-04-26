using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using ApacchiisClassesMod;
using Microsoft.Xna.Framework;

namespace ApacchiisCuratedClasses
{
	public class ACCPlayer : ModPlayer
	{
        // Main mod ability variables
        public int baseCooldown;
        public float cooldownReduction;
        public float abilityPower;
        public float abilityDuration;

        // Main mod Misc. Variables
        public bool hasEquipedClass;
        public bool hasClassPath1;
        public bool hasClassPath2;

        //Explorer
        public bool hasExplorer;
        int explorerDodgeTimer = 0;
        bool explorerDodgeHeal = false;
        int explorerPassiveTimer = 180;
        public bool explorerThrownTeleporter = false;
        public Vector2 explorerTeleporterPos; // This variable is the one updated by the "ExplorerTeleporter" projectile

        public override void ResetEffects()
        {
            hasClassPath1 = false;
            hasClassPath2 = false;

            // Resetting class variables to their default so when the player unnequips the class the class' variables are reset
            hasExplorer = false;
            
            base.ResetEffects();
        }

        // Grabbing the main mod's values of these variables
        public override void PreUpdate()
        {
            baseCooldown = player.GetModPlayer<MyPlayer>().baseCooldown;
            cooldownReduction = player.GetModPlayer<MyPlayer>().cooldownReduction;

            abilityPower = player.GetModPlayer<MyPlayer>().abilityDamage;
            abilityDuration = player.GetModPlayer<MyPlayer>().abilityDuration;

            base.PreUpdate();
        }

        public override void PreUpdateBuffs()
        {
            #region Explorer Ability 1 Timers & Effects
            // Here we increase or decrease timers
            if (explorerDodgeTimer > 0) // So if the Dodge Timer is above 0
            {
                explorerDodgeTimer--; // Decrease it 1 by 1 each tick
                 
                player.invis = true;
                player.velocity = new Vector2(0, 0);
                player.moveSpeed = 0f;
                player.slowFall = true;
            }

            if (explorerPassiveTimer < 180) // If the passive timer is below 180 (3 seconds)
                explorerPassiveTimer++; // Increase it 1 by 1 each tick
            #endregion
            base.PreUpdateBuffs();
        }

        public override void PostUpdateBuffs()
        {
            #region Explorer Purge
            if (explorerDodgeTimer > 0) // If the dodge timer is above 0
            {
                // Making the player immune to the debuffs will clear them, and wont allow them to be re-applied for as long as we are dodging
                player.buffImmune[BuffID.Bleeding] = true;
                player.buffImmune[BuffID.Poisoned] = true;
                player.buffImmune[BuffID.OnFire] = true;
                player.buffImmune[BuffID.Venom] = true;
                player.buffImmune[BuffID.Darkness] = true;
                player.buffImmune[BuffID.Blackout] = true;
                player.buffImmune[BuffID.Silenced] = true;
                player.buffImmune[BuffID.Cursed] = true;
                player.buffImmune[BuffID.Confused] = true;
                player.buffImmune[BuffID.Silenced] = true;
                player.buffImmune[BuffID.Slow] = true;
                player.buffImmune[BuffID.OgreSpit] = true;
                player.buffImmune[BuffID.Weak] = true;
                player.buffImmune[BuffID.BrokenArmor] = true;
                player.buffImmune[BuffID.WitheredArmor] = true;
                player.buffImmune[BuffID.WitheredWeapon] = true;
                player.buffImmune[BuffID.CursedInferno] = true;
                player.buffImmune[BuffID.Ichor] = true;
                player.buffImmune[BuffID.Frostburn] = true;
                player.buffImmune[BuffID.Chilled] = true;
                player.buffImmune[BuffID.Frozen] = true;
                player.buffImmune[BuffID.Webbed] = true;
                player.buffImmune[BuffID.Stoned] = true;
                player.buffImmune[BuffID.VortexDebuff] = true;
                player.buffImmune[BuffID.Electrified] = true;
            }
            #endregion
            base.PostUpdateBuffs();
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            #region Explorer Dodge Heal
            if (explorerDodgeTimer > 0 && !explorerDodgeHeal) // If the timer is above 0, and we have not yet healed from dodging
            {
                damage = 0; // Set the damage taken to 0 (this will actually be 1, since damage taken cannot be below 1)
                player.statLife += (int)(player.statLifeMax2 * .06f * abilityPower +1 ); // Heal for 4% of max health * ability power, +1 for the damage we took when dodging
                player.HealEffect((int)(player.statLifeMax2 * .06f * abilityPower + 1)); // Display the Heal Effect for the same value (green numbers above the player's head when healed)
                explorerDodgeHeal = true; // We have now healed from dodging, so this can no longer happen until we re-use the ability
            }
            #endregion
            base.ModifyHitByNPC(npc, ref damage, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            #region Explorer Dodge Heal
            if (explorerDodgeTimer > 0 && !explorerDodgeHeal) // If the timer is above 0, and we have not yet healed from dodging
            {
                damage = 0; // Set the damage taken to 0 (this will actually be 1, since damage taken cannot be below 1)
                player.statLife += (int)(player.statLifeMax2 * .04f * abilityPower + 1); // Heal for 4% of max health * ability power, +1 for the damage we took when dodging
                player.HealEffect((int)(player.statLifeMax2 * .04f * abilityPower + 1)); // Display the Heal Effect for the same value (green numbers above the player's head when healed)
                explorerDodgeHeal = true; // We have now healed from dodging, so this can no longer happen until we re-use the ability
            }
            #endregion
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        // Modify what happens right before we hit an NPC
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            #region Explorer Passive
            if (hasExplorer) // If out currently equipped class is Explorer
            {
                if(explorerPassiveTimer == 180) // If the timer is at 180 ticks (3 seconds)
                {
                    explorerPassiveTimer = 0; // Reset the timer back to 0
                    damage = (int)(damage * 1.15f); // Multiply the damage by 15%
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), new Color(255, 255, 255), "!", true); // Small visual feedback that the passive was used
                }                                                                                                                                                             // Displays a white "!" on top of the player
            }
            #endregion
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        // Modify what happens right before we hit an NPC with a Projectile
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            #region Explorer Passive
            if (hasExplorer) // If out currently equipped class is Explorer
            {
                if (explorerPassiveTimer == 180) // If the timer is at 180 ticks (3 seconds)
                {
                    explorerPassiveTimer = 0; // Reset the timer back to 0
                    damage = (int)(damage * 1.15f); // Multiply the damage by 15%
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 25, player.width, player.height), new Color(255, 255, 255), "!", true); // Small visual feedback that the passive was used
                }                                                                                                                                                             // Displays a white "!" on top of the player
            }
            #endregion
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            #region Ability 1
            // If the main mod's ability 1 cooldown debuff is NOT currently active, run all the code below this line
            if (ACM.ClassAbility1.JustPressed && player.FindBuffIndex(ModContent.BuffType<ApacchiisClassesMod.Buffs.ActiveCooldown1>()) == -1)
            {
                #region Explorer
                if (hasExplorer) // If out currently equipped class is Explorer
                {
                    if (!explorerThrownTeleporter) // And we haven't thrown out our teleporter
                    {
                        // Grab mouse position compared to the player's position and normalize velocity
                        Vector2 vel = Main.MouseWorld - player.position;
                        vel.Normalize();
                        vel *= 2; // The lower this value, the lower the speed at which the projectile moves

                        // Spawn the teleporter projectile towards mouse position                   V      V                                                            V
                        var tp = Projectile.NewProjectile(player.Center.X, player.position.Y - 10, vel.X, vel.Y, ModContent.ProjectileType<Projectiles.Explorer.ExplorerTeleporter>(), 0, 0, player.whoAmI);
                        explorerThrownTeleporter = true; // We have now thrown our teleporter
                    }
                    else // If we have already thrown out our teleporter
                    {
                        if(hasClassPath1) // And we have the class' Path 1
                            player.AddBuff(ModContent.BuffType<ApacchiisClassesMod.Buffs.ActiveCooldown1>(), (int)(baseCooldown * cooldownReduction * 35)); // Add a 35 second cooldown
                        else                                                                                                                                //       Otherwise
                            player.AddBuff(ModContent.BuffType<ApacchiisClassesMod.Buffs.ActiveCooldown1>(), (int)(baseCooldown * cooldownReduction * 42)); // Add a 42 second cooldown

                        player.Teleport(explorerTeleporterPos); // Set the player's position to a Vector2 variable thats updated every tick by the "ExplorerTeleporter" projectile.
                        explorerThrownTeleporter = false; // Resetting this variable will allow the player to throw out the teleporter again and re-use the ability

                        // See "ExplorerTeleporter.cs" for more
                    }
                }
                #endregion
            }
            #endregion

            #region Ability 2
            // If the main mod's ability 2 cooldown debuff is NOT currently active, run all the code below this line
            if (ACM.ClassAbility2.JustPressed && player.FindBuffIndex(ModContent.BuffType<ApacchiisClassesMod.Buffs.ActiveCooldown2>()) == -1)
            {
                #region Explorer
                if (hasExplorer) // If out currently equipped class is Explorer
                {
                    // If the player has the class' second path, apply a 16s cooldown, otherwise, apply a 18s cooldown.
                    // baseCooldown is 60 (1 second) by default, but can be changed through the main mod's config to decrease the overall cooldown abilities have
                    // cooldownReduction is how much % of, well, cooldown reduction the players has, reducing the ability's cooldown
                    if (hasClassPath2) 
                        player.AddBuff(ModContent.BuffType<ApacchiisClassesMod.Buffs.ActiveCooldown2>(), (int)(baseCooldown * cooldownReduction * 16)); // <-- 16 is the cooldown in seconds
                    else
                        player.AddBuff(ModContent.BuffType<ApacchiisClassesMod.Buffs.ActiveCooldown2>(), (int)(baseCooldown * cooldownReduction * 18)); // <-- 18 is the cooldown in seconds

                    explorerDodgeTimer = (int)(20 * abilityDuration); // Set the timer to 20 ticks, multiplied by abilityDuration so it scales with it.
                    explorerDodgeHeal = false; // Setting a variable to false will allow us to mamke the player to heal only once when hit by an NPC/Projectile

                    Main.PlaySound(SoundID.MenuClose); // Play a sound
                }
                #endregion
            }
            #endregion
            base.ProcessTriggers(triggersSet);
        }
    }
}