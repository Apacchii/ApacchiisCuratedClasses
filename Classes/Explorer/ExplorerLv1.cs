using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Explorer
{
	public class ExplorerLv1 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Explorer Lv.1");
            Tooltip.SetDefault("+1.2% All Damage\n" +
                               "+1% Movement Speed\n" +
                               "+1.5% Mining Speed\n" +
                               "+1 Defense\n" +
                               "(By: Apacchii)");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0; // Value of 0 so the item is worth no gold when selling
			item.rare = 1; // Item rarity goes up by 1 each level
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.ClassPicker>(), 1); // Gets the official mod's Class Picker
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true; // Necessary so players cant equip multiple classes at the same time
            accPlayer.hasExplorer = true; // Update ModPlayer's "hasExplorer" to true so we can make/use abilities/passives

            player.allDamage += .012f;
            player.moveSpeed += .01f;
            player.pickSpeed += .015f;
            player.statDefense += 1;
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

