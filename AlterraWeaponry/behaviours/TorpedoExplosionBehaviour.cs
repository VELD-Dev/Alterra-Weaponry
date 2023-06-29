using Nautilus;
namespace VELD.AlterraWeaponry.Behaviours;

public class TorpedoExplosionBehaviour : MonoBehaviour
{
    public void Awake()
    {
        UWE.CoroutineHost.StartCoroutine(SetupDetonationPrefabAsync());
    }

    public void Start()
    {
        DamageSystem.RadiusDamage((250f * Main.Options.dmgMultiplier), base.gameObject.transform.position, 5f, DamageType.Explosive, base.gameObject);
#if BELOWZERO
        global::Utils.PlayOneShotPS(detonationEffectPrefab, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
#elif SUBNAUTICA
        //global::Utils.PlayOneShotPS(GameObject.Instantiate<LavaLizard>(new LavaLizard()).GetComponent<LavaLiazardRangedAttack>().attackStartFXcontrol.emitters[0].fx, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
#endif
        UnityEngine.Object.Destroy(base.gameObject);
    }

    public static GameObject detonationEffectPrefab;

    public static IEnumerator SetupDetonationPrefabAsync()
    {
        var vfxMeteor = new VFXMeteor();
        var task = GameObject.Instantiate<VFXMeteor>(vfxMeteor);
        yield return task;
        var taskGo = GameObject.Instantiate(task.impactPrefab);
        yield return taskGo;
        detonationEffectPrefab = taskGo;

    }
}
