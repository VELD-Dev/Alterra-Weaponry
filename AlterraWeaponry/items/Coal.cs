﻿using CuddleLibs.Interfaces;

namespace VELD.AlterraWeaponry.Items;

internal class Coal
{
    public static string ClassID = "Coal";
    public static TechType TechType { get; private set; } = 0;


    public PrefabInfo Info { get; private set; }

    public Coal()
    {
        if (!Main.AssetsCache.TryGetAsset("Coal", out Sprite icon))
            Main.logger.LogError("Unable to load Coal Sprite from cache.");

        this.Info = PrefabInfo
            .WithTechType(classId: ClassID, displayName: null, description: null, unlockAtStart: true, techTypeOwner: Assembly.GetExecutingAssembly())
            .WithIcon(icon)
            .WithSizeInInventory(new(1, 1));

        TechType = this.Info.TechType;
    }

    public void Patch()
    {
        Main.logger.LogDebug("Setting Coal recipe");
        RecipeData recipe = new()
        {
            craftAmount = 2,
            Ingredients =
            [
                new(TechType.CreepvinePiece, 1)
            ]
        };

        CustomPrefab customPrefab = new(this.Info);
        PrefabTemplate clone = new CloneTemplate(this.Info, TechType.Sulphur)
        {
            ModifyPrefab = (go) =>
            {
                var renderer = go.GetComponentInChildren<MeshRenderer>();
                foreach(var mat in renderer.materials)
                {
                    if (Main.AssetsCache.TryGetAsset("Coal", out Texture2D albedo))
                        mat.SetTexture(ShaderPropertyID._MainTex, albedo);

                    if (Main.AssetsCache.TryGetAsset("Coal_spec", out Texture2D specular))
                        mat.SetTexture(ShaderPropertyID._SpecTex, specular);

                    if (Main.AssetsCache.TryGetAsset("Coal_illum", out Texture2D illumination))
                        mat.SetTexture(ShaderPropertyID._Illum, illumination);
                }

                var vfxFabricating = go.EnsureComponent<VFXFabricating>();
                MaterialUtils.ApplySNShaders(go);
            } 
        };

        customPrefab.SetGameObject(clone);
        customPrefab.SetUnlock(TechType.CreepvinePiece)
            .WithPdaGroupCategoryBefore(TechGroup.Resources, TechCategory.BasicMaterials);
        customPrefab.SetEquipment(EquipmentType.None);
        customPrefab.SetRecipe(recipe)
            .WithCraftingTime(4f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "BasicMaterials");

        OutcropsUtils.EnsureOutcropDrop(
            (TechType.LimestoneChunk, TechType, 0.408f),
#if BZ
            (TechType.BreakableGold, TechType, 0.159f),
            (TechType.BreakableSilver, TechType, 0.118f)
#elif SN1
            (TechType.SandstoneChunk, TechType, 0.159f),
            (TechType.ShaleChunk, TechType, 0.118f)
#endif
        );

        customPrefab.Register();

        BaseBioReactor.charge.Add(TechType, 560f);

        Main.logger.LogDebug("Prefab loaded and registered for Coal.");
    }
}
