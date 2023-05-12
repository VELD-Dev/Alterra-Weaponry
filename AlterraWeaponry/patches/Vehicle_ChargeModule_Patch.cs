

namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(Vehicle))]
public class Vehicle_ChargeModule_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Vehicle.ChargeModule))]
    public static bool ChargeModule(Vehicle __instance, TechType techType, int slotID)
    {
        float num = __instance.quickSlotCharge[slotID];
        float maxCharge = TechData.GetMaxCharge(techType);

        // TEMP CODE WAITING FOR SMLHELPER UPDATE
        if(techType == PrawnSelfDefenseModule.TechType)
            maxCharge = PrawnSelfDefenseModule.maxCharge;

        float num2;
        TechData.GetEnergyCost(techType, out num2);

        // TEMP CODE WAITING FOR SMLHELPER UPDATE
        if (techType == PrawnSelfDefenseModule.TechType)
            num2 = PrawnSelfDefenseModule.energyCost;

        float num3 = num2 * Time.deltaTime;
        float num4 = maxCharge - num;
        bool flag = num3 >= num4;
        float b = flag ? Mathf.Max(0f, num4) : num3;
        float num5 = Mathf.Min(__instance.energyInterface.TotalCanProvide(out int num6), b);
        __instance.ConsumeEnergy(num5);
        __instance.quickSlotCharge[slotID] = __instance.quickSlotCharge[slotID] + num5;
        if (__instance.quickSlotCharge[slotID] > 0f && (flag || num5 == 0f))
        {
            __instance.OnUpgradeModuleUse(techType, slotID);
            __instance.quickSlotCharge[slotID] = 0f;
        }
        return false;
    }
}
