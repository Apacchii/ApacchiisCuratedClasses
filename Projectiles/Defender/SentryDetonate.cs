using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Projectiles.Defender
{
	public class SentryDetonate : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.SentryShot[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 384;
			projectile.height = 384;
            projectile.alpha = 255;
			projectile.friendly = true;
			projectile.hide = true;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.tileCollide = false;
			projectile.timeLeft = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 2;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.realLife >= 0 && target.type != NPCID.WallofFlesh && target.type != NPCID.WallofFleshEye)
				damage /= 5;
			else if (target.type == NPCID.EaterofWorldsHead || target.type == NPCID.EaterofWorldsBody || target.type == NPCID.EaterofWorldsTail)
				damage /= 5;

			if (target.position.X < projectile.position.X + projectile.width * 5)
				hitDirection = -1;
			else
				hitDirection = 1;
			base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
		}

		//Thanks to Verveine for this method
		public static void resetIFrames(Projectile projectile)
		{
			for (int l = 0; l < Main.npc.Length; l++)
			{  
				NPC target = Main.npc[l];
				if (projectile.Hitbox.Intersects(target.Hitbox)) 
				{
					target.immune[projectile.owner] = 2;
				}
			}
		}

		public override void AI()
		{
			if (projectile.ai[0] == 0)
			{
				resetIFrames(projectile);
				Main.PlaySound(SoundID.Item15, projectile.position);
				//Smoke Dust spawn
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				//Fire Dust spawn
				for (int i = 0; i < 40; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 3f;
				}
				//Large Smoke Gore spawn
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				projectile.ai[0] = 1;
			}
		}
	}
}
