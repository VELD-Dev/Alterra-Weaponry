namespace VELD.AlterraWeaponry.behaviours;

public class TorpedoExplosionBehaviour : MonoBehaviour
{
    public void Start()
    {
        DamageSystem.RadiusDamage(250f, base.gameObject.transform.position, 5f, DamageType.Explosive, base.gameObject);
#if BZ
        Utils.PlayOneShotPS(GameObject.Instantiate(new VFXMeteor().gameObject).GetComponent<VFXMeteor>().impactPrefab, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
#else
        Utils.PlayOneShotPS(GameObject.Instantiate(new LavaLizard().gameObject).GetComponent<LavaLiazardRangedAttack>().attackStartFXcontrol.emitters[0].fx, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
#endif
        UnityEngine.Object.Destroy(base.gameObject);
    }

    public static GameObject detonationEffectPrefab;
}
