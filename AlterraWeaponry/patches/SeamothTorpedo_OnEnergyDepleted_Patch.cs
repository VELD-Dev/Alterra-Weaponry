using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.patches;

[HarmonyPatch(typeof(SeamothTorpedo))]
public class SeamothTorpedo_OnEnergyDepleted_Patch // Thanks to Grimm The Second !
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SeamothTorpedo.OnEnergyDepleted))]
    public static bool OnEnergyDepleted(SeamothTorpedo __instance)
    {
        return __instance.enabled;
    }
}
