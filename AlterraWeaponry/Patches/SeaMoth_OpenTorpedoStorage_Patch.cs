#if SN1
namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(SeaMoth))]
internal class SeaMoth_OpenTorpedoStorage_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SeaMoth.OpenTorpedoStorage))]
    private static void OpenTorpedoStorage(SeaMoth __instance)
    {
        try
        {
            Main.logger.LogDebug("Trying to open Seamoth torpedo module storage. Adding TechType: " + ExplosiveTorpedo.TechType);
            for (int i = 0; i < __instance.slotIDs.Length; i++)
            {
                ItemsContainer storageInSlot = __instance.GetStorageInSlot(i, TechType.SeamothTorpedoModule);
                storageInSlot.allowedTech.AddRange(new[]
                {
                    ExplosiveTorpedo.TechType
                });
            }
            Main.logger.LogDebug("Added torpedo techtypes to PRAWN torpedo arm container filter.");
        }
        catch (Exception e)
        {
            Main.logger.LogError(e);
        }
    }
}
#endif