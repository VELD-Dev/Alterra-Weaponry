namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(Vehicle))]
internal class Vehicle_OnUpgradeModuleChange_Patch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Vehicle.OnUpgradeModuleChange))]
    public static void OnUpgradeModuleChange(TechType techType, bool added, Vehicle __instance)
    {
        if(techType == PrawnSelfDefenseModule.TechType)
        {
            if(added)
            {
                Main.logger.LogInfo("Adding component ZapFunctionality to Vehicle.");
                __instance.gameObject.AddComponent<ZapFunctionalityBehaviour>();
                Main.logger.LogInfo("Added successfully ZapFunctionality to Vehicle.");
            }
            else
            {
                __instance.TryGetComponent<ZapFunctionalityBehaviour>(out ZapFunctionalityBehaviour defenseMono);
                if (defenseMono != null)
                    UnityEngine.Object.Destroy(defenseMono);
            }
        }
    }
}
