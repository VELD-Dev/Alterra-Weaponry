using Nautilus.Assets.PrefabTemplates;

namespace VELD.AlterraWeaponry.Items;

internal class ExplosiveTorpedo
{
    public static string ClassID = "ExplosiveTorpedo";
    public static TechType TechType { get; private set; } = 0;


    public static GameObject prefab;
    public PrefabInfo Info { get; private set; }

    public ExplosiveTorpedo()
    {
        if (!Main.resources.TryGetAsset("ExplosiveTorpedo", out Sprite icon))
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
            Ingredients = new()
            {
                new(BlackPowder.TechType, 2),
                new(TechType.Titanium, 1)
            }
        };

        CustomPrefab customPrefab = new(Info);
        CloneTemplate clone = new(Info, TechType.GasTorpedo);

        customPrefab.SetGameObject(clone);
        if (!Main.resources.TryGetAsset("UpgradePopup", out Sprite popupSprite))
            Main.logger.LogError("Unable to load UpgradePopup sprite from cache.");
        customPrefab.SetUnlock(Coal.TechType)
            .WithEncyclopediaEntry("Tech/Weaponry", popupSprite);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(3f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Upgrades", "ExosuitUpgrades");

        customPrefab.Register();
    }
}
