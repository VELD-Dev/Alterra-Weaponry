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
        Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithSizeInInventory(new(1, 1))
            .WithIcon(Main.assets.LoadAsset<Sprite>("Sprite.ExplosiveTorpedo"));

        TechType = this.Info.TechType;
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

        CustomPrefab customPrefab = new(this.Info);
        CloneTemplate clone = new(this.Info, TechType.GasTorpedo);

        customPrefab.SetGameObject(clone);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(3f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Upgrades", "ExosuitUpgrades");

        customPrefab.Register();
    }
}
