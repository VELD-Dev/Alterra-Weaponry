namespace VELD.AlterraWeaponry;

[BepInPlugin(modGUID, modName, modVers)]
public class Main : BaseUnityPlugin
{
    // MOD INFO
    internal const string modName = "Alterra Weaponry";
    internal const string modGUID = "com.VELD.AlterraWeaponry";
    internal const string modVers = "1.0.5";

    // BepInEx/Harmony/Unity
    private static readonly Harmony harmony = new(modGUID);
    public static ManualLogSource logger;

    // STORY GOALS
    internal static StoryGoal AWPresentationGoal;
    internal static ItemGoal AWFirstLethal;

    public static readonly AssetBundle assets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "alterraweaponry.assets"));
    internal static Options Options { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

    private void Awake()
    {
        logger = Logger;
        logger.LogInfo($"{modName} {modVers} started patching.");
        harmony.PatchAll();
        logger.LogInfo($"{modName} {modVers} harmony patched.");
        LanguagesHandler.LanguagePatch();
        logger.LogInfo($"{modName} {modVers} languages lines patched.");
        Initializer.PatchPDAEncyEntries();
        logger.LogInfo($"{modName} {modVers} PDA encyclopedia entries registered.");
        //Initializer.PatchPDALogs();
        logger.LogInfo($"{modName} {modVers} PDA logs registered.");

        Coal coal = new();
        coal.Patch();

        BlackPowder blackPowder = new();
        blackPowder.Patch();

        ExplosiveTorpedo explosiveTorpedo = new();
        explosiveTorpedo.Patch();

        PrawnSelfDefenseModule prawnSelfDefenseModule = new();
        prawnSelfDefenseModule.Patch();

        logger.LogInfo($"{modName} {modVers} items registered.");
    }

    private void Update()
    {
        if(UnityInput.Current.GetKeyDown(KeyCode.P))
        {
            logger.LogInfo("Should play audio.");
            GameObject cameraObject = Camera.main.gameObject;
            AudioSource audioSource = cameraObject.GetComponent<AudioSource>();
            audioSource.clip = Main.assets.LoadAsset<AudioClip>("AudioClip.PWAPresentation");
            audioSource.Play();
            logger.LogInfo("Should have played an audio.");
        }
    }
}
