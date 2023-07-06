namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(uGUI_SceneLoading))]
public class uGUISceneLoading_End_Patch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(uGUI_SceneLoading.End))]
    public static void End(uGUI_SceneLoading __instance, bool fade)
    {
        Main.logger.LogInfo("Triggered LoadingScene end.");
        //if(StoryGoalManager.main.OnGoalComplete(Main.AWPresentationGoal.key))
        Main.AWPresentationGoal.Trigger();
        Main.logger.LogInfo("Should play or have played AWPresentationGoal.");
    }
}
