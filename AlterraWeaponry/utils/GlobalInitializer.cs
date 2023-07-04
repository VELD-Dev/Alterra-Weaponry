using FMOD;
using Nautilus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

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
            delay = 8f
        };

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent(Main.AWPresentationGoal.key, () =>
        {
            Main.logger.LogInfo("Played PWAPresentation goal.");
        });

        Main.AWFirstLethal = Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("AWFirstLethal", Story.GoalType.PDA, ExplosiveTorpedo.TechType, 3f);
    }

    internal static void PatchPDALogs()
    {
        // Load audio clips
        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Loading audio clips...");
        /*
        if (!Main.AssetsCache.TryGetAsset("PWAPresentation", out AudioClip AWPresentationAudioClip))
            Main.logger.LogError("PWAPresentation audio was not loaded.");

        if (!Main.AssetsCache.TryGetAsset("FirstLethalMessage", out AudioClip AWFirstLethalAudioClip))
            Main.logger.LogError("FirstLethalMessage audio was not loaded.");
        */
        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Audio clips loaded!");

        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Registering PDA Logs...");

        // Presentation PDA log "Hello xenoworker 91802..."
        if(Main.AssetsCache.TryGetAsset("PWAPresentation", out AudioClip PWAPresentation))
        {
            Main.logger.LogInfo("PWAPresentation audio message is being registered.");
            Sound sound = AudioUtils.CreateSound(PWAPresentation, MODE._2D);
            CustomSoundHandler.RegisterCustomSound(Main.AWPresentationGoal.key, sound, AudioUtils.BusPaths.PDAVoice);
            //FMODAsset fmodAsset = ScriptableObject.CreateInstance<FMODAsset>();
            //fmodAsset.path = Main.AWPresentationGoal.key;
            FMODAsset fmodAsset = AudioUtils.GetFmodAsset(Main.AWPresentationGoal.key);
            PDAHandler.AddLogEntry(
                Main.AWPresentationGoal.key,
                "Subtitles_AWPresentation",
                sound: fmodAsset,
                SpriteManager.Get(SpriteManager.Group.Log, "Pda")
            );
        }

        // First lethal weapon PDA log "A lethal weapon have been detected into your inventory..."
        if(Main.AssetsCache.TryGetAsset("FirstLethalMessage", out AudioClip AWFirstLethal))
        {
            Main.logger.LogInfo("AWFirstLethal audio message is being registered.");
            Sound sound = AudioUtils.CreateSound(AWFirstLethal, MODE.DEFAULT | MODE._2D | MODE.ACCURATETIME | MODE.CREATESTREAM);
            CustomSoundHandler.RegisterCustomSound("AWFirstLethal", sound, AudioUtils.BusPaths.PDAVoice);
            FMODAsset fmodAsset = AudioUtils.GetFmodAsset("AWFirstLethal");
            PDAHandler.AddLogEntry(
                key: "AWFirstLethal",
                languageKey: "Subtitles_AWFirstLethal",
                sound: fmodAsset,
                SpriteManager.Get(SpriteManager.Group.Log, "Pda")
            );
            Main.logger.LogInfo("AWFirstLethal registered successfully.");

        }

        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Registered PDA logs!");
    }

    internal static void PatchPDAEncyEntries()
    {
        // Register AWModInfo entry
        PDAHandler.AddEncyclopediaEntry(new()
        {
            key = "AWModInfo",
            kind = PDAEncyclopedia.EntryData.Kind.Encyclopedia,
            nodes = new[] { "Meta" },
            path = "Meta",
            unlocked = true,
        });

        // Prawn laser arm entry
        PDAHandler.AddEncyclopediaEntry(new()
        {
            key = "PrawnLaserArm",
            kind = PDAEncyclopedia.EntryData.Kind.Encyclopedia,
            nodes = new[] { "Tech", "Weaponry" },
            path = "Tech/Weaponry",
            unlocked = false,
        });
    }
}
