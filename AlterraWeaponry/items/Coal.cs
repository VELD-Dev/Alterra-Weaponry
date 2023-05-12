namespace VELD.AlterraWeaponry.Items;

internal class Coal
{
    public static string ClassID = "Coal";
    public static TechType TechType { get; private set; } = 0;


    public PrefabInfo Info { get; private set; }

    public Coal()
    {
        this.Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, unlockAtStart: true, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithIcon(Main.assets.LoadAsset<Sprite>("Sprite.Coal"))
            .WithSizeInInventory(new(1, 1));

        TechType = this.Info.TechType;
    }

    public void Patch()
    {
        RecipeData recipe = new()
        {
            craftAmount = 4,
            Ingredients = new()
            {
                new(TechType.CreepvinePiece, 1)
            }
        };

        CustomPrefab customPrefab = new(this.Info);
        PrefabTemplate clone = new CloneTemplate(this.Info, TechType.Nickel);

        customPrefab.SetGameObject(clone);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(4f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "BasicMaterials");

        customPrefab.Register();
    }
}
