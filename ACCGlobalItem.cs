using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses
{
    public class ACCGlobalitem : GlobalItem
	{
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var accPlayer = player.GetModPlayer<ACCPlayer>();

            if (accPlayer.hasSpellblade)
            {
                if (accPlayer.spellbladeToggle && item.melee)
                {
                    Vector2 vel = Main.MouseWorld - player.position;
                    vel.Normalize();
                    vel *= 15;

                    var p = Projectile.NewProjectile(player.Center.X, player.position.Y, vel.X, vel.Y, ModContent.ProjectileType<Projectiles.Spellblade.Blade>(), accPlayer.magicBladeBaseDamage, 1, player.whoAmI);
                }
            }
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool UseItem(Item item, Player player)
        {
            var accPlayer = player.GetModPlayer<ACCPlayer>();

            if (accPlayer.hasSpellblade && player.itemAnimation == player.HeldItem.useAnimation)
            {
                if (accPlayer.spellbladeToggle && item.melee && item.axe <= 0 && item.pick <= 0 && item.hammer <= 0)
                {
                    Vector2 vel = Main.MouseWorld - player.position;
                    vel.Normalize();
                    vel *= 15;

                    var p = Projectile.NewProjectile(player.Center.X, player.position.Y, vel.X, vel.Y, ModContent.ProjectileType<Projectiles.Spellblade.Blade>(), accPlayer.magicBladeBaseDamage, 1, player.whoAmI);
                }
            }
            return base.UseItem(item, player);
        }
    }
}