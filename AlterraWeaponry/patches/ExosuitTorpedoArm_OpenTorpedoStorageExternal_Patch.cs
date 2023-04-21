using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.patches;

[HarmonyPatch(typeof(ExosuitTorpedoArm))]
public class ExosuitTorpedoArm_OpenTorpedoStorageExternal_Patch // Thanks to Grimm The Second !
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(ExosuitTorpedoArm.OnOpenTorpedoStorage))]
    public static void OpenTorpedoStorageExternal(ExosuitTorpedoArm __instance)
    {
        __instance.container.allowedTech.AddRange(new TechType[]
        {
            ExplosiveTorpedo.techType
        });
    }
}
