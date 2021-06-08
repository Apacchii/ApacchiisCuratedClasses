using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework.Graphics;

namespace ApacchiisCuratedClasses.Projectiles.Spellblade
{
    public class Blade : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];
        bool init = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 60 * 3;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            aiType = ProjectileID.WoodenArrowFriendly;

            drawOffsetX = -28;
        }

        public override void AI()
        {
            var accPlayer = player.GetModPlayer<ACCPlayer>();

            if (!init)
            {
                if (accPlayer.hasClassPath2)
                    projectile.penetrate = 4;
                else
                    projectile.penetrate = 2;

                init = true;
            }
                
            // Adding some Dust effects and Light
            Lighting.AddLight(projectile.Center, .2f, .05f, .3f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;

            var dust = Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), DustID.BlueCrystalShard, new Vector2(projectile.velocity.X * .1f, projectile.velocity.Y * .1f), 0, Color.MediumPurple, 2f);
            dust.noGravity = true;
            dust.alpha = 255;
            dust.alpha--;
            base.AI();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
