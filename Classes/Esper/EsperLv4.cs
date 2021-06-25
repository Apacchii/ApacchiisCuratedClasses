using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisCuratedClasses.Classes.Esper
{
	public class EsperLv4 : ModItem
	{
        Player player = Main.player[Main.myPlayer];
        Mod EsperClassMod = ModLoader.GetMod("EsperClass");

        public override void SetStaticDefaults()
        {
            if(EsperClassMod != null)
            {
                DisplayName.SetDefault("Class: Esper Lv.4");
                Tooltip.SetDefault("+14% Telekinetic Damage\n" +
                                   "+8% Telekinetic Crit Chance\n" +
                                   "+40% Telekinetic Velocity\n" +
                                   "(By: TheLoneGamer)\n" +
                                   "(Custom Sounds By: Peb)");
            }
            else
            {
                DisplayName.SetDefault("Class: Esper Lv.4");
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
			item.rare = 4;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.GetInstance<ApacchiisClassesMod.Items.Tokens.ClassTokenLv4>(), 1);
            recipe.AddIngredient(ModContent.GetInstance<EsperLv3>());
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

                //Reflection for cross-mod compatability without hard references
                ModPlayer ECPlayer = player.GetModPlayer(EsperClassMod, "ECPlayer");
                Type ECPlayerType = ECPlayer.GetType();

                // Telekinetic Damage
                FieldInfo tkDamage = ECPlayerType.GetField("tkDamage", BindingFlags.Instance | BindingFlags.Public);
                float oldtkDamage = (float)tkDamage.GetValue(ECPlayer);
                tkDamage.SetValue(ECPlayer, oldtkDamage + 0.14f);

                // Telekinetic Crit
                FieldInfo tkCrit = ECPlayerType.GetField("tkCrit", BindingFlags.Instance | BindingFlags.Public);
                int oldtkCrit = (int)tkCrit.GetValue(ECPlayer);
                tkCrit.SetValue(ECPlayer, oldtkCrit + 8);

                // Telekinetic Velocity
                FieldInfo tkVel = ECPlayerType.GetField("tkVel", BindingFlags.Instance | BindingFlags.Public);
                float oldtkVel = (float)tkVel.GetValue(ECPlayer);
                tkVel.SetValue(ECPlayer, oldtkVel + 0.4f);
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
