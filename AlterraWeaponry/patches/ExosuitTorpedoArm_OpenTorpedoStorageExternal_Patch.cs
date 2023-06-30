namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(ExosuitTorpedoArm))]
public class ExosuitTorpedoArm_OpenTorpedoStorageExternal_Patch // Thanks to Grimm The Second !
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ExosuitTorpedoArm), nameof(ExosuitTorpedoArm.OpenTorpedoStorageExternal))]
    public static void OpenTorpedoStorageExternal(ExosuitTorpedoArm __instance)
    {
        Main.logger.LogDebug("Trying to open PRAWN torpedo arm. Adding TechType: " + ExplosiveTorpedo.TechType);
        __instance.container.allowedTech.AddRange(new[]
        {
            ExplosiveTorpedo.TechType
        });
        Main.logger.LogDebug("Added torpedo techtypes to PRAWN torpedo arm container filter.");
    }
}
