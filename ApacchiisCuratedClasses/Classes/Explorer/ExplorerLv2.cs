using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Explorer
{
	public class ExplorerLv2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Explorer Lv.2");
            Tooltip.SetDefault("+2.4% All Damage\n" +
                               "+2% Movement Speed\n" +
                               "+3% Mining Speed\n" +
                               "+2 Defense\n" +
                               "(By: Apacchii)");
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
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv2>(), 1); // The official mod's Class Token Lv.X
            recipe.AddIngredient(ModContent.GetInstance<ExplorerLv1>()); // And the previous level class
            recipe.SetResult(this); // Makes the Lv.2 class
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasExplorer = true;

            player.allDamage += .024f;
            player.moveSpeed += .02f;
            player.pickSpeed += .03f;
            player.statDefense += 2;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass == true)
                return false;

            return base.CanEquipAccessory(player, slot);
        }

        public override bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount)
        {
            reforgePrice = 150000; // 15/3 = 5 Gold
            return base.ReforgePrice(ref reforgePrice, ref canApplyDiscount);
        }
    }
}

