namespace VELD.AlterraWeaponry.Items;

internal class BlackPowder
{
    public static string ClassID = "BlackPowder";
    public static TechType TechType { get; private set; } = 0;


    public static GameObject AssetPrefab = Main.AssetsCache.GetAsset<GameObject>("BlackPowder");
    public PrefabInfo Info { get; private set; }

    public BlackPowder()
    {
        Main.logger.LogDebug("Loading BlackPowder prefab info");
        if (!Main.AssetsCache.TryGetAsset("BlackPowder", out Sprite icon))
            Main.logger.LogError("Unable to load BlackPowder sprite from cache.");

        this.Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, unlockAtStart: true, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithIcon(icon)
            .WithSizeInInventory(new(1, 1));
        TechType = this.Info.TechType;
        Main.logger.LogDebug("Loaded BlackPowder prefab info and assigned TechType");
    }

    public void Patch()
    {
        Main.logger.LogDebug("Loading BlackPowder recipe");
        RecipeData recipe = new()
        {
            craftAmount = 1,
            Ingredients = new()
            {
                new(Coal.TechType, 1),
                new(TechType.Sulphur, 1),
                new(TechType.JeweledDiskPiece, 3)
            }
        };

        Main.logger.LogDebug("Loaded BlackPowder recipe, loading custom prefab...");

        CustomPrefab customPrefab = new(this.Info);

        customPrefab.SetGameObject(SetupGameObject());
        customPrefab.SetUnlock(Coal.TechType)
            .WithPdaGroupCategoryBefore(TechGroup.Resources, TechCategory.AdvancedMaterials, TechType.HydrochloricAcid);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(2.5f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "BasicMaterials");

        customPrefab.Register();

        Main.logger.LogDebug("Loaded and registered BlackPowder prefab.");
    }

    /// <summary>
    /// Setup the game object
    /// </summary>
    /// <returns>The modified asset prefab</returns>
    public GameObject SetupGameObject()
    {
        Main.logger.LogDebug("Setting up BlackPowder GameObject.");
        Main.logger.LogDebug("Setting shaders.");
        var renderer = AssetPrefab.EnsureComponent<MeshRenderer>();
        foreach(var mat in renderer.materials)
        {
            if (Main.AssetsCache.TryGetAsset("BlackPowder", out Texture2D albedo))
                mat.SetTexture(ShaderPropertyID._MainTex, albedo);

            if(Main.AssetsCache.TryGetAsset("BlackPowder_spec", out Texture2D speculars))
                mat.SetTexture(ShaderPropertyID._SpecTex, speculars);

            if(Main.AssetsCache.TryGetAsset("BlackPowder_normals", out Texture2D normals))
                mat.SetTexture(ShaderPropertyID._NormalsTex, normals);
        }
        MaterialUtils.ApplySNShaders(AssetPrefab);
        return AssetPrefab;
    }
}
