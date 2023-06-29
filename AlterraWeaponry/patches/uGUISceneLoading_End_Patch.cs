namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(uGUI_SceneLoading))]
public class uGUISceneLoading_End_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(uGUI_SceneLoading.End))]
    public static void End(bool fade)
    {
        Main.logger.LogInfo("Triggered LoadingScene end.");
        //Main.AWPresentationGoal.Trigger();
        Main.logger.LogInfo("Should play or have played AWPresentationGoal.");
    }
}
