using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Spellblade
{
	public class SpellbladeUltimate : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Spellblade Ultimate");
            Tooltip.SetDefault("[c/3485c5:[Holding a magic weaponc/3485c5:]]\n" +
                               "[c/3485c5:+14% Magic Damage]\n" +
                               "[c/3485c5:-12% Mana Costs]\n" +
                               "[c/3485c5:+40 Max Mana]\n" +
                               "[c/d33838:[Holding a melee weapon[c/d33838:]]\n" +
                               "[c/d33838:+18% Magic Damage]\n" +
                               "[c/d33838:+10% Melee Crit Chance]\n" +
                               "[c/d33838:Melee Damage is also increased by Magic Damage]\n" +
                               "[c/d33838:Defense is increased by 40% of magic crit]\n" +
                               "-----------\n" +
                               "[Magic Blade] Base Damage: 50\n" +
                               "[Magic Blade] Base Mana Cost: 28\n" +
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
			item.rare = 9;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.GetInstance<Classes.Spellblade.SpellbladeLv8>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenUltimate>(), 1);
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
                player.magicDamage += .14f;
                player.manaCost -= .12f;
                player.statManaMax2 += 40;
            }

            if (player.HeldItem.melee)
            {
                player.magicDamage += .18f;
                player.meleeCrit += 10;
            }

            accPlayer.shokkZoneTimerBase = 35;
            accPlayer.magicBladeBaseDamage = 50;
            accPlayer.magicBladeBaseCost = 28;
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

