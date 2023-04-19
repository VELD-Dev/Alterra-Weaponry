﻿using VELD.AlterraWeaponry.items;

namespace VELD.AlterraWeaponry;

[BepInPlugin(modGUID, modName, modVers)]
public class Main : BaseUnityPlugin
{
    // MOD INFO
    private const string modName = "Alterra Weaponry";
    private const string modGUID = "com.VELD.AlterraWeaponry";
    private const string modVers = "1.0.3";

    // BepInEx/Harmony/Unity
    private static readonly Harmony harmony = new(modGUID);
    public static ManualLogSource logger;


    // STORY GOALS
    internal static StoryGoal AWPresentationGoal = new("Goal_AWPresentation", Story.GoalType.PDA, 8f);


    // PATHS
    public static readonly string AssetsLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets");


    private void Awake()
    {
        logger = Logger;
        logger.LogInfo($"{modName} {modVers} started patching.");
        harmony.PatchAll();
        logger.LogInfo($"{modName} {modVers} harmony patched.");
        RegisterPDALogs();
        logger.LogInfo($"{modName} {modVers} PDA logs registered.");

        Coal coal = new();
        BlackPowder blackPowder = new();
        ExplosiveTorpedo explosiveTorpedo = new();
        coal.Patch();
        blackPowder.Patch();
        explosiveTorpedo.Patch();
        logger.LogInfo($"{modName} {modVers} items registered.");

        LanguagesHandler.LanguagePatch();
        logger.LogInfo($"{modName} {modVers} languages lines patched.");

    }
    private static void RegisterPDALogs()
    {
        // Presentation PDA log "Hello xenoworker 91802..."
        CustomSoundHandler.RegisterCustomSound("Goal_AWPresentation", Path.Combine(AssetsLocation, "xenoworx_pda_presentation.mp3"), AudioUtils.BusPaths.PDAVoice);
        FMODAsset presentation = ScriptableObject.CreateInstance<FMODAsset>();
        presentation.path = "Goal_AWPresentation";
        PDALogHandler.AddCustomEntry(
            AWPresentationGoal.key,
            "Subtitles_AWPresentation",
            sound: presentation
        );

        // First lethal weapon PDA log "A lethal weapon have been detected into your inventory..."
        CustomSoundHandler.RegisterCustomSound("Goal_FirstLethal", Path.Combine(AssetsLocation, "first_lethal_weapon_message.ogg"), AudioUtils.BusPaths.PDAVoice);
        FMODAsset firstLethal = ScriptableObject.CreateInstance<FMODAsset>();
        firstLethal.path = "Goal_FirstLethal";
        PDALogHandler.AddCustomEntry(
            "Goal_FirstLethal",
            "Subtitles_AWFirstLethal",
            sound: firstLethal
        );
    }
}
