namespace VELD.AlterraWeaponry.Utils;

public class ResourcesCacheManager
{
    private static readonly List<string> ResourcesNames = new()
    {
        "AudioClip.FirstLethalMessage",
        "AudioClip.PWAPresentation",
        "GameObject.BlackPowder",
        "GameObject.CustomEventTrigger",
        "Material.BlackPowder",
        "Mesh.BlackPowder",
        "Sprite.BlackPowder",
        "Sprite.Coal",
        "Sprite.ExplosiveTorpedo",
        "Sprite.PrawnSelfDefenseModule",
        "Sprite.PrawnShieldModule",
        "Sprite.UpgradePopup",
        "Texture2D.BlackPowder",
        "Texture2D.BlackPowder_normal",
        "Texture2D.BlackPowder_spec",
        "Texture2D.Coal",
        "Texture2D.Coal_illum",
        "Texture2D.Coal_normals",
        "Texture2D.Coal_spec",
        "Texture2D.ModLogo"
    };

    private static readonly Dictionary<string, string[]> ResourcesPaths = new()
    {
        { "AudioClip", new[] { "AudioClips" } },
        { "GameObject", new[] { "GameObjects" } },
        { "Material", new[] { "Materials" } },
        { "Mesh", new[] { "Meshes" } },
        { "Sprite", new[] { "Sprites" } },
        { "Texture2D", new[] { "Textures2D" } }
    };

    public static UnityEngine.Object[] RawResources;

    public Dictionary<string, AudioClip> CachedAudioClips { get; private set; } = new();
    public Dictionary<string, GameObject> CachedPrefabs { get; private set; } = new();
    public Dictionary<string, Material> CachedMaterials { get; private set; } = new();
    public Dictionary<string, Mesh> CachedMeshes { get; private set; } = new();
    public Dictionary<string, Sprite> CachedSprites { get; private set; } = new();
    public Dictionary<string, Texture2D> CachedTextures { get; private set; } = new();
    public ResourcesCacheManager() { }

    public static ResourcesCacheManager LoadResources(string path)
    {
        Main.logger.LogInfo("Loading assetbundle...");
        var bundle = AssetBundle.LoadFromFile(path) ?? throw new IOException($"Provided path '{path}' does not contain any assetbundle.");
        RawResources = bundle.LoadAllAssets();
        var rm = new ResourcesCacheManager();

        Main.logger.LogInfo("Loaded bundle, encaching...");

        foreach (string resName in ResourcesNames)
        {
            Main.logger.LogInfo($"Trying to encache asset {resName}...");
            var assetName = resName.Split('.').Last();
            var assetType = resName.Split('.').First();
            try
            {
                UnityEngine.Object asset;
                switch(true)
                {
                    case true when assetType == "AudioClip":
                        asset = bundle.LoadAsset<AudioClip>(resName) ?? throw new KeyNotFoundException($"No resource with name {resName} exists as a {assetType} in the asset bundle.");
                        rm.CachedAudioClips.Add(assetName, asset as AudioClip);
                        Main.logger.LogInfo($"Added {resName} to rm.CachedAudioClips.");
                    break;
                    case true when assetType == "GameObject" || assetType == "Prefab":
                        asset = bundle.LoadAsset<GameObject>(resName) ?? throw new KeyNotFoundException($"No resource with name {resName} exists as a {assetType} in the asset bundle.");
                        rm.CachedPrefabs.Add(assetName, asset as GameObject);
                        Main.logger.LogInfo($"Added {resName} to rm.CachedPrefabs.");
                        break;
                    case true when assetType == "Material":
                        asset = bundle.LoadAsset<Material>(resName) ?? throw new KeyNotFoundException($"No resource with name {resName} exists as a {assetType} in the asset bundle.");
                        rm.CachedMaterials.Add(assetName, asset as Material);
                        Main.logger.LogInfo($"Added {resName} to rm.CachedMaterials.");
                        break;
                    case true when assetType == "Mesh":
                        asset = bundle.LoadAsset<Mesh>(resName) ?? throw new KeyNotFoundException($"No resource with name {resName} exists as a {assetType} in the asset bundle.");
                        rm.CachedMeshes.Add(assetName, asset as Mesh);
                        Main.logger.LogInfo($"Added {resName} to rm.CachedMeshes.");
                        break;
                    case true when assetType == "Sprite":
                        asset = bundle.LoadAsset<Sprite>(resName) ?? throw new KeyNotFoundException($"No resource with name {resName} exists as a {assetType} in the asset bundle.");
                        rm.CachedSprites.Add(assetName, asset as Sprite);
                        Main.logger.LogInfo($"Added {resName} to rm.CachedSprites.");
                        break;
                    case true when assetType == "Texture2D":
                        asset = bundle.LoadAsset<Texture2D>(resName) ?? throw new KeyNotFoundException($"No resource with name {resName} exists as a {assetType} in the asset bundle.");
                        rm.CachedTextures.Add(assetName, asset as Texture2D);
                        Main.logger.LogInfo($"Added {resName} to rm.CachedTextures.");
                        break;
                    default:
                        throw new Exception("The type of the asset is incorrect.");
                }
                Main.logger.LogInfo($"Encached {resName} to key {assetName} and in {assetType}'s cache.");
            }
            catch(Exception ex)
            {
                Main.logger.LogFatal($"A fatal error has ocurred when loading the resources.\n{ex}");
            }
        }
        var str = new StringBuilder();
        str.AppendLine("RESOURCES CACHE MANAGER: CACHED CONTENTS:");
        str.AppendLine(" ");
        str.AppendLine("rm.CachedAudioClips = [");
        foreach(var asset in rm.CachedAudioClips)
            str.AppendLine($"\t{asset}");
        str.AppendLine("]");
        str.AppendLine(" ");
        str.AppendLine("rm.CachedPrefabs = [");
        foreach(var asset in rm.CachedPrefabs)
            str.AppendLine($"\t{asset}");
        str.AppendLine("]");
        str.AppendLine(" ");
        str.AppendLine("rm.CachedMaterials = [");
        foreach (var asset in rm.CachedMaterials)
            str.AppendLine($"\t{asset}");
        str.AppendLine("]");
        str.AppendLine(" ");
        str.AppendLine("rm.CachedMeshes = [");
        foreach (var asset in rm.CachedMeshes)
            str.AppendLine($"\t{asset}");
        str.AppendLine("]");
        str.AppendLine(" ");
        str.AppendLine("rm.CachedSprites = [");
        foreach (var asset in rm.CachedSprites)
            str.AppendLine($"\t{asset}");
        str.AppendLine("]");
        str.AppendLine(" ");
        str.AppendLine("rm.CachedTextures = [");
        foreach (var asset in rm.CachedTextures)
            str.AppendLine($"\t{asset}");
        str.AppendLine("]");
        str.AppendLine(" ");
        Main.logger.LogDebug(str.ToString());
        Main.logger.LogInfo("Encached all assets.");
        return rm;
    }

