using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWE;
using static VehicleUpgradeConsoleInput;
using static VFXParticlesPool;

namespace VELD.AlterraWeaponry.behaviours;

internal class ZapFunctionalityBehaviour : MonoBehaviour // Thanks to ECM and PrimeSonic 👌
{
    private static GameObject seamothElectricalDefensePrefab = null;

    public static GameObject ElectricalDefensePrefab => seamothElectricalDefensePrefab;

    public const float ZapCooldown = 5f;

    public float Overcharge { get; private set; }
    public float OverchargeScalar { get; private set; }

    public static IEnumerator UpdateDefensePrefab()
    {
        if (seamothElectricalDefensePrefab) yield break;

        var task = CraftData.GetPrefabForTechTypeAsync(TechType.SeaTruck);
        yield return task;
        var prefab = task.GetResult();

        seamothElectricalDefensePrefab = prefab?.GetComponent<SeaTruckUpgrades>().electricalDefensePrefab;
    }
    public bool Zap(Vehicle vehicle, int usedSlotID)
    {

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
        if (vehicle == null)
            return;

        GameObject gameObject = Utils.SpawnZeroedAt(ElectricalDefensePrefab, vehicle.transform, false);
        ElectricalDefense defenseComponent = gameObject.GetComponent<ElectricalDefense>();
        defenseComponent.charge = this.Overcharge;
        defenseComponent.chargeScalar = this.OverchargeScalar;
    }
}
