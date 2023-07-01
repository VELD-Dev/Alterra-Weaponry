using Nautilus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace VELD.AlterraWeaponry.Utils;

internal class Initializer
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
        if (!Main.resources.TryGetAsset("PWAPresentation", out AudioClip AWPresentationAudioClip))
            Main.logger.LogError("PWAPresentation audio was not loaded.");

        if (!Main.resources.TryGetAsset("FirstLethalMessage", out AudioClip AWFirstLethalAudioClip))
            Main.logger.LogError("FirstLethalMessage audio was not loaded.");
        */
        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Audio clips loaded!");

        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Registering PDA Logs...");

        // Presentation PDA log "Hello xenoworker 91802..."
        CustomSoundHandler.RegisterCustomSound(Main.AWPresentationGoal.key, Path.Combine(AudiosPath, "AudioClip.PWAPresentation.ogg"), AudioUtils.BusPaths.PDAVoice);
        FMODAsset presentation = ScriptableObject.CreateInstance<FMODAsset>();
        presentation.path = Main.AWPresentationGoal.key;
        //FMODAsset presentation = AudioUtils.GetFmodAsset(Main.AWPresentationGoal.key);
        PDAHandler.AddLogEntry(
            Main.AWPresentationGoal.key,
            "Subtitles_AWPresentation",
            sound: presentation
        );

        // First lethal weapon PDA log "A lethal weapon have been detected into your inventory..."
        CustomSoundHandler.RegisterCustomSound("AWFirstLethal", Path.Combine(AudiosPath, "AudioClip.FirstLethalMessage.ogg"), AudioUtils.BusPaths.PDAVoice);
        FMODAsset firstLethal = AudioUtils.GetFmodAsset("AWFirstLethal");
        PDAHandler.AddLogEntry(
            "AWFirstLethal",
            "Subtitles_AWFirstLethal",
            sound: firstLethal
        );

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
