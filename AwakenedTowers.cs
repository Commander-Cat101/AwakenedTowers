using MelonLoader;
using BTD_Mod_Helper;
using AwakenedTowers;
using MainUI;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using Il2CppAssets.Scripts.Simulation.Objects;
using BTD_Mod_Helper.Api.Enums;
using Methods;

[assembly: MelonInfo(typeof(AwakenedTowers.AwakenedTower), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace AwakenedTowers;

public class AwakenedTower : BloonsTD6Mod
{
    public bool ingame = false;
    public override void OnApplicationStart()
    {
        ModHelper.Msg<AwakenedTower>("AwakenedTowers loaded!");
    }
    public override void OnMatchStart()
    {
        ingame = true;
        base.OnMatchStart();
    }
    public override void OnMatchEnd()
    {
        ingame = false;
        base.OnMatchEnd();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (ingame)
        {
            if (InGame.instance.inputManager.SelectedTower != null)
            {
                if (MainUI.MainUI.active == false)
                {
                    MainUI.MainUI.ShowUI();
                    if (InGame.instance.inputManager.SelectedTower.tower.GetMutatorById(GetVisionMutator().id) != null)
                    {
                        UIMethods.SetVisionBuyable(false);
                    }
                    else
                    {
                        UIMethods.SetVisionBuyable(true);
                    }

                    if (InGame.instance.inputManager.SelectedTower.tower.GetMutatorById(GetDamageMutator().id) != null)
                    {
                        UIMethods.SetDamageBuyable(false);
                    }
                    else
                    {
                        UIMethods.SetDamageBuyable(true);
                    }

                    if (InGame.instance.inputManager.SelectedTower.tower.GetMutatorById(GetSpeedMutator().id) != null)
                    {
                        UIMethods.SetSpeedBuyable(false);
                    }
                    else
                    {
                        UIMethods.SetSpeedBuyable(true);
                    }
                }
            }
            else if (MainUI.MainUI.active)
            {
                MainUI.MainUI.HideUI();
            }
        }
    }
    public static BehaviorMutator GetVisionMutator()
    {
        BehaviorMutator mutator = new RangeSupport.MutatorTower(true, "VisionMutator", 0, 0, null);
        return mutator;
    }
    public static BehaviorMutator GetDamageMutator()
    {
        BehaviorMutator mutator = new DamageSupport.MutatorTower(1, true, "DamageMutator", null);
        return mutator;
    }
    public static BehaviorMutator GetSpeedMutator()
    {
        BehaviorMutator mutator = new RateSupport.RateSupportMutator(true, "SpeedMutator", 0, 0, null);
        return mutator;
    }
} 