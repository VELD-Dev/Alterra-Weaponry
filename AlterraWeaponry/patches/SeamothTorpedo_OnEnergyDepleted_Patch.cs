namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(SeamothTorpedo))]
public class SeamothTorpedo_OnEnergyDepleted_Patch // Thanks to Grimm The Second !
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SeamothTorpedo.OnEnergyDepleted))]
    public static bool OnEnergyDepleted(SeamothTorpedo __instance)
    {
        return __instance.enabled;
    }
}
