using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Esper
{
	public class EsperLv6Path2 : ModItem
	{
        Player player = Main.player[Main.myPlayer];
        Mod EsperClassMod = ModLoader.GetMod("EsperClass");

        public override string Texture => "ApacchiisCuratedClasses/Classes/Esper/EsperLv6";

        public override void SetStaticDefaults()
        {
            if (EsperClassMod != null)
            {
                DisplayName.SetDefault("Class: Esper Lv.6");
                Tooltip.SetDefault("+21% Telekinetic Damage\n" +
                                   "+12% Telekinetic Crit Chance\n" +
                                   "+60% Telekinetic Velocity\n" +
                                   "[c/af7A6:[Path 2][c/af7A6:]]\n" +
                                   "+5 Max Psychosis\n" +
                                   "[Telekinetic Hover] Base drain rate lowered to 1\n" +
                                   "[Telekinetic Hover] Boosted movement speed while in use\n" +
                                   "(By: TheLoneGamer)\n" +
                                   "(Custom Sounds By: Peb)");
            }
            else
            {
                DisplayName.SetDefault("Class: Esper Lv.6");
                Tooltip.SetDefault("[c/d33838:This class requires the mod 'Esper Class' to work]\n" +
                                   "[c/d33838:You can download it through the Mod Browser]");
            }
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
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv6>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<EsperLv5Path2>());
            recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void UpdateAccessory (Player player, bool hideVisual)
		{
            ACCPlayer accPlayer = Main.player[player.whoAmI].GetModPlayer<ACCPlayer>();
            if (EsperClassMod != null)
            {
                player.GetModPlayer<ApacchiisClassesMod.MyPlayer>().hasEquippedClass = true;
                accPlayer.hasEsper = true;
                accPlayer.hasClassPath2 = true;

                //Reflection for cross-mod compatability without hard references
                ModPlayer ECPlayer = player.GetModPlayer(EsperClassMod, "ECPlayer");
                Type ECPlayerType = ECPlayer.GetType();

                // Telekinetic Damage
                FieldInfo tkDamage = ECPlayerType.GetField("tkDamage", BindingFlags.Instance | BindingFlags.Public);
                float oldtkDamage = (float)tkDamage.GetValue(ECPlayer);
                tkDamage.SetValue(ECPlayer, oldtkDamage + 0.21f);

                // Telekinetic Crit
                FieldInfo tkCrit = ECPlayerType.GetField("tkCrit", BindingFlags.Instance | BindingFlags.Public);
                int oldtkCrit = (int)tkCrit.GetValue(ECPlayer);
                tkCrit.SetValue(ECPlayer, oldtkCrit + 12);

                // Telekinetic Velocity
                FieldInfo tkVel = ECPlayerType.GetField("tkVel", BindingFlags.Instance | BindingFlags.Public);
                float oldtkVel = (float)tkVel.GetValue(ECPlayer);
                tkVel.SetValue(ECPlayer, oldtkVel + 0.6f);

                // Max Psychosis
                FieldInfo maxPsychosis2 = ECPlayerType.GetField("maxPsychosis2", BindingFlags.Instance | BindingFlags.Public);
                int oldmaxPsychosis2 = (int)maxPsychosis2.GetValue(ECPlayer);
                maxPsychosis2.SetValue(ECPlayer, oldmaxPsychosis2 + 5);
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
