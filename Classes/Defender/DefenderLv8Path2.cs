using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Defender
{
	public class DefenderLv8Path2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];
        Mod es = ModLoader.GetMod("ExpandedSentries");

        public override string Texture => "ApacchiisCuratedClasses/Classes/Defender/DefenderLv8";

        public override void SetStaticDefaults()
        {
            if (es != null)
            {
                DisplayName.SetDefault("Class: Defender Lv.8");
                Tooltip.SetDefault("+24% Sentry Damage\n" +
                                   "+3 Max Sentries\n" +
                                   "+40% Sentry Range\n" +
                                   "+20% Sentry Speed\n" +
                                   "[Detonate Sentries] Base Damage: 5000\n" +
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
			item.rare = 8;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv8>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<DefenderLv7Path2>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            if (es != null)
            {
                player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
                accPlayer.hasDefender = true;
                accPlayer.hasClassPath2 = true;

                player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryDamage += 0.24f;
                player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryRange += 0.4f;
                player.GetModPlayer<ExpandedSentries.ESPlayer>().sentrySpeed += 0.2f;
                player.maxTurrets += 3;
                accPlayer.defenderAbility1Damage = 5000;
                player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryCrit += 5;
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

