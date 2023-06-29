using static VFXParticlesPool;

namespace VELD.AlterraWeaponry.Items;

public class PrawnSelfDefenseModule
{
    public const float maxCharge = 10f;
    public const float energyCost = 5f;
    public const float cooldown = 5f;

    public static string ClassID = "PrawnSelfDefenseModule";
    public static TechType TechType { get; private set; } = 0;


    public static GameObject prefab;
    public PrefabInfo Info { get; private set; }

    public PrawnSelfDefenseModule()
    {
        Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithSizeInInventory(new(1, 1))
            .WithIcon(Main.assets.LoadAsset<Sprite>("Sprite.PrawnSelfDefenseModule"));

        TechType = this.Info.TechType;

    }

    public void Patch()
    {
        RecipeData recipe = new()
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

        CustomPrefab customPrefab = new(this.Info);
        CloneTemplate clone = new(this.Info, TechType.SeaTruckUpgradePerimeterDefense);

        customPrefab.SetGameObject(clone);
        
        customPrefab.SetVehicleUpgradeModule(EquipmentType.ExosuitModule, QuickSlotType.Chargeable)
            .WithEnergyCost(energyCost)
            .WithMaxCharge(maxCharge)
            .WithCooldown(5f)
            .WithOnModuleAdded((Vehicle instance, int slotID) =>
            {
                if (!instance.gameObject.TryGetComponent(out ZapFunctionalityBehaviour defMono))
                    instance.gameObject.EnsureComponent<ZapFunctionalityBehaviour>();
                else
                    Main.logger.LogWarning("For some reason, the defense mono was already existing on the prawn although the upgrade was not equipped.");
            })
            .WithOnModuleRemoved((Vehicle instance, int slotID) =>
            {
                if (instance.gameObject.TryGetComponent(out ZapFunctionalityBehaviour defMono))
                    UnityEngine.Object.Destroy(defMono);
                else
                    Main.logger.LogWarning("For some reason, the defense mono was no existing on Prawn although the upgrade was equipped.");
            })
            .WithOnModuleUsed((Vehicle instance, int slotID, float charge, float chargeScalar) =>
            {
                if (!instance.gameObject.TryGetComponent(out ZapFunctionalityBehaviour defenseMono))
                    defenseMono = instance.gameObject.EnsureComponent<ZapFunctionalityBehaviour>();
                Main.logger.LogInfo("Zapping !");
                try
                {
                    defenseMono.Zap(instance, slotID, charge, chargeScalar);
                }
                catch (Exception e)
                {
                    Main.logger.LogError($"Cannot use the defense mono.\nError:");
                    Main.logger.LogError(e);
                }
                Main.logger.LogInfo("Zapped !");
            });
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(2.5f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Upgrades", "ExosuitUpgrades");

        customPrefab.Register();
    }
}
