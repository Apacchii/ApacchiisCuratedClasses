using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Explorer
{
	public class ExplorerLv8Path2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override string Texture => "ApacchiisCuratedClasses/Classes/Explorer/ExplorerLv8";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Explorer Lv.8");
            Tooltip.SetDefault("+9.6% All Damage\n" +
                               "+8% Movement Speed\n" +
                               "+12% Mining Speed\n" +
                               "+8 Defense\n" +
                               "[c/af7A6:[Path 2][c/af7A6:]]\n" +
                               "Movement speed increased by an additional 4%\n" +
                               "Defense increased by an additional 5\n" +
                               "Ability 2 Base cooldown reduced from 18 to 16 seconds\n" +
                               "(By: Apacchii)");
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
            recipe.AddIngredient(ModContent.GetInstance<ExplorerLv7Path2>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasExplorer = true;
            accPlayer.hasClassPath2 = true;

            player.allDamage += .096f;
            player.moveSpeed += .12f;
            player.pickSpeed += .12f;
            player.statDefense += 13;
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

