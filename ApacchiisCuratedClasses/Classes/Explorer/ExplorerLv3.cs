using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Explorer
{
	public class ExplorerLv3 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Explorer Lv.3");
            Tooltip.SetDefault("+3.6% All Damage\n" +
                               "+3% Movement Speed\n" +
                               "+4.5% Mining Speed\n" +
                               "+3 Defense\n" +
                               "(By: Apacchii)");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0;
			item.rare = 3;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv3>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<ExplorerLv2>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasExplorer = true;

            player.allDamage += .036f;
            player.moveSpeed += .03f;
            player.pickSpeed += .045f;
            player.statDefense += 3;
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

