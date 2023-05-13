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
    public bool Zap(Vehicle vehicle, int usedSlotID)
    {
        if (vehicle == null)
            return false;

        float charge = vehicle.quickSlotCharge[usedSlotID];
        float slotCharge = vehicle.GetSlotCharge(usedSlotID);
        this.Overcharge = charge;
        this.OverchargeScalar = slotCharge;
        CoroutineHost.StartCoroutine(UpdateDefensePrefab());

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
    }
}
