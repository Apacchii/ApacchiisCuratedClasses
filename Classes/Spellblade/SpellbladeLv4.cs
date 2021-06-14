using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Spellblade
{
	public class SpellbladeLv4 : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Spellblade Lv.4");
            Tooltip.SetDefault("[c/3485c5:[Holding a magic weapon[c/3485c5:]]\n" +
                               "[c/3485c5:+5.6% Magic Damage]\n" +
                               "[c/3485c5:-4.8% Mana Costs]\n" +
                               "[c/3485c5:+16 Max Mana]\n" +
                               "[c/d33838:[Holding a melee weapon[c/d33838:]]\n" +
                               "[c/d33838:+7.2% Magic Damage]\n" +
                               "[c/d33838:+4% Melee Crit Chance]\n" +
                               "[c/d33838:Melee Damage is also increased by Magic Damage]\n" +
                               "[c/d33838:Defense is increased by 40% of magic crit]\n" +
                               "-----------\n" +
                               "[Magic Blade] Base Damage: 20\n" +
                               "[Magic Blade] Base Mana Cost: 13\n" +
                               "(By: Apacchii)");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0;
			item.rare = 4;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.GetInstance<Classes.Spellblade.SpellbladeLv3>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv4>(), 1);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            var acmPlayer = player.GetModPlayer<ApacchiisClassesMod.MyPlayer>();
            acmPlayer.hasEquippedClass = true;
            accPlayer.hasSpellblade = true;

            if (player.HeldItem.magic)
            {
                player.magicDamage += .056f;
                player.manaCost -= .048f;
                player.statManaMax2 += 16;
            }

            if (player.HeldItem.melee)
            {
                player.magicDamage += .072f;
                player.meleeCrit += 4;
            }

            accPlayer.shokkZoneTimerBase = 50;
            accPlayer.magicBladeBaseDamage = 20;
            accPlayer.magicBladeBaseCost = 13;
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

