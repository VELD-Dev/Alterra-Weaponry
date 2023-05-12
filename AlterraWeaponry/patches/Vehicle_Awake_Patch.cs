namespace VELD.AlterraWeaponry.Patches;

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
            GameObject prefab = torpedoType?.prefab;
            ExplosiveTorpedoInitializer.InitPrefab(prefab);
        }
        __instance.torpedoTypes = __instance.torpedoTypes.ToArray().AddRangeToArray(new[]
        {
            ExplosiveTorpedoInitializer.torpedoType
        });
    }
}
