namespace VELD.AlterraWeaponry.Utils;

internal class GlobalInitializer
{
    internal static string AudiosPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sounds");
    internal static void PatchGoals()
    {

        Main.AWPresentationGoal = new("PWAPresentation", Story.GoalType.PDA, 8f)
        { 
            playInCreative = true,
            playInCinematics = false,
            checkPlayerSafety = true,
            delay = 15f
        };

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent(Main.AWPresentationGoal.key, () =>
        {
            Main.logger.LogInfo("Played PWAPresentation goal.");
        });

        Main.AWFirstLethal = Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("AWFirstLethal", Story.GoalType.PDA, ExplosiveTorpedo.TechType, 3f);

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("AWFirstLethal", () =>
        {
            Main.logger.LogInfo("Revealing the WeaponsAuthorization PDA ency entry.");
            try
            {
                PDAEncyclopedia.Reveal("Ency_WeaponsAuthorization", true);
            }
            catch(Exception ex)
            {
                Main.logger.LogError(ex.ToString());
            }

            try
            {
                PDAEncyclopedia.Reveal("WeaponsAuthoriazation", true);
            }
            catch (Exception ex)
            {
                Main.logger.LogError(ex.ToString());
            }
        });
    }

    internal static void PatchPDALogs()
    {
        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Registering PDA Logs...");

        // Presentation PDA log "Hello xenoworker 91802..."
        if(Main.AssetsCache.TryGetAsset("PWAPresentation", out AudioClip PWAPresentation))
        {
            Main.logger.LogInfo("PWAPresentation audio message is being registered.");
            if(Main.Options.allowDialogs)
            {
                Sound sound = AudioUtils.CreateSound(PWAPresentation, AudioUtils.StandardSoundModes_Stream);
                CustomSoundHandler.RegisterCustomSound("PWAPresAudio", sound, AudioUtils.BusPaths.PDAVoice);
                FMODAsset fmodAsset = AudioUtils.GetFmodAsset("PWAPresAudio");
                PDAHandler.AddLogEntry(
                    Main.AWPresentationGoal.key,
                    "Subtitles_AWPresentation",
                    sound: fmodAsset,
                    SpriteManager.Get(SpriteManager.Group.Log, "Pda")
                );
            }
            else
            {
                PDAHandler.AddLogEntry(
                    Main.AWPresentationGoal.key,
                    "Subtitles_AWPresentation",
                    sound: null,
                    SpriteManager.Get(SpriteManager.Group.Log, "Pda")
                );
            }
        }

        // First lethal weapon PDA log "A lethal weapon have been detected into your inventory..."
        if(Main.AssetsCache.TryGetAsset("FirstLethalMessage", out AudioClip AWFirstLethal))
        {
            Main.logger.LogInfo("AWFirstLethal audio message is being registered.");
            if(Main.Options.allowDialogs)
            {
                Sound sound = AudioUtils.CreateSound(AWFirstLethal, AudioUtils.StandardSoundModes_Stream);
                CustomSoundHandler.RegisterCustomSound("FirstLethalMessage", sound, AudioUtils.BusPaths.PDAVoice);
                FMODAsset fmodAsset = AudioUtils.GetFmodAsset("FirstLethalMessage");
                PDAHandler.AddLogEntry(
                    key: "AWFirstLethal",
                    languageKey: "Subtitles_AWFirstLethal",
                    sound: fmodAsset,
                    SpriteManager.Get(SpriteManager.Group.Log, "Pda")
                );
            }
            else
            {
                PDAHandler.AddLogEntry(
                    key: "AWFirstLethal",
                    languageKey: "Subtitles_AWFirstLethal",
                    sound: null,
                    SpriteManager.Get(SpriteManager.Group.Log, "Pda")
                );
            }
            Main.logger.LogInfo("AWFirstLethal registered successfully.");

        }

        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Registered PDA logs!");
    }

    internal static void PatchPDAEncyEntries()
    {
        // Register AWModInfo entry
        PDAHandler.AddEncyclopediaEntry(
            "AWModInfo",
            "Meta",
            null,
            null,
            unlockSound: PDAHandler.UnlockBasic
        );

        // Prawn laser arm entry
        PDAHandler.AddEncyclopediaEntry(
            "PrawnLaserArm",
            "Tech/Weaponry",
            null,
            null,
            unlockSound: PDAHandler.UnlockBasic
        );

        PDAHandler.AddEncyclopediaEntry(
            "WeaponsAuthorization",
            "Tech/Weaponry",
            null,
            null,
            unlockSound: PDAHandler.UnlockImportant
        );
    }
}
