﻿namespace VELD.AlterraWeaponry.Behaviours;

internal class ZapFunctionalityBehaviour : MonoBehaviour // Thanks to ECM and PrimeSonic 👌
{
    private static GameObject seamothElectricalDefensePrefab = null;

    public static GameObject ElectricalDefensePrefab => seamothElectricalDefensePrefab;

    public float Overcharge { get; private set; }
    public float OverchargeScalar { get; private set; } 

    private void Awake()
    {
        if(seamothElectricalDefensePrefab == null)
            CoroutineHost.StartCoroutine(UpdateDefensePrefab());
    }

    public static IEnumerator UpdateDefensePrefab()
    {
        if (seamothElectricalDefensePrefab is not null) yield break;
        Main.logger.LogDebug("Updating defense prefab for ZapFunctionalityBehaviour.ElectricalDefensePrefab.");

#if BZ
        var task = CraftData.GetPrefabForTechTypeAsync(TechType.SeaTruck);
        yield return task;
        var prefab = task.GetResult();

        if (prefab == null) yield break;

        seamothElectricalDefensePrefab = prefab.GetComponent<SeaTruckUpgrades>().electricalDefensePrefab;
#elif SN1
        var task = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
        yield return task;
        var prefab = task.GetResult();

        if (prefab == null) yield break;

        seamothElectricalDefensePrefab = prefab.GetComponent<SeaMoth>().seamothElectricalDefensePrefab;
#endif

        Main.logger.LogDebug("Done !");
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
