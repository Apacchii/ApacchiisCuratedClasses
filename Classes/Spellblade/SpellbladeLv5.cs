using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Spellblade
{
	public class SpellbladeLv5 : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Spellblade Lv.5 [Path 1]");
            Tooltip.SetDefault("[c/3485c5:[Holding a magic weaponc/3485c5:]]\n" +
                               "[c/3485c5:+7% Magic Damage\n" +
                               "[c/3485c5:-6% Mana Costs\n" +
                               "[c/3485c5:+20 Max Mana\n" +
                               "[c/d33838:[Holding a melee weapon[c/d33838:]]\n" +
                               "[c/d33838:+9% Magic Damage\n" +
                               "[c/d33838:+5% Melee Crit Chance\n" +
                               "[c/d33838:Melee Damage is also increased by Magic Damage\n" +
                               "[c/d33838:Defense is increased by 40% of magic crit\n" +
                               "-----------\n" +
                               "[Magic Blade] Base Damage: 25\n" +
                               "[Magic Blade] Base Mana Cost: 16\n" +
                               "[c/af7A6:[Path 1][c/af7A6:]]\n" +
                               "[Shokk Zone] Now attacks faster\n" +
                               "[Shokk Zone] Base cooldown reduced by 8 seconds\n" +
                               "[Shokk Zone] Base duration increased by 2 seconds\n" +
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
            recipe.AddIngredient(ModContent.GetInstance<Classes.Spellblade.SpellbladeLv4>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv5>(), 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            var acmPlayer = player.GetModPlayer<ApacchiisClassesMod.MyPlayer>();
            acmPlayer.hasEquippedClass = true;
            accPlayer.hasSpellblade = true;
            accPlayer.hasClassPath1 = true;

            if (player.HeldItem.magic)
            {
                player.magicDamage += .07f;
                player.manaCost -= .06f;
                player.statManaMax2 += 20;
            }

            if (player.HeldItem.melee)
            {
                player.magicDamage += .09f;
                player.meleeCrit += 5;
            }

            accPlayer.shokkZoneTimerBase = 35;
            accPlayer.magicBladeBaseDamage = 25;
            accPlayer.magicBladeBaseCost = 16;
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