    /// <summary>
    /// Gets a resource in the AssetsCache cache.
    /// <para>You better use <see cref="TryGetAsset{T}(string, out T)"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of the item to find.</typeparam>
    /// <param name="name">Name of the asset to find.</param>
    /// <returns>A reference to the instance of the asset, or an error if nothing have been found.</returns>
    /// <exception cref="ArgumentException">If the provided type is not a valid UnityEngine.Object or is not supported.</exception>
    public T GetAsset<T>(string name) where T : UnityEngine.Object
    {
        Main.logger.LogDebug($"Getting the asset {typeof(T).Name}.{name}...");
        UnityEngine.Object result = true switch
        {
            true when typeof(T) == typeof(AudioClip) => CachedAudioClips[name],
            true when typeof(T) == typeof(GameObject) => CachedPrefabs[name],
            true when typeof(T) == typeof(Material) => CachedMaterials[name],
            true when typeof(T) == typeof(Mesh) => CachedMeshes[name],
            true when typeof(T) == typeof(Sprite) => CachedSprites[name],
            true when typeof(T) == typeof(Texture2D) => CachedTextures[name],
            _ => throw new ArgumentException("The type of T is not recognized as an UnityEngine.Object class or subclass, or it is not supported."),
        };
        Main.logger.LogDebug($"Asset found: {((T)result).GetType().Name} '{name}'");

        return result as T;
    }

    /// <summary>
    /// Try to get an asset from the resourcescache.
    /// </summary>
    /// <typeparam name="T">Type of the resource to find.</typeparam>
    /// <param name="name">Name of the resource to find.</param>
    /// <param name="result">Asset found. Null if not found.</param>
    /// <returns>True if the item have been found, otherwise false.</returns>
    /// <exception cref="ArgumentException">If the provided type is not a valid UnityEngine.Object or not supported.</exception>
    public bool TryGetAsset<T>(string name, out T result) where T : UnityEngine.Object
    {
        bool res;
        switch(true)
        {
            case true when typeof(T) == typeof(AudioClip):
                res = CachedAudioClips.TryGetValue(name, out var audio);
                result = audio as T;
                return res;
            case true when typeof(T) == typeof(GameObject):
                res = CachedPrefabs.TryGetValue(name, out var gameObject);
                result = gameObject as T;
                return res;
            case true when typeof(T) == typeof(Material):
                res = CachedMaterials.TryGetValue(name, out var material);
                result = material as T;
                return res;
            case true when typeof(T) == typeof(Mesh):
                res = CachedMeshes.TryGetValue(name, out var mesh);
                result = mesh as T;
                return res;
            case true when typeof(T) == typeof(Sprite):
                res = CachedSprites.TryGetValue(name, out var sprite);
                result = sprite as T;
                return res;
            case true when typeof(T) == typeof(Texture2D):
                res = CachedTextures.TryGetValue(name, out var texture);
                result = texture as T;
                return res;
            default:
                throw new ArgumentException("Type of T is not a valid UnityEngine.Object, or is not supported.");
        }
    }
}
