namespace VELD.AlterraWeaponry.Patches;

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
