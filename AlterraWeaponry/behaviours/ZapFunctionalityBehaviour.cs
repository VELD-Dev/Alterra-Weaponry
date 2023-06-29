using VELD.AlterraWeaponry.Utils;

namespace VELD.AlterraWeaponry.Behaviours;

internal class ZapFunctionalityBehaviour : MonoBehaviour // Thanks to ECM and PrimeSonic 👌
{
    private static GameObject seamothElectricalDefensePrefab = null;

    public static GameObject ElectricalDefensePrefab => seamothElectricalDefensePrefab;

    public float Overcharge { get; private set; }
    public float OverchargeScalar { get; private set; } 

    public static IEnumerator UpdateDefensePrefab()
    {
        if (seamothElectricalDefensePrefab) yield break;

        var task = CraftData.GetPrefabForTechTypeAsync(TechType.SeaTruck);
        yield return task;
        var prefab = task.GetResult();

        if (prefab == null) yield break;

        seamothElectricalDefensePrefab = prefab.GetComponent<SeaTruckUpgrades>().electricalDefensePrefab;
    }
    public bool Zap(Vehicle vehicle, int usedSlotID, float charge, float chargeScalar)
    {
        Main.logger.LogInfo("Preparing the zap...");
        if (vehicle == null)
            return false;

        Main.logger.LogInfo("Should zap.");
        this.Overcharge = charge;
        this.OverchargeScalar = chargeScalar;
        Main.logger.LogInfo("Settings set, it should be zapping.");
        CoroutineHost.StartCoroutine(UpdateDefensePrefab());

        Main.logger.LogInfo("Executing Zap in radius..");
        ZapRadius(vehicle);
        return true;
    }

    private void ZapRadius(Vehicle vehicle)
    {

        GameObject gameObject = global::Utils.SpawnZeroedAt(ElectricalDefensePrefab, vehicle.transform, false);
        ElectricalDefense defenseComponent = gameObject.GetComponent<ElectricalDefense>();
        defenseComponent.charge = this.Overcharge;
        defenseComponent.chargeScalar = this.OverchargeScalar;
        defenseComponent.damage *= Main.Options.dmgMultiplier;
        Main.logger.LogInfo("Should have zapped !");
    }
}
