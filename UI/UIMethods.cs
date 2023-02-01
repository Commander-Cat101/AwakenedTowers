using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    public class UIMethods
    {
        public static void SetVisionBuyable(bool Active)
        {
            MainUI.MainUI.VisionButton.Button.interactable = Active;
            switch (Active)
            {
                case true:
                    MainUI.MainUI.VisionButtonText.SetText("Vision");
                    break;
                case false:
                    MainUI.MainUI.VisionButtonText.SetText("Bought");
                    break;
            }
        }
        public static void SetDamageBuyable(bool Active)
        {
            MainUI.MainUI.DamageButton.Button.interactable = Active;
            switch (Active)
            {
                case true:
                    MainUI.MainUI.DamageButtonText.SetText("Damage");
                    break;
                case false:
                    MainUI.MainUI.DamageButtonText.SetText("Bought");
                    break;
            }
        }
        public static void SetSpeedBuyable(bool Active)
        {
            MainUI.MainUI.SpeedButton.Button.interactable = Active;
            switch (Active)
            {
                case true:
                    MainUI.MainUI.SpeedButtonText.SetText("Speed");
                    break;
                case false:
                    MainUI.MainUI.SpeedButtonText.SetText("Bought");
                    break;
            }
        }

    }
}
