namespace VELD.AlterraWeaponry.Behaviours;

internal class ExplosiveTorpedoInitializer // Thanks to Grimm The Second !
{
    public static TorpedoType torpedoType { get; private set; }

    public static void InitPrefab(GameObject prefab)
    {
        bool flag = torpedoType != null;
        if(!flag)
        {
            if(!prefab)
            {
                Main.logger.LogError("ExplosiveTorpedoBehaviour.InitPrefab() -> invalid prefab for torpedo.");
            }
            else
            {
                GameObject go = new GameObject("TorpedoExplosion", new Type[]
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
        }
    }
}
