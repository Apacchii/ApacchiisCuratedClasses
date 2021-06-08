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

namespace ApacchiisCuratedClasses.Projectiles.Explorer
{
    public class ExplorerTeleporter : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];
        bool flag = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explorer's Teleporter");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 60 * 10;
            projectile.light = 0.3f;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            if (projectile.owner == Main.myPlayer)
            {
                // I make projectiles scale with Ability Duration by increasing its base timeLeft by abilityDuration only when the projectile is spawned
                if (!flag) // So, if flag is false
                {
                    projectile.timeLeft = (int)(60 * 10 * player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().abilityDuration); // Scale the projectile's timeLeft with the player's abilityDuration
                    flag = true; // Set flag to true so it does not repeat this process ever again until the projectile dies
                }

                // In here we are updating the Vector2 variable that the player uses for teleporting
                player.GetModPlayer<ACCPlayer>().explorerTeleporterPos = projectile.position;

                // Kill the projectile if the player has re-casted the ability
                if (player.GetModPlayer<ACCPlayer>().explorerThrownTeleporter == false)
                    projectile.Kill();
            }
        }

        // If the projectile collides with terrain, come to a full stop
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = new Vector2(0, 0);
            return false; // Returning false wont kill the projectile when it collides with terrain, allowing it to stay still where it hit until its timeLeft reaches 0 and it dies
        }

        // Kill the projectile if the player has re-casted the ability
        public override void Kill(int timeLeft)
        {
            player.GetModPlayer<ACCPlayer>().explorerThrownTeleporter = false;
            base.Kill(timeLeft);
        }

        // Trail effect
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

        public override bool? CanHitNPC(NPC target)
        {
            return false; // Returning false so the projectile cannot hit NPCs
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.CadetBlue;
        }
    }
}
