using Nautilus.Assets.PrefabTemplates;

namespace VELD.AlterraWeaponry.Items;

internal class BlackPowder
{
    public static string ClassID = "BlackPowder";
    public static TechType TechType { get; private set; } = 0;


    public static GameObject AssetPrefab = Main.assets.LoadAsset<GameObject>("GameObject.BlackPowder");
    public PrefabInfo Info { get; private set; }

    public BlackPowder()
    {
        Main.logger.LogDebug("Loading BlackPowder prefab info");
        this.Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, unlockAtStart: true, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithIcon(Main.assets.LoadAsset<Sprite>("Sprite.BlackPowder"))
            .WithSizeInInventory(new(1, 1));
        TechType = this.Info.TechType;
        SetupGameObject();
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

        customPrefab.SetGameObject(AssetPrefab);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(4f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "BasicMaterials");

        customPrefab.Register();

        Main.logger.LogDebug("Loaded and registered BlackPowder prefab.");
    }

    /// <summary>
    /// Setup the game object
    /// </summary>
    /// <returns>the final version of the gameobject</returns>
    public GameObject SetupGameObject()
    {
        Main.logger.LogDebug("Setting up BlackPowder GameObject.");
        Main.logger.LogDebug("Setting pickupable component.");
        Pickupable pickupable = AssetPrefab.EnsureComponent<Pickupable>();
        Main.logger.LogDebug("Overriding Pickupable TechType");
        pickupable.overrideTechType = TechType;

        Main.logger.LogDebug("Set up pickupable component. Now setting PrefabIdentifier");

        PrefabIdentifier prefabIdentifier = AssetPrefab.EnsureComponent<PrefabIdentifier>();
        prefabIdentifier.ClassId = ClassID;
        prefabIdentifier.name = ClassID;

        Main.logger.LogDebug("Set up PrefabIdentifier component. Now setting TechTag");

        TechTag techTag = AssetPrefab.EnsureComponent<TechTag>();
        techTag.type = TechType;

        Main.logger.LogDebug("Set up TechTag component. Instantiating Object...");

        AssetPrefab.SetActive(true);

        GameObject go = UnityEngine.Object.Instantiate<GameObject>(AssetPrefab);
        Main.logger.LogDebug("Object correctly instantiated. Set up Black Powder correctly");
        return go;
    }
}
