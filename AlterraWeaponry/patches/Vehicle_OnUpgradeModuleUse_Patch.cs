using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.patches;

[HarmonyPatch(typeof(Vehicle))]
public class Vehicle_OnUpgradeModuleUse_Patch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Vehicle.OnUpgradeModuleUse))]
    public static void OnUpgradeModuleUse(TechType techType, int slotID, Vehicle __instance)
    {
        if(__instance is not Exosuit)
            return;


        bool flag = true;
        float num = 0f;
        if (techType == PrawnSelfDefenseModule.techType)
        {
            if (!__instance.TryGetComponent(out ZapFunctionalityBehaviour defenseMono))
                return;
            defenseMono.Zap(__instance, slotID);
            num = ZapFunctionalityBehaviour.ZapCooldown;
        }
        if (flag)
        {
            __instance.quickSlotTimeUsed[slotID] = Time.time;
            __instance.quickSlotCooldown[slotID] = num;
        }
    }
}
