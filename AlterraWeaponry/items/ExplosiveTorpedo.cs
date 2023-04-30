namespace VELD.AlterraWeaponry.items;

internal class ExplosiveTorpedo : Craftable
{
    public static GameObject prefab;
    public static TechType techType { get; private set; } = 0;

    public ExplosiveTorpedo() : base("ExplosiveTorpedo", "ExplosiveTorpedo", "Tooltip_ExplosiveTorpedo")
    {
        OnFinishedPatching += () =>
        {
            techType = TechType;
        };
    }

    public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
    public override TechType RequiredForUnlock => TechType.ExosuitTorpedoArmModule;
    public override float CraftingTime => 3f;
    public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
    public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
    public override Vector2int SizeInInventory => new(1, 1);
    public override string[] StepsToFabricatorTab => new string[] { "Upgrades", "ExosuitUpgrades" };
    public override string DiscoverMessage => "Discover_LethalWeapon";
    protected override RecipeData GetBlueprintRecipe()
    {
        return new()
        {
            craftAmount = 2,
            Ingredients = new()
            {
                new(BlackPowder.techType, 2),
                new(TechType.Titanium, 1),
            }
        };
    }
    protected override Sprite GetItemSprite()
    {
        return Main.assets.LoadAsset<Sprite>("Sprite.ExplosiveTorpedo");
    }
    public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
    {
        if (prefab == null)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.WhirlpoolTorpedo);
            yield return task;

            prefab = GameObject.Instantiate(task.GetResult());
        }

        GameObject go = GameObject.Instantiate(prefab);
        gameObject.Set(go);
    }
}
