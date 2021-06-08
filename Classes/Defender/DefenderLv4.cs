using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Defender
{
	public class DefenderLv4 : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Defender Lv.4");
            Tooltip.SetDefault("+12% Sentry Damage\n" +
                               "+2 Max Sentries\n" +
                               "+20% Sentry Range\n" +
                               "+10% Sentry Speed\n" +
                               "[Detonate Sentries] Base Damage: 500\n" +
                               "(By: TheLoneGamer)");
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
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv4>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<DefenderLv3>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
            accPlayer.hasDefender = true;

            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryDamage += 0.12f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentryRange += 0.2f;
            player.GetModPlayer<ExpandedSentries.ESPlayer>().sentrySpeed += 0.1f;
            player.maxTurrets += 2;
            accPlayer.defenderAbility1Damage = 500;
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

