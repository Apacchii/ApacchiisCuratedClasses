using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Explorer
{
	public class ExplorerLv6 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Explorer Lv.6"); // <-- Level 5 and Ultimate tokens say which path the token is, other levels do not
            Tooltip.SetDefault("+7.2% All Damage\n" +
                               "+6% Movement Speed\n" +
                               "+9% Mining Speed\n" +
                               "+6 Defense\n" +
                               "[c/af7A6:[Path 1][c/af7A6:]]\n" +
                               "Mining speed increased by an additional 5%\n" +
                               "Permanent night vision buff\n" +
                               "Ability 1 Base cooldown decreased from 42 to 35 seconds\n" +
                               "(By: Apacchii)");
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
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv6>(), 1); // The official mod's Class Token Lv.X
            recipe.AddIngredient(ModContent.GetInstance<ExplorerLv5>());  // And the previous level class with Path 1
            recipe.SetResult(this); // Makes the Lv.6 class Path 1
            recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true; // Necessary so players cant equip multiple classes at the same time
            accPlayer.hasExplorer = true; // Update ModPlayer's "hasExplorer" to true so we can make/use abilities/passives
            accPlayer.hasClassPath1 = true;

            player.allDamage += .072f;
            player.moveSpeed += .06f;
            player.pickSpeed += .14f;
            player.statDefense += 6;
            player.nightVision = true;
        }

        // Necessary so players cant equip multiple classes at the same time
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

