namespace VELD.AlterraWeaponry.Utils;

internal class ExplosiveTorpedoInitializer // Thanks to Grimm The Second !
{
    public static TorpedoType torpedoType { get; private set; }

    public static void InitPrefab(GameObject prefab)
    {
        if(torpedoType == null)
        {
            if(!prefab)
            {
                Main.logger.LogError("ExplosiveTorpedoBehaviour.InitPrefab() -> invalid prefab for torpedo.");
            }
            else
            {
                try
                {
                    CoroutineHost.StartCoroutine(TorpedoExplosionBehaviour.SetupDetonationPrefabAsync());
                    Main.logger.LogInfo("Initializing TorpedoExplosionBehaviour TorpedoExplosion prefab...");
                    GameObject go = new("TorpedoExplosion", new Type[]
                    {
                        typeof(TorpedoExplosionBehaviour)
                    });
                    ModPrefabCache.AddPrefab(go);
                    prefab.GetComponent<SeamothTorpedo>().explosionPrefab = go;
                    prefab.GetComponent<SeamothTorpedo>().homingTorpedo = true;
                    torpedoType = new()
                    {
                        techType = ExplosiveTorpedo.TechType,
                        prefab = prefab
                    };
                }
                catch(Exception ex)
                {
                    Main.logger.LogError($"An error has occured while initializing torpedo prefab.\n{ex}");
                }
            }
        }
    }
}
