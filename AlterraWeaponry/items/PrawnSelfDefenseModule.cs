using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.items;

public class PrawnSelfDefenseModule : Equipable
{
    public static GameObject prefab;
    public static float maxCharge = 10f;
    public static float energyCost = 5f;
    public static TechType techType { get; private set; } = 0;

    public PrawnSelfDefenseModule() : base("PrawnSelfDefenseModule", "PrawnSelfDefenseModule", "Tooltip_PrawnSelfDefenseModule")
    {
        OnFinishedPatching += () =>
        {
            techType = TechType;
        };
    }

    public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
    public override float CraftingTime => 3f;
    public override EquipmentType EquipmentType => EquipmentType.ExosuitModule;
    public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
    public override int FragmentsToScan => 1;
    public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
    public override QuickSlotType QuickSlotType => QuickSlotType.Chargeable;
    public override TechType RequiredForUnlock => TechType.SeaTruckUpgradePerimeterDefense;
    public override Vector2int SizeInInventory => new(1, 1);
    public override string[] StepsToFabricatorTab => new string[] { "Upgrades", "ExosuitUpgrades" };
    protected override RecipeData GetBlueprintRecipe()
    {
        return new()
        {
            craftAmount = 1,
            Ingredients = new()
            {
                new(TechType.AdvancedWiringKit, 1),
                new(TechType.AluminumOxide, 2),
                new(TechType.PowerCell, 2),
                new(TechType.Polyaniline, 1)
            }
        };
    }
    protected override Sprite GetItemSprite()
    {
        return Main.assets.LoadAsset<Sprite>("Sprite.PrawnSelfDefenseModule");
    }
    public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
    {
        if (prefab == null)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.SeaTruckUpgradePerimeterDefense);
            yield return task;

            prefab = GameObject.Instantiate(task.GetResult());
        }

        GameObject go = GameObject.Instantiate(prefab);
        gameObject.Set(go);
    }
}
