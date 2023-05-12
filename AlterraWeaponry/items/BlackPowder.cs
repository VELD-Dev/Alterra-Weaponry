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
        SetupGameObject();
        this.Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, unlockAtStart: true, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithIcon(Main.assets.LoadAsset<Sprite>("Sprite.BlackPowder"))
            .WithSizeInInventory(new(1, 1));
        TechType = this.Info.TechType;
    }

    public void Patch()
    {
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

        CustomPrefab customPrefab = new(this.Info);


        customPrefab.SetGameObject(AssetPrefab);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(4f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "BasicMaterials");

        customPrefab.Register();
    }

    /// <summary>
    /// Setup the game object
    /// </summary>
    /// <returns>the final version of the gameobject</returns>
    public GameObject SetupGameObject()
    {
        Pickupable pickupable = AssetPrefab.GetComponent<Pickupable>();
        pickupable.overrideTechType = TechType;

        PrefabIdentifier prefabIdentifier = AssetPrefab.GetComponent<PrefabIdentifier>();
        prefabIdentifier.ClassId = ClassID;
        prefabIdentifier.name = ClassID;

        TechTag techTag = AssetPrefab.GetComponent<TechTag>();
        techTag.type = TechType;

        AssetPrefab.SetActive(true);

        GameObject go = UnityEngine.Object.Instantiate<GameObject>(AssetPrefab);
        return go;
    }
}
