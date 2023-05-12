namespace VELD.AlterraWeaponry.Items;

public class PrawnSelfDefenseModule
{
    public static float maxCharge = 10f;
    public static float energyCost = 5f;
    public static float cooldown = 5f;

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
        customPrefab.SetEquipment(EquipmentType.ExosuitModule);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(3f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Upgrades", "ExosuitUpgrades");

        customPrefab.Register();
    }
}
