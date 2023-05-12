using VELD.AlterraWeaponry.Utils;

namespace VELD.AlterraWeaponry.Behaviours;

public class TorpedoExplosionBehaviour : MonoBehaviour
{
    public void Start()
    {
        DamageSystem.RadiusDamage((250f * Options.dmgMultiplier), base.gameObject.transform.position, 5f, DamageType.Explosive, base.gameObject);
#if BZ
        Utils.PlayOneShotPS(GameObject.Instantiate(new VFXMeteor().gameObject).GetComponent<VFXMeteor>().impactPrefab, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
#else
        global::Utils.PlayOneShotPS(GameObject.Instantiate(new LavaLizard().gameObject).GetComponent<LavaLiazardRangedAttack>().attackStartFXcontrol.emitters[0].fx, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
#endif
        UnityEngine.Object.Destroy(base.gameObject);
    }

    public static GameObject detonationEffectPrefab;
}
