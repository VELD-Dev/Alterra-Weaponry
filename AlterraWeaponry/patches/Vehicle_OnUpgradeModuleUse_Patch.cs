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
        if(techType == PrawnSelfDefenseModule.techType)
        {
            Main.logger.LogInfo($"OnUpgradeModuleUse input received on slot {slotID}");
            if (!__instance.TryGetComponent(out ZapFunctionalityBehaviour defenseMono))
                return;
            defenseMono.Zap(__instance);
        }
    }
}
