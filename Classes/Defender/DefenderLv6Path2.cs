using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Defender
{
	public class DefenderLv6Path2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override string Texture => "ApacchiisCuratedClasses/Classes/Defender/DefenderLv6";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Defender Lv.6");
            Tooltip.SetDefault("+18% Sentry Damage\n" +
                               "+3 Max Sentries\n" +
                               "+30% Sentry Range\n" +
                               "+15% Sentry Speed\n" +
                               "[Detonate Sentries] Base Damage: 1500\n" +
                               "[c/af7A6:[Path 2][c/af7A6:]]\n" +
                               "[Powered Sentries] Base duration increased from 10 to 15 seconds\n" +
                               "[Powered Sentries] Base cooldown reduced from 60 to 50 seconds\n" +
                               "+5% Sentry Crit Chance\n" +
                               "(By: TheLoneGamer)");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0;
			item.rare = 6;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv6>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<DefenderLv5Path2>());
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasDefender = true;
            accPlayer.hasClassPath2 = true;

            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryDamage += 0.18f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryRange += 0.30f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentrySpeed += 0.15f;
            player.maxTurrets += 3;
            accPlayer.defenderAbility1Damage = 1500;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryCrit += 5;
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

