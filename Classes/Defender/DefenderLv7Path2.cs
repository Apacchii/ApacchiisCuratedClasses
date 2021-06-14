using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Defender
{
	public class DefenderLv7Path2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];
        Mod ExpSentriesMod = ModLoader.GetMod("ExpandedSentries");

        public override string Texture => "ApacchiisCuratedClasses/Classes/Defender/DefenderLv7";

        public override void SetStaticDefaults()
        {
            if (ExpSentriesMod != null)
            {
                DisplayName.SetDefault("Class: Defender Lv.7");
                Tooltip.SetDefault("+21% Sentry Damage\n" +
                                   "+3 Max Sentries\n" +
                                   "+35% Sentry Range\n" +
                                   "+17.5% Sentry Speed\n" +
                                   "[Detonate Sentries] Base damage: 3000\n" +
                                   "[c/af7A6:[Path 2][c/af7A6:]]\n" +
                                   "[Powered Sentries] Base duration increased from 10 to 15 seconds\n" +
                                   "[Powered Sentries] Base cooldown reduced from 60 to 50 seconds\n" +
                                   "+5% Sentry Crit Chance\n" +
                                   "(By: TheLoneGamer)");
            }
            else
            {
                DisplayName.SetDefault("Class: Defender Lv.1");
                Tooltip.SetDefault("[c/d33838:This class required the mod 'Expanded Sentries' to work]\n" +
                                   "[c/d33838:You can download it through the Mod Browser]");
            }
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0;
			item.rare = 7;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv7>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<DefenderLv6Path2>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            if (ExpSentriesMod != null)
            {
                player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
                accPlayer.hasDefender = true;
                accPlayer.hasClassPath2 = true;

                //Reflection for cross-mod compatability without hard references
                ModPlayer esPlayer = player.GetModPlayer(ExpSentriesMod, "ESPlayer");
                Type esPlayerType = esPlayer.GetType();

                // Sentry Damage
                FieldInfo sentryDamage = esPlayerType.GetField("sentryDamage", BindingFlags.Instance | BindingFlags.Public);
                float oldSentryDamage = (float)sentryDamage.GetValue(esPlayer);
                sentryDamage.SetValue(esPlayer, oldSentryDamage + .21f);

                // Sentry Range
                FieldInfo sentryRange = esPlayerType.GetField("sentryRange", BindingFlags.Instance | BindingFlags.Public);
                float oldSentryRange = (float)sentryRange.GetValue(esPlayer);
                sentryRange.SetValue(esPlayer, oldSentryRange + .35f);

                // Sentry Speed
                FieldInfo sentrySpeed = esPlayerType.GetField("sentrySpeed", BindingFlags.Instance | BindingFlags.Public);
                float oldSentrySpeed = (float)sentrySpeed.GetValue(esPlayer);
                sentrySpeed.SetValue(esPlayer, oldSentrySpeed + .175f);

                // Sentry Crit
                FieldInfo sentryCrit = esPlayerType.GetField("sentryCrit", BindingFlags.Instance | BindingFlags.Public);
                int oldSentryCrit = (int)sentryCrit.GetValue(esPlayer);
                sentryCrit.SetValue(esPlayer, oldSentryCrit + 5);

                player.maxTurrets += 3;
                accPlayer.defenderAbility1Damage = 3000;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass == true)
                return false;

            return base.CanEquipAccessory(player, slot);
        }

        public override bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount)
        {
            reforgePrice = 150000;
            return base.ReforgePrice(ref reforgePrice, ref canApplyDiscount);
        }
    }
}

