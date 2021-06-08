using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Defender
{
	public class DefenderLv5 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Defender Lv.5 [Path 1]");
            Tooltip.SetDefault("+15% Sentry Damage\n" +
                               "+3 Max Sentries\n" +
                               "+25% Sentry Range\n" +
                               "+12.5% Sentry Speed\n" +
                               "[Detonate Sentries] Base Damage: 1000\n" +
                               "[c/af7A6:[Path 1][c/af7A6:]]\n" +
                               "+1 Max Sentries\n" +
                               "[Detonate Sentries] Deals 20% more damage\n" +
                               "[Detonate Sentries] Base cooldown reduced from 15 to 10 seconds\n" +
                               "[Sentry Defense] Extra passive defense after using [Detonate Sentries] increased from 2 to 3\n" +
                               "(By: TheLoneGamer)");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0;
			item.rare = 5;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv5>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<DefenderLv4>());
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasDefender = true;

            accPlayer.hasClassPath1 = true;

            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryDamage += 0.15f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryRange += 0.25f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentrySpeed += 0.125f;
            player.maxTurrets += 4;
            accPlayer.defenderAbility1Damage = 1000;
			player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().abilityDamage += 0.2f;
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
