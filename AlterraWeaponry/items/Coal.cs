namespace VELD.AlterraWeaponry.Items;

internal class Coal
{
    public static string ClassID = "Coal";
    public static TechType TechType { get; private set; } = 0;


    public PrefabInfo Info { get; private set; }

    public Coal()
    {
        Main.logger.LogDebug("Loading Coal prefab info");
        if (!Main.AssetsCache.TryGetAsset("Coal", out Sprite icon))
            Main.logger.LogError("Unable to load Coal Sprite from cache.");

        this.Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, unlockAtStart: true, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithIcon(icon)
            .WithSizeInInventory(new(1, 1));

        TechType = this.Info.TechType;
        Main.logger.LogDebug("Loaded Coal prefab and assigned techType");
    }

    public void Patch()
    {
        Main.logger.LogDebug("Setting Coal recipe");
        RecipeData recipe = new()
        {
            craftAmount = 4,
            Ingredients = new()
            {
                new(TechType.CreepvinePiece, 1)
            }
        };

        Main.logger.LogDebug("Recipe set, now patching prefab.");

        CustomPrefab customPrefab = new(this.Info);
        PrefabTemplate clone = new CloneTemplate(this.Info, TechType.Nickel);

        customPrefab.SetGameObject(clone);
        customPrefab.SetUnlock(TechType.CreepvineSeedCluster);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(4f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "BasicMaterials");

        customPrefab.Register();

        Main.logger.LogDebug("Prefab loaded and registered for Coal.");
    }
}
