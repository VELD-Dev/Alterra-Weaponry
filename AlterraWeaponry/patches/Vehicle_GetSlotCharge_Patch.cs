namespace VELD.AlterraWeaponry.Patches;


[HarmonyPatch(typeof(Vehicle))]
internal class Vehicle_GetSlotCharge_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Vehicle.GetSlotCharge))]
    public static bool GetSlotCharge(Vehicle __instance, int slotID, ref float __result)
    {
        if (slotID < 0 || slotID >= __instance.slotIDs.Length)
        {
            __result = 1f;
            return false;
        }
        TechType techType;
        QuickSlotType quickSlotType = __instance.GetQuickSlotType(slotID, out techType);
        if (quickSlotType == QuickSlotType.Chargeable || quickSlotType == QuickSlotType.SelectableChargeable)
        {
            float maxCharge = TechData.GetMaxCharge(techType);

            // TEMPORARY PATCH, WAITING FOR AN SMLHELPER UPDATE
            bool flag = techType == PrawnSelfDefenseModule.TechType;
            if (flag)
                maxCharge = PrawnSelfDefenseModule.maxCharge;

            if (maxCharge > 0f)
            {
                __result = __instance.quickSlotCharge[slotID] / maxCharge;
                Main.logger.LogInfo($"Slot charge: {__result}");
                return false;
            }
        }
        __result = 1f;
        return false;
    }
}
