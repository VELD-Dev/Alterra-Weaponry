namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(Vehicle))]
public class Vehicle_Awake_Patch // Thanks to Grimm The Second !
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Vehicle.Awake))]
    public static void Awake(Vehicle __instance)
    {
        if(ExplosiveTorpedoInitializer.torpedoType == null)
        {
            TorpedoType torpedoType = Enumerable.FirstOrDefault(__instance.torpedoTypes, (TorpedoType type) => type.techType == TechType.GasTorpedo);
            GameObject prefab = torpedoType?.prefab;
            ExplosiveTorpedoInitializer.InitPrefab(prefab);
        }
        __instance.torpedoTypes = CollectionExtensions.AddRangeToArray(__instance.torpedoTypes, new[]
        {
            ExplosiveTorpedoInitializer.torpedoType
        }).ToArray();
    }
}
