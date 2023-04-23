using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.patches;

[HarmonyPatch(typeof(GameSettings))]
public class GameSettings_SaveAsync_Patch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(GameSettings.SaveAsync))]
    public static void SaveAsync(GameSettings.OnSaveDelegate onSave)
    {
        LanguagesHandler.LanguagePatch();
    }
}
