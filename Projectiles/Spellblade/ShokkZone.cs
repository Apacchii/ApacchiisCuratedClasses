using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.IO;

namespace ApacchiisCuratedClasses.Projectiles.Spellblade
{
    public class ShokkZone : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];
        int timer;
        int timerBase;
        bool init = false;
        bool init2 = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shokk Zone");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 320;
            projectile.height = 320;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 60 * 6;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            // Initializing the projectile's stats
            if (!init)
            {
                timerBase = player.GetModPlayer<ACCPlayer>().shokkZoneTimerBase;
                timer = 0;
                projectile.timeLeft = (int)(60 * 6 * player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().abilityDuration);
                init = true;
            }

            // Changing the alpha for a fade-in effect
            if (!init2)
            {
                projectile.alpha -= 2;
                if (projectile.alpha <= 100)
                {
                    projectile.alpha = 100;
                    init2 = true;
                }
            }

            // Timer for hitting enemies
            timer--;

            if(timer <= 0) // If the timer is 0 or lower than 0
                timer = timerBase; // Set the timer to its base value in ticks

            // Animate and add light to the projectile
            if (timer == timerBase)
            {
                projectile.rotation += MathHelper.ToDegrees(Main.rand.NextFloat(45f, 90f)); // Rotate by some random value for more visual variation
                Main.PlaySound(SoundID.Item100);
            }
                
            if (timer >= (int)(timerBase * .8f))
            {
                projectile.frame = 1;
                projectile.light = 1f;
            }
            else
            {
                projectile.frame = 0;
                projectile.light = 0f;
            }
            
            base.AI();
        }

        // Syncing the timers in multiplayer to avoid de-syncing (Sending the info)
        public override void SendExtraAI(BinaryWriter writer)
        { 
            writer.Write(timer);
            writer.Write(timerBase);
            base.SendExtraAI(writer);
        }

        // Syncing the timers in multiplayer to avoid de-syncing (Receiving the info)
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            timer = reader.ReadInt32();
            timerBase = reader.ReadInt32();
        }

        // When can this projectile hit enemies ?
        public override bool? CanHitNPC(NPC target)
        {
            if (timer == (int)(timerBase * .8f) && !target.townNPC)
                return true;
            else
                return false;
        }

        public override bool? CanCutTiles() => false; // The projectile cannot break tiles like Vines or Pots
    }
}
