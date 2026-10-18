namespace VELD.AlterraWeaponry.Items;

internal class ExplosiveTorpedo
{
    public static string ClassID = "ExplosiveTorpedo";
    public static TechType TechType { get; private set; } = 0;


    public static GameObject prefab;
    public PrefabInfo Info { get; private set; }

    public ExplosiveTorpedo()
    {
        if (!Main.AssetsCache.TryGetAsset("ExplosiveTorpedo", out Sprite icon))
            Main.logger.LogError("Unable to load ExplosiveTorpedo Sprite from cache.");

        Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithSizeInInventory(new(1, 1))
            .WithIcon(icon);

        TechType = Info.TechType;
    }

    public void Patch()
    {
        RecipeData recipe = new()
        {
            craftAmount = 2,
            Ingredients =
            [
                new(BlackPowder.TechType, 2),
                new(TechType.Titanium, 1)
            ]
        };

        CustomPrefab customPrefab = new(Info);
        CloneTemplate clone = new(Info, TechType.GasTorpedo);

        customPrefab.SetGameObject(clone);
        var scanningGadget = customPrefab.SetUnlock(BlackPowder.TechType);
        scanningGadget.WithPdaGroupCategoryAfter(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades, TechType.GasTorpedo);
        scanningGadget.WithCompoundTechsForUnlock([ Coal.TechType ]);

#if BZ  // This sets the popup on BZ if it can find it.
        if (!Main.AssetsCache.TryGetAsset("UpgradePopup", out Sprite popupSprite))
        {
            Main.logger.LogError("Unable to load UpgradePopup sprite from cache.");
        }
        else
        {
            scanningGadget.WithEncyclopediaEntry("Tech/Weaponry", popupSprite);
            scanningGadget.WithAnalysisTech(popupSprite, PDAHandler.UnlockImportant);
        }
#endif

        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(3f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Upgrades", "ExosuitUpgrades");

        customPrefab.Register();
    }
}
