using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwakenedTowers
{
    [HarmonyPatch(typeof(RangeSupport.MutatorTower), nameof(RangeSupport.MutatorTower.Mutate))]
    internal static class RangeSupport_MutatorTower_Mutate
    {
        [HarmonyPrefix]
        private static bool Prefix(RangeSupport.MutatorTower __instance, Model model)
        {
            if (__instance.id == AwakenedTower.GetVisionMutator().id)
            {
                model.GetDescendants<AttackModel>().ForEach(am =>
                {
                    am.attackThroughWalls = true;
                    am.range *= 1.5f;

                });
                model.TryCast<TowerModel>().ignoreBlockers = true;
                model.TryCast<TowerModel>().range *= 1.5f;
                model.GetDescendants<ProjectileModel>().ForEach(pm =>
                {
                    pm.ignoreBlockers = true;
                });
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(DamageSupport.MutatorTower), nameof(DamageSupport.MutatorTower.Mutate))]
    static class DamageSupport_MutatorTower_Mutate
    {
        [HarmonyPrefix]
        private static bool Prefix(DamageSupport.MutatorTower __instance, Model model)
        {
            if (__instance.id == AwakenedTower.GetDamageMutator().id)
            {
                model.GetDescendants<ProjectileModel>().ForEach(pm =>
                {
                    var Fire = Game.instance.model.GetTower(TowerType.MortarMonkey, 0, 0, 2).GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetBehavior<AddBehaviorToBloonModel>();
                    Fire.GetBehavior<DamageOverTimeModel>().damage = pm.GetDamageModel().damage / 10;
                    Fire.GetBehavior<DamageOverTimeModel>().displayLifetime = 0.5f;
                    Fire.GetBehavior<DamageOverTimeModel>().interval = 0.1f;
                    pm.AddBehavior(Fire);
                    pm.collisionPasses = new[] { -1, 0 };
                });
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(RateSupport.RateSupportMutator), nameof(RateSupport.RateSupportMutator.Mutate))]
    static class RateSupport_MutatorTower_Mutate
    {
        [HarmonyPrefix]
        private static bool Prefix(RateSupport.RateSupportMutator __instance, Model model)
        {
            if (__instance.id == AwakenedTower.GetDamageMutator().id)
            {
                model.GetDescendants<AttackModel>().ForEach(am =>
                {
                    am.AddBehavior(new StartOfRoundRateBuffModel("DoubleSpeedAfter5Seconds", 2, 5));
                    am.weapons.ForEach(wp =>
                    {
                        wp.Rate *= 0.5f;
                    });
                });
            }
            return true;
        }
    }
}
