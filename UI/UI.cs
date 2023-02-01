using AwakenedTowers;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using TaskScheduler = BTD_Mod_Helper.Api.TaskScheduler;

namespace MainUI
{
    public static class MainUI
    {
        public static bool active;
        public static ModHelperPanel screenpanel;
        public static ModHelperPanel panel;
        public static Animator animator;
        public static ModHelperPanel buy;

        public static ModHelperButton VisionButton;
        public static ModHelperText VisionButtonText;

        public static ModHelperButton DamageButton;
        public static ModHelperText DamageButtonText;

        public static ModHelperButton SpeedButton;
        public static ModHelperText SpeedButtonText;
        private static void CreateUI(GameObject Screen)
        {
            screenpanel = Screen.AddModHelperPanel(new BTD_Mod_Helper.Api.Components.Info("ScreenPanel"));
            panel = Screen.AddModHelperPanel(new Info("Panel", 0, 0, 1200, 500), VanillaSprites.MainBGPanelBlueNotchesShadow);

            var matchpos = panel.AddComponent<MatchLocalPosition>();
            matchpos.transformToCopy = GameObject.Find("MainHudLeftAlign(Clone)").transform; 
            matchpos.offset = new Vector3(1900, -200);
            matchpos.scale = new Vector3(1, 1);

            DamageButton = panel.AddButton(new Info("DamageButton", -400, 0, 300, 300), VanillaSprites.UpgradeContainerRed, new Action(DamageBought));
            var image = DamageButton.AddImage(new Info("DamageUpgradeImage", 0, 0, 250, 250), VanillaSprites.FirestormAA);

            DamageButtonText = panel.AddText(new Info("DamageText", -400, 150, 400, 80), "Damage", 70);
            panel.AddText(new Info("DamageCostText", -400, -150, 400, 80), "$2500", 70);

            VisionButton = panel.AddButton(new Info("VisionButton", 0, 0, 300, 300), VanillaSprites.PsiVision, new Action(VisionBought));
            VisionButtonText = panel.AddText(new Info("VisionText", 0, 150, 400, 80), "Vision", 70);
            panel.AddText(new Info("VisionCostText", 0, -150, 400, 80), "$2500", 70);

            SpeedButton = panel.AddButton(new Info("SpeedButton", 400, 0, 300, 300), VanillaSprites.UpgradeContainerGrey, new Action(SpeedBought));
            SpeedButton.AddImage(new Info("SpeedUpgradeImage", 0, 0, 250, 250), VanillaSprites.FasterShootingUpgradeIcon2);

            SpeedButtonText = panel.AddText(new Info("SpeedText", 400, 150, 400, 80), "Speed", 70);
            panel.AddText(new Info("SpeedCostText", 400, -150, 400, 80), "$2500", 70);

            animator = panel.AddComponent<Animator>();
            animator.runtimeAnimatorController = Animations.PopupAnim;
            animator.speed = .55f;
        }
        private static void Init()
        {
            var screen = InGame.instance.uiRect.transform;
            var ButtonPanel = screen.FindChild("Panel");
            if (ButtonPanel == null)
                CreateUI(screen.gameObject);
        }
        private static void Hide()
        {
            panel.GetComponent<Animator>().Play("PopupSlideOut");
            TaskScheduler.ScheduleTask(() => panel.SetActive(false), ScheduleType.WaitForFrames, 13);
        }
        public static void ShowUI()
        {
            Init();
            active = true;
            panel.SetActive(true);
            panel.GetComponent<Animator>().Play("PopupSlideIn");
        }
        public static void HideUI()
        {
            var screen = InGame.instance.uiRect.transform;
            var ButtonPanel = screen.FindChild("Panel");
            if (ButtonPanel != null)
                Hide();
            active = false;
        }
        public static void VisionBought()
        {
            if (InGame.instance.GetCash() >= 2500)
            {
                InGame.instance.AddCash(-2500);
                InGame.instance.inputManager.SelectedTower.tower.AddMutatorIncludeSubTowers(AwakenedTower.GetVisionMutator());
                InGame.instance.inputManager.SelectedTower.tower.worth += 2500;
                UIMethods.SetVisionBuyable(false);
            }
        }
        public static void DamageBought()
        {
            if (InGame.instance.GetCash() >= 2500)
            {
                InGame.instance.AddCash(-2500);
                InGame.instance.inputManager.SelectedTower.tower.AddMutatorIncludeSubTowers(AwakenedTower.GetDamageMutator());
                InGame.instance.inputManager.SelectedTower.tower.worth += 2500;
                UIMethods.SetDamageBuyable(false);
            }
        }

        public static void SpeedBought()
        {
            if (InGame.instance.GetCash() >= 2500)
            {
                InGame.instance.AddCash(-2500);
                InGame.instance.inputManager.SelectedTower.tower.AddMutatorIncludeSubTowers(AwakenedTower.GetSpeedMutator());
                InGame.instance.inputManager.SelectedTower.tower.worth += 2500;
                UIMethods.SetSpeedBuyable(false);
            }
        }
    }
}
