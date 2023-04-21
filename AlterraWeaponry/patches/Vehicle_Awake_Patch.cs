using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VELD.AlterraWeaponry.behaviours;

namespace VELD.AlterraWeaponry.patches;

[HarmonyPatch(typeof(Vehicle))]
public class Vehicle_Awake_Patch // Thanks to Grimm The Second !
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Vehicle.Awake))]
    public static void Awake(Vehicle __instance)
    {
        bool flag = ExplosiveTorpedoInitializer.torpedoType == null;
        if(flag)
        {
            TorpedoType torpedoType = Enumerable.FirstOrDefault<TorpedoType>(__instance.torpedoTypes, (TorpedoType type) => type.techType == TechType.GasTorpedo);
            GameObject prefab = (torpedoType != null) ? torpedoType.prefab : null;
            ExplosiveTorpedoInitializer.InitPrefab(prefab);
        }
        __instance.torpedoTypes = __instance.torpedoTypes.ToArray().AddRangeToArray(new[]
        {
            ExplosiveTorpedoInitializer.torpedoType
        });
    }
}
