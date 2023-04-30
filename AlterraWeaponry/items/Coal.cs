namespace VELD.AlterraWeaponry.items;

internal class Coal : Craftable
{
    public static GameObject prefab;
    public static TechType techType { get; private set; } = 0;

    public Coal() : base ("Coal", "Coal", "Tooltip_Coal")
    {
        OnFinishedPatching += () =>
        {
            techType = TechType;
        };
    }

    public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
    public override TechCategory CategoryForPDA => TechCategory.BasicMaterials;
    public override TechGroup GroupForPDA => TechGroup.Resources;
    public override Vector2int SizeInInventory => new(1, 1);
    public override float CraftingTime => 3f;
    public override bool UnlockedAtStart => true;
    public override string[] StepsToFabricatorTab => new string[] { "Resources", "BasicMaterials" };
    protected override RecipeData GetBlueprintRecipe()
    {
        return new()
        {
            craftAmount = 4,
            Ingredients = new()
            {
                new(TechType.CreepvinePiece, 1)
            }
        };
    }
    protected override Sprite GetItemSprite()
    {
        return Main.assets.LoadAsset<Sprite>("Sprite.Coal");
    }
    public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
    {
        if (prefab == null)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Titanium);
            yield return task;

            prefab = GameObject.Instantiate(task.GetResult());
        }

        GameObject go = GameObject.Instantiate(prefab);
        gameObject.Set(go);
    }


}
