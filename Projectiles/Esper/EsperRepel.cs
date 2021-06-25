using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Projectiles.Esper
{
	public class EsperRepel : ModProjectile
	{
		SoundEffectInstance loopSound;
		SoundEffectInstance repelSound;
		public override void SetDefaults()
		{
			projectile.width = 256;
			projectile.height = 256;
            projectile.alpha = 255;
			projectile.friendly = true;
			projectile.hide = true;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.tileCollide = false;
		}

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override bool? CanHitNPC(NPC npc)
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			if (loopSound != null)
			{
				loopSound.Stop(true);
			}
			Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Esper/EsperRepelEnd"));
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
            var acmPlayer = player.GetModPlayer<ApacchiisClassesMod.MyPlayer>();
            var accPlayer = player.GetModPlayer<ACCPlayer>();
			if (!player.active || player.dead || projectile.hostile)
			{
				projectile.Kill();
				return;
			}
			if ((loopSound == null || loopSound.State != SoundState.Playing) && projectile.ai[0] >= 15)
			{
				loopSound = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Esper/EsperRepelLoop"));
			}
			if (projectile.ai[0] <= 0)
				projectile.ai[0] = 1;
			projectile.velocity = Vector2.Zero;
			float fieldSize = (projectile.ai[0] / 15) * 256 * acmPlayer.abilityDamage;
			projectile.width = (int)fieldSize;
			projectile.height = (int)fieldSize;
			projectile.Center = player.Center;
			for (int i = 0; i < 60; i++)
			{
				Vector2 dustPos = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 60 * i)) * ((projectile.width + projectile.height) / 4);
				int dustIndex = Dust.NewDust(dustPos, 1, 1, 86, 0, 0, 150, default(Color), 0.7f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity = Vector2.Zero;
			}
			for (int l = 0; l < Main.npc.Length; l++)
			{
				NPC target = Main.npc[l];
				float distanceCheck = (fieldSize / 2);
				if (Vector2.Distance(target.Center, projectile.Center) <= distanceCheck)
				{
					if (repelSound == null || repelSound.State != SoundState.Playing)
						repelSound = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Esper/EsperRepelHit"));
					if (target.lifeMax == 1)
					{
						target.life = 0;
					}
					if (target.knockBackResist > 0f && !target.townNPC)
					{
						if (target.Center.X < projectile.Center.X && target.velocity.X > -projectile.knockBack)
						{
							target.velocity.X = -projectile.knockBack * target.knockBackResist;
						}
						else if (target.velocity.X < projectile.knockBack)
						{
							target.velocity.X = projectile.knockBack * target.knockBackResist;
						}
						if (target.Center.Y < projectile.Center.Y && target.velocity.Y > -projectile.knockBack)
						{
							target.velocity.Y = -projectile.knockBack * target.knockBackResist;
						}
						else if (target.velocity.Y < projectile.knockBack)
						{
							target.velocity.Y = projectile.knockBack * target.knockBackResist;
						}
					}
				}
			}
			if (accPlayer.hasClassPath1)
			{
				int damageBlock;
				if (!Main.expertMode)
					damageBlock = 30 + (int)(player.statDefense * 0.5f);
				else
					damageBlock = 60 + (int)(player.statDefense * 0.75f);
				for (int j = 0; j < Main.projectile.Length; j++)
				{
					Projectile proj = Main.projectile[j];
					float distanceCheck2 = (fieldSize / 2);
					if (Vector2.Distance(proj.Center, projectile.Center) <= distanceCheck2)
					{
						if (proj.hostile && !proj.friendly && proj.damage > 0 && proj.damage <= damageBlock)
						{
							if (repelSound == null || repelSound.State != SoundState.Playing)
								repelSound = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Esper/EsperRepelHit"));
							proj.Kill();
						}
					}
				}
			}
			if (projectile.ai[0] < 15)
			{
				projectile.ai[0]++;
				if (projectile.ai[0] == 15)
					projectile.knockBack *= 0.25f;
			}
		}
	}
}
