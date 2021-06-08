using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Defender
{
	public class DefenderLv2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Defender Lv.2");
            Tooltip.SetDefault("+6% Sentry Damage\n" +
                               "+1 Max Sentries\n" +
                               "+10% Sentry Range\n" +
                               "+5% Sentry Speed\n" +
                               "[Detonate Sentries] Base Damage: 150\n" +
                               "(By: TheLoneGamer)");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0;
			item.rare = 2;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv2>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<DefenderLv1>());
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasDefender = true;

            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryDamage += 0.06f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryRange += 0.1f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentrySpeed += 0.05f;
            player.maxTurrets++;
            accPlayer.defenderAbility1Damage = 150;
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

