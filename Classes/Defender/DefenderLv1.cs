using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Defender
{
	public class DefenderLv1 : ModItem
	{
        Player player = Main.player[Main.myPlayer];
        Mod es = ModLoader.GetMod("ExpandedSentries");

        public override void SetStaticDefaults()
        {
            if(es != null)
            {
                DisplayName.SetDefault("Class: Defender Lv.1");
                Tooltip.SetDefault("+3% Sentry Damage\n" +
                                   "+1 Max Sentries\n" +
                                   "+5% Sentry Range\n" +
                                   "+2.5% Sentry Speed\n" +
                                   "[Detonate Sentries] Base Damage: 100\n" +
                                   "(By: TheLoneGamer)");
            }
            else
            {
                DisplayName.SetDefault("Class: Defender Lv.1");
                Tooltip.SetDefault("[c/d33838:This class required the mod 'Expanded Sentries' to work]\n" +
                                   "[c/d33838:You can download it through the Mod Browser]");
            }
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = 0;
			item.rare = 1;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.ClassPicker>(), 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void OnCraft(Recipe recipe)
        {
            if (es == null)
            {
                player.QuickSpawnItem(ModContent.ItemType<ApacchiisClassesMod.Items.ClassPicker>(), 1);
                Main.NewText("This class required the mod 'Expanded Sentries' to work, you can download it through the Mod Browser");
                Main.NewText("[Class Picker returned]");
            }
                base.OnCraft(recipe);
        }

        public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            if(es != null)
            {
                player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
                accPlayer.hasDefender = true;

                player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryDamage += 0.03f;
                player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryRange += 0.05f;
                player.GetModPlayer<ExpandedSentries.ESPlayer>().sentrySpeed += 0.025f;
                player.maxTurrets++;
                accPlayer.defenderAbility1Damage = 100;
            }
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
