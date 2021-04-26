using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Explorer
{
	public class ExplorerUltimatepath2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override string Texture => "ApacchiisCuratedClasses/Classes/Explorer/ExplorerUltimate";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Explorer Ultimate [Path 2]"); // <-- Level 5 and Ultimate tokens say which path the token is, other levels do not
            Tooltip.SetDefault("+12% All Damage\n" +
                               "+10% Movement Speed\n" +
                               "+15% Mining Speed\n" +
                               "+10 Defense\n" +
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
			item.rare = 9;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenUltimate>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<ExplorerLv8Path2>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasExplorer = true;
            accPlayer.hasClassPath2 = true;

            player.allDamage += .12f;
            player.moveSpeed += .14f;
            player.pickSpeed += .15f;
            player.statDefense += 15;
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