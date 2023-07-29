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
        if (!Main.AssetsCache.TryGetAsset("PrawnSelfDefenseModule", out Sprite icon))
            Main.logger.LogError("Unable to load PrawnSelfDefenseModule sprite from cache.");

        Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithSizeInInventory(new(1, 1))
            .WithIcon(icon);

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

        CloneTemplate clone = new(this.Info,
#if BZ
            TechType.SeaTruckUpgradePerimeterDefense
#elif SN1
            TechType.SeamothElectricalDefense
#endif
        );

        customPrefab.SetGameObject(clone);

        var scanningGadget = customPrefab.SetUnlock(TechType.Polyaniline);
        scanningGadget.WithPdaGroupCategoryAfter(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades, TechType.ExosuitThermalReactorModule);
#if BZ  // Sets this only on BZ if it can find it.
        if (!Main.AssetsCache.TryGetAsset("UpgradePopup", out Sprite popupSprite))
            Main.logger.LogError("Unable to load UpgradePopup sprite from cache.");
        else
            scanningGadget.WithEncyclopediaEntry("Tech/Weaponry", popupSprite);
#endif

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
