using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.behaviours;

public class TorpedoExplosionBehaviour : MonoBehaviour
{
    public void Start()
    {
        DamageSystem.RadiusDamage(250f, base.gameObject.transform.position, 5f, DamageType.Explosive, base.gameObject);
        Utils.PlayOneShotPS(VFXSunbeam.main.explosionPrefab, base.gameObject.transform.position, base.gameObject.transform.rotation, null);
        UnityEngine.Object.Destroy(base.gameObject);
    }

    public static GameObject detonationEffectPrefab;
}
