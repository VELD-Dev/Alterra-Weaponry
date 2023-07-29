namespace VELD.AlterraWeaponry.Behaviours;

public class TorpedoExplosionBehaviour : MonoBehaviour
{
    public void Awake()
    {
    }

    public void Start()
    {
        Main.logger.LogInfo("Releasing explosion !");
        DamageSystem.RadiusDamage((250f * Main.Options.dmgMultiplier), gameObject.transform.position, 10f, DamageType.Explosive, gameObject);
#if BELOWZERO
        try
        {
            var vfxMeteor = detonationPrefab.GetComponent<VFXMeteor>();
            if (vfxMeteor.impactPrefab == null)
                throw new Exception("vfxMeteor.impactPerfab is null.");
            global::Utils.PlayOneShotPS(vfxMeteor.impactPrefab, gameObject.transform.position, gameObject.transform.rotation);
            if (vfxMeteor.meteorCrashOSSound == null)
                throw new Exception("vfxMeteor.meteorCrashOSSound is null.");
            VFXWeatherManager.PlayOneShotSound(vfxMeteor.meteorCrashOSSound, gameObject.transform.position, 8f, Array.Empty<VFXWeatherManager.FmodParameter>());
        }
        catch(Exception e)
        {
            Main.logger.LogError($"An error has occured while exploding the torpedo.\n{e}");
        }
#elif SUBNAUTICA
        global::Utils.PlayOneShotPS(GameObject.Instantiate<LavaLizard>(new LavaLizard()).GetComponent<LavaLiazardRangedAttack>().attackStartFXcontrol.emitters[0].fx, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
#endif
        Destroy(gameObject);
        Main.logger.LogInfo("Exploded !!!");
    }

    public static GameObject detonationPrefab;

    public static IEnumerator SetupDetonationPrefabAsync()
    {
        Main.logger.LogInfo($"{typeof(TorpedoExplosionBehaviour).FullName}: Setting up detonation prefab for explosive torpedo...");
        if (detonationPrefab != null)
        {
            Main.logger.LogInfo($"{typeof(TorpedoExplosionBehaviour).FullName}: vfxMeteor is already defined.");
            yield break;
        }

        GameObject prefab;

#if BZ
        prefab = VFXWeatherManager.main.meteorController.closeUpPrefab;

        if(prefab.GetComponent<VFXMeteor>() == null)
        {
            Main.logger.LogError($"{typeof(TorpedoExplosionBehaviour).FullName}: No VFXMeteor found in the closeupPrefab.");
            yield break;
        }
#else
        var task = PrefabDatabase.GetPrefabAsync("db6907f8-2c37-4d0b-8eac-1b1e3b59fa71");
        yield return task;
        task.TryGetPrefab(out prefab);
#endif
        yield return prefab;
        detonationPrefab = prefab;
        Main.logger.LogInfo($"{typeof(TorpedoExplosionBehaviour).FullName}: Detonation prefab set up.");
    }
}
