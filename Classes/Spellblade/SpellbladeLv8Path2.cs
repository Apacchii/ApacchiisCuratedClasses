using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Spellblade
{
	public class SpellbladeLv8Path2 : ModItem
	{
        public override string Texture => "ApacchiisCuratedClasses/Classes/Explorer/ExplorerLv8";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Spellblade Lv.8");
            Tooltip.SetDefault("[c/3485c5:[Holding a magic weaponc/3485c5:]]\n" +
                               "[c/3485c5:+11.2% Magic Damage\n" +
                               "[c/3485c5:-9.8% Mana Costs\n" +
                               "[c/3485c5:+32 Max Mana\n" +
                               "[c/d33838:[Holding a melee weapon[c/d33838:]]\n" +
                               "[c/d33838:+14.4% Magic Damage\n" +
                               "[c/d33838:+8% Melee Crit Chance\n" +
                               "[c/d33838:Melee Damage is also increased by Magic Damage\n" +
                               "[c/d33838:Defense is increased by 40% of magic crit\n" +
                               "-----------\n" +
                               "[Magic Blade] Base Damage: 40(+10)\n" +
                               "[Magic Blade] Base Mana Cost: 25\n" +
                               "[c/af7A6:[Path 2][c/af7A6:]]\n" +
                               "[Magic Blade] Projectile now pierces 2 more enemies\n" +
                               "[Magic Blade] Base damage increased by 10\n" +
                               "-8% Mana costs\n" +
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
            recipe.AddIngredient(ModContent.GetInstance<Classes.Spellblade.SpellbladeLv7Path2>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv8>(), 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            var acmPlayer = player.GetModPlayer<ApacchiisClassesMod.MyPlayer>();
            acmPlayer.hasEquippedClass = true;
            accPlayer.hasSpellblade = true;
            accPlayer.hasClassPath2 = true;

            if (player.HeldItem.magic)
            {
                player.magicDamage += .112f;
                player.manaCost -= .098f;
                player.statManaMax2 += 32;
            }

            if (player.HeldItem.melee)
            {
                player.magicDamage += .144f;
                player.meleeCrit += 8;
            }

            accPlayer.shokkZoneTimerBase = 50;
            accPlayer.magicBladeBaseDamage = 50;
            accPlayer.magicBladeBaseCost = 25;
            player.manaCost -= .08f;
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
