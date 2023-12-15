namespace VELD.AlterraWeaponry;

[BepInPlugin(modGUID, modName, modVers)]
public class Main : BaseUnityPlugin
{
    // MOD INFO
    internal const string modName = "Alterra Weaponry";
    internal const string modGUID = "com.VELD.AlterraWeaponry";
    internal const string modVers = "1.0.5";
    internal const string modLongVers = "1.0.5.2";

    // BepInEx/Harmony/Unity
    private static readonly Harmony harmony = new(modGUID);
    public static ManualLogSource logger;

    // STORY GOALS
    internal static StoryGoal AWPresentationGoal;
    internal static ItemGoal AWFirstLethal;

    public static ResourcesCacheManager AssetsCache { get; private set; }

    internal static Options Options { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

    private void Awake()
    {
        logger = Logger;
        try
        {
#if BZ
            AssetsCache = ResourcesCacheManager.LoadResources(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "bz.alterraweaponry.assets"));
#else
            AssetsCache = ResourcesCacheManager.LoadResources(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "sn.alterraweaponry.assets"));
#endif
        }
        catch (Exception ex)
        {
            logger.LogFatal($"Fatal error occured: Unable to load resources to cache.\n{ex}");
        }
    }

    private void Start()
    {
        logger.LogInfo($"{modName} {modVers} started patching.");
        harmony.PatchAll();
        logger.LogInfo($"{modName} {modVers} harmony patched.");
        LanguagesHandler.GlobalPatch();
        logger.LogInfo($"{modName} {modVers} languages lines patched.");

// These features are reserved to Below Zero, due to the lore of the game.
#if BZ
        GlobalInitializer.PatchPDAEncyEntries();
        logger.LogInfo($"{modName} {modVers} PDA encyclopedia entries registered.");
        GlobalInitializer.PatchGoals();
        logger.LogInfo($"{modName} {modVers} PDA goals initialized.");
        //CoroutineHost.StartCoroutine(GlobalInitializer.PatchPDALogs());
        GlobalInitializer.PatchPDALogs();
        logger.LogInfo($"{modName} {modVers} Started registering PDA logs.");
#endif

        ModDatabankHandler.RegisterMod(new ModDatabankHandler.ModData()
        {
            guid = modGUID,
            version = modVers,
            image = AssetsCache.GetAsset<Texture2D>("ModLogo"),
            name = "Alterra Weaponry",
#if BZ
            desc = "Since the return of the Aurora survivor, Alterra secretely added a few weapons blueprints.\nThis information is kept confidential, by using the PWA (Personal Weaponry Assistance) you agree the Alterra's NDA (Non-Divulgation Accord)."
#else
            desc = "Ever wanted to bust some asses in Subnautica? You got the right mod!\n\nThis mod is made to run on Below Zero. Because of that, some features will not be available on Subnautica, for example PDA voicelines, encyclopedia entries, and everything related to lore."
#endif
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

}
