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
    internal static StoryGoal AWPresentationGoal = new("Log_PDA_Goal_AWPresentation", Story.GoalType.PDA, 0f) { playInCreative = true, playInCinematics = false, delay = 8f };


    // PATHS
    public static readonly AssetBundle assets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "alterraweaponry.assets"));


    private void Awake()
    {
        logger = Logger;
        logger.LogInfo($"{modName} {modVers} started patching.");
        harmony.PatchAll();
        //logger.LogInfo($"{modName} {modVers} harmony patched.");
        //RegisterPDALogs();
        //logger.LogInfo($"{modName} {modVers} PDA logs registered.");
        RegisterPDAEncyEntries();
        logger.LogInfo($"{modName} {modVers} PDA encyclopedia entries registered.");

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
        // Load audio clips
        logger.LogInfo($"{modName} {modVers} Loading audio clips...");
        AudioClip AWPresentationAudioClip = assets.LoadAsset<AudioClip>("pwa_presentation_message");
        AudioClip AWFirstLethalAudioClip = assets.LoadAsset<AudioClip>("first_lethal_message");
        logger.LogInfo($"{modName} {modVers} Audio clips loaded!");

        logger.LogInfo($"{modName} {modVers} Registering PDA Logs...");

        // Presentation PDA log "Hello xenoworker 91802..."
        CustomSoundHandler.RegisterCustomSound(AWPresentationGoal.key, AWPresentationAudioClip, AudioUtils.BusPaths.PDAVoice);
        FMODAsset presentation = ScriptableObject.CreateInstance<FMODAsset>();
        presentation.path = AWPresentationGoal.key;
        presentation.id = AWPresentationGoal.key;
        PDALogHandler.AddCustomEntry(
            AWPresentationGoal.key,
            "Subtitles_AWPresentation",
            sound: presentation
        );

        // First lethal weapon PDA log "A lethal weapon have been detected into your inventory..."
        CustomSoundHandler.RegisterCustomSound("Log_PDA_Goal_FirstLethal", AWFirstLethalAudioClip, AudioUtils.BusPaths.PDAVoice);
        FMODAsset firstLethal = ScriptableObject.CreateInstance<FMODAsset>();
        firstLethal.path = "Log_PDA_Goal_FirstLethal";
        firstLethal.id = "Log_PDA_Goal_FirstLethal";
        PDALogHandler.AddCustomEntry(
            "Log_PDA_Goal_FirstLethal",
            "Subtitles_AWFirstLethal",
            sound: firstLethal
        );
    }

    private static void RegisterPDAEncyEntries()
    {

    }
}
