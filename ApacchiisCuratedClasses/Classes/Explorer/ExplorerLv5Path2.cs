using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Explorer
{
	public class ExplorerLv5Path2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override string Texture => "ApacchiisCuratedClasses/Classes/Explorer/ExplorerLv5"; // <--

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Explorer Lv.5 [Path 2]"); // <-- Level 5 and Ultimate tokens say which path the token is, other levels do not
            Tooltip.SetDefault("+6% All Damage\n" +
                               "+5% Movement Speed\n" +
                               "+7.5% Mining Speed\n" +
                               "+5 Defense\n" +
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
			item.rare = 5;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv5>(), 1); // The official mod's Class Token Lv.X
            recipe.AddIngredient(ModContent.GetInstance<ExplorerLv4>()); // And the previous level class
            recipe.SetResult(this); // Makes the Lv.5 class
            recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true; // Necessary so players cant equip multiple classes at the same time
            accPlayer.hasExplorer = true; // Update ModPlayer's "hasExplorer" to true so we can make/use abilities/passives

            accPlayer.hasClassPath2 = true;
            // This is how you make paths work, this is necessary if the path changes something that changes anything in the ModPlayer class such as an ability's base damage or base cooldown
            // Otherwise, if the path only grants something like Ability Damage, this variable is not really needed since you only have to do something like on the line below in this file
            // accPlayer.abilityDamage += .2f   <-- +20% Ability Damage

            player.allDamage += .06f;
            player.moveSpeed += .09f;
            player.pickSpeed += .075f;
            player.statDefense += 10;
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

