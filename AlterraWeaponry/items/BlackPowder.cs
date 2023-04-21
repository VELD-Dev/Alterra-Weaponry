﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.U2D;

namespace VELD.AlterraWeaponry.items;

internal class BlackPowder : Craftable
{
    public static GameObject prefab;
    public static TechType techType { get; private set; } = 0;

    public BlackPowder() : base("BlackPowder", "BlackPowder", "Tooltip_BlackPowder")
    {
        OnFinishedPatching += () =>
        {
            techType = TechType;
        };
    }
    public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;
    public override TechGroup GroupForPDA => TechGroup.Resources;
    public override TechType RequiredForUnlock => Coal.techType;
    public override float CraftingTime => 2.5f;
    public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
    public override Vector2int SizeInInventory => new(1, 1);
    public override string[] StepsToFabricatorTab => new string[] { "Resources", "AdvancedMaterials"};
    protected override RecipeData GetBlueprintRecipe()
    {
        return new()
        {
            craftAmount = 1,
            Ingredients = new()
            {
                new(Coal.techType, 1),
                new(TechType.Sulphur, 1),
                new(TechType.JeweledDiskPiece, 3)
            }
        };
    }
    protected override Sprite GetItemSprite()
    {
        return Main.assets.LoadAsset<Sprite>("Coal");
    }
    public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
    {
        if (prefab == null)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.HydrochloricAcid);
            yield return task;

            prefab = GameObject.Instantiate(task.GetResult());
        }

        GameObject go = GameObject.Instantiate(prefab);
        gameObject.Set(go);
    }
}
