using VELD.AlterraWeaponry.utils;

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

    public static ResourcesManager resources { get; private set; }

    internal static Options Options { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

    private void Awake()
    {
        logger = Logger;
        try
        {
            resources = ResourcesManager.LoadResources(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "alterraweaponry.assets"));
        }
        catch (Exception ex)
        {
            logger.LogFatal($"Fatal error occured: Unable to load resources to cache.\n{ex}");
        }
        logger.LogInfo($"{modName} {modVers} started patching.");
        harmony.PatchAll();
        logger.LogInfo($"{modName} {modVers} harmony patched.");
        LanguagesHandler.LanguagePatch();
        logger.LogInfo($"{modName} {modVers} languages lines patched.");
        Initializer.PatchPDAEncyEntries();
        logger.LogInfo($"{modName} {modVers} PDA encyclopedia entries registered.");
        Initializer.PatchGoals();
        logger.LogInfo($"{modName} {modVers} PDA goals initialized.");
        Initializer.PatchPDALogs();
        logger.LogInfo($"{modName} {modVers} PDA logs registered.");

        ModDatabankHandler.RegisterMod(new ModDatabankHandler.ModData()
        {
            guid = modGUID,
            version = modVers,
            image = resources.GetAsset<Texture2D>("ModLogo"),
            name = "Alterra Weaponry",
            desc = "Since the return of the Aurora survivor, Alterra secretely added a few weapons blueprints.\nThis information is kept confidential, by using the PWA (Personal Weaponry Assistance) you agree the Alterra's NDA (Non-Divulgation Accord)."
        });

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
            logger.LogInfo("CameraObject defined.");
            AudioSource audioSource = cameraObject.GetComponent<AudioSource>();
            logger.LogInfo("audioSource defined.");
            if (!resources.TryGetAsset("PWAPresentation", out AudioClip clip))
                logger.LogError("Audio was not able to load from cache.");
            audioSource.clip = clip;
            logger.LogInfo("AudioClip defined.");
            try
            {
                audioSource.Play();
                logger.LogInfo("Should have played an audio.");
            }
            catch(Exception e)
            {
                logger.LogError(e);
            }
        }
    }
}
