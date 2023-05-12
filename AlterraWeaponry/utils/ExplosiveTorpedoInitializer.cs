namespace VELD.AlterraWeaponry.Behaviours;

internal class ExplosiveTorpedoInitializer // Thanks to Grimm The Second !
{
    public static TorpedoType torpedoType { get; private set; }

    public static void InitPrefab(GameObject prefab)
    {
        bool flag = torpedoType != null;
        if(!flag)
        {
            bool flag2 = !prefab;
            if(flag2)
            {
                Main.logger.LogError("ExplosiveTorpedoBehaviour.InitPrefab() -> invalid prefab for torpedo.");
            }
            else
            {
                GameObject go = new GameObject("TorpedoExplosion", new Type[]
                {
                    typeof(TorpedoExplosionBehaviour)
                });
                ModPrefabCache.AddPrefab(go, false);
                GameObject go2 = ModPrefabCache.AddPrefabCopy(prefab, false);
                go2.GetComponent<SeamothTorpedo>().explosionPrefab = go;
                go2.GetComponent<SeamothTorpedo>().homingTorpedo = true;
                torpedoType = new()
                {
                    techType = ExplosiveTorpedo.TechType,
                    prefab = go2
                };
            }
        }
    }
}
