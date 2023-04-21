using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.U2D;

namespace VELD.AlterraWeaponry.items;

internal class BlackPowder : Craftable
{
    public static GameObject prefab = Main.assets.LoadAsset<GameObject>("GameObject.BlackPowder");
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
        return Main.assets.LoadAsset<Sprite>("Sprite.BlackPowder");
    }
    public override GameObject GetGameObject()
    {
        bool flag = !prefab;
        if (flag)
        {
            prefab = Main.assets.LoadAsset<GameObject>("GameObject.BlackPowder");
            Pickupable pickupable = prefab.AddComponent<Pickupable>();
            pickupable.overrideTechType = base.TechType;
            PrefabIdentifier prefabIdentifier = prefab.AddComponent<PrefabIdentifier>();
            prefabIdentifier.ClassId = base.ClassID;
            prefabIdentifier.name = base.ClassID;
            Rigidbody rigidbody = prefab.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.mass = 1f;
            prefab.AddComponent<WorldForces>();
            LargeWorldEntity largeWorldEntity = prefab.AddComponent<LargeWorldEntity>();
            TechTag techTag = prefab.AddComponent<TechTag>();
            techTag.type = base.TechType;
            Renderer[] componentsInChildren = prefab.GetComponentsInChildren<Renderer>();
            Shader shader = Shader.Find("MarmosetUBER");
            for(int i = 0; i < componentsInChildren.Length; i++)
            {
                for(int ind = 0; ind < componentsInChildren[i].materials.Length; ind++)
                {
                    Material material = componentsInChildren[i].materials[ind];
                    Texture texture = material.GetTexture("_SpecGlossMap");
                    Texture texture2 = material.GetTexture("_EmissionMap");
                    material.shader = shader;
                    material.DisableKeyword("_SPECGLOSSMAP");
                    material.DisableKeyword("_NORMALMAP");
                    bool flag2 = texture != null;
                    if (flag2)
                    {
                        material.SetTexture("_SpecTex", texture);
                        material.SetFloat("_SpecInt", 1f);
                        material.SetFloat("_Shininess", 4f);
                        material.EnableKeyword("MARMO_SPECMAP");
                        material.SetColor("_SpecColor", new(1f, 1f, 1f, 1f));
                        material.SetFloat("_Fresnel", 0.24f);
                        material.SetVector("_SpecTex_ST", new(1f, 1f, 0f, 0f));
                    }
                    bool flag3 = material.IsKeywordEnabled("_EMMISSION");
                    if(flag3)
                    {
                        material.EnableKeyword("MARMO_EMISSION");
                        material.SetFloat("_EnableGlow", 1f);
                        material.SetTexture("_Illum", texture2);
                        material.SetFloat("_GlowStrenght", 1f);
                        material.SetFloat("_GlowStrengthNight", 1f);
                    }
                    bool flag4 = material.GetTexture("_BumpMap");
                    if(flag4)
                    {
                        material.EnableKeyword("MARMO_NORMALMAP");
                    }
                    bool flag5 = material.name.ToLower().Contains("cutout");
                    if(flag5)
                    {
                        material.EnableKeyword("MARMO_ALPHA_CLIP");
                    }
                    bool flag6 = material.name.ToLower().Contains("transparent");
                    if(flag6)
                    {
                        material.EnableKeyword("_ZWRITE_ON");
                        material.EnableKeyword("WBOIT");
                        material.SetInt("_ZWrite", 0);
                        material.SetInt("_Cutoff", 0);
                        material.SetFloat("_SrcBlend", 1f);
                        material.SetFloat("_DstBlend", 1f);
                        material.SetFloat("_SrcBlend2", 0f);
                        material.SetFloat("_DstBlend2", 10f);
                        material.SetFloat("_AddSrcBlend", 1f);
                        material.SetFloat("_AddDstBlend", 1f);
                        material.SetFloat("_AddSrcBlend2", 0f);
                        material.SetFloat("_AddDstBlend2", 10f);
                        material.globalIlluminationFlags = (MaterialGlobalIlluminationFlags.RealtimeEmissive | MaterialGlobalIlluminationFlags.EmissiveIsBlack);
                        material.renderQueue = 3101;
                        material.enableInstancing = true;
                    }
                }
            }
            prefab.SetActive(true);
        }
        GameObject go = UnityEngine.Object.Instantiate<GameObject>(prefab);
        //go.transform.GetChild(0).localPosition = new(0, 0, 0);
        //go.transform.GetChild(0).localEulerAngles = new(-90f, 0, 0);
        return go;
    }
    public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
    {
        CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Sulphur);
        yield return task;
        GameObject go = GameObject.Instantiate(task.GetResult());

        // Don't forget to change to custom prefab or try to get the alternative prefab as fast as possible.
        gameObject.Set(go);
    }
}
