using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWE;

namespace VELD.AlterraWeaponry.behaviours;

internal class ZapFunctionalityBehaviour : MonoBehaviour // Thanks to ECM and PrimeSonic 👌
{
    private static GameObject seamothElectricalDefensePrefab = null;

    public static GameObject ElectricalDefensePrefab => seamothElectricalDefensePrefab;

    private const float EnergyCostPerZap = 5;
    private const float ZapPower = 6f;
    private const float BaseCharge = 2f;
    private const float BaseRadius = 1f;

    public const float ZapCooldown = 10f;
    public static float timeNextZap = 0;
    private static float DamageMultiplier => 1f;
    private static float DirectZapDamage = (BaseRadius + ZapPower * BaseCharge) * DamageMultiplier * 0.5f;
    // Calculations and initial values based off ElectricalDefense component

    public static bool AbleToZap(Vehicle vehicle)
    {
        vehicle.energyInterface.GetValues(out float charge, out float capacity);
        if (GameModeManager.GetOption<bool>(GameOption.TechnologyRequiresPower) &&  charge < EnergyCostPerZap)
            return false;

        return true;
    }

    public static IEnumerator UpdateDefensePrefab()
    {
        if (seamothElectricalDefensePrefab) yield break;

        var task = CraftData.GetPrefabForTechTypeAsync(TechType.SeaTruck);
        yield return task;
        var prefab = task.GetResult();

        seamothElectricalDefensePrefab = prefab?.GetComponent<SeaTruckUpgrades>().electricalDefensePrefab;
    }

    public bool Zap(Vehicle vehicle)
    {
        CoroutineHost.StartCoroutine(UpdateDefensePrefab());
        if (Time.time < timeNextZap)
            return true;

        if (!AbleToZap(vehicle))
            return false;

        ZapRadius(vehicle);

        timeNextZap = Time.time + ZapCooldown;
        return true;
    }

    private static void ZapRadius(Vehicle vehicle)
    {
        if (vehicle == null)
            return;

        GameObject gameObject = Utils.SpawnZeroedAt(ElectricalDefensePrefab, vehicle.transform, false);
        ElectricalDefense defenseComponent = gameObject.GetComponent<ElectricalDefense>();
        defenseComponent.charge = ZapPower;
        defenseComponent.chargeScalar = ZapPower;
        defenseComponent.radius *= ZapPower;
        defenseComponent.chargeRadius *= ZapPower;

        if (GameModeManager.GetOption<bool>(GameOption.TechnologyRequiresPower))
            vehicle.energyInterface.ConsumeEnergy(EnergyCostPerZap);
    }
}
