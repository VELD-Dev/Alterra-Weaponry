using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace VELD.AlterraWeaponry.Utils;

internal class Initializer
{
    internal static void PatchGoals()
    {
        Main.AWPresentationGoal = new("AWPresentation", Story.GoalType.PDA, 8f)
        { 
            playInCreative = true,
            playInCinematics = false,
            delay = 8f
        };

        Main.AWFirstLethal = null;
    }

    internal static void PatchPDALogs()
    {
        // Load audio clips
        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Loading audio clips...");
        AudioClip AWPresentationAudioClip = Main.assets.LoadAsset<AudioClip>("AudioClip.PWAPresentation");
        AudioClip AWFirstLethalAudioClip = Main.assets.LoadAsset<AudioClip>("AudioClip.FirstLethalMessage");
        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Audio clips loaded!");

        Main.logger.LogInfo($"{Main.modName} {Main.modVers} Registering PDA Logs...");

        // Presentation PDA log "Hello xenoworker 91802..."
        CustomSoundHandler.RegisterCustomSound(Main.AWPresentationGoal.key, AWPresentationAudioClip, AudioUtils.BusPaths.PDAVoice);
        FMODAsset presentation = AudioUtils.GetFmodAsset(Main.AWPresentationGoal.key);
        PDAHandler.AddLogEntry(
            Main.AWPresentationGoal.key,
            "Subtitles_AWPresentation",
            sound: presentation
        );

        // First lethal weapon PDA log "A lethal weapon have been detected into your inventory..."
        CustomSoundHandler.RegisterCustomSound("AWFirstLethal", AWFirstLethalAudioClip, AudioUtils.BusPaths.PDAVoice);
        FMODAsset firstLethal = AudioUtils.GetFmodAsset("AWFirstLethal");
        PDAHandler.AddLogEntry(
            "AWFirstLethal",
            "Subtitles_AWFirstLethal",
            sound: firstLethal
        );
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

        // Explosive torpedoes entry
        PDAHandler.AddEncyclopediaEntry(new()
        {
            key = "ExplosiveTorpedo",
            kind = PDAEncyclopedia.EntryData.Kind.Encyclopedia,
            nodes = new[] { "Tech", "Weaponry" },
            path = "Tech/Weaponry",
            unlocked = false,
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

        // Prawn Self Defense Module
        PDAHandler.AddEncyclopediaEntry(new()
        {
            key = "PrawnDefensePerimeter",
            kind = PDAEncyclopedia.EntryData.Kind.Encyclopedia,
            nodes = new[] { "Tech", "Weaponry", "Modules" },
            path = "Tech/Weaponry/Modules",
            unlocked = false,
        });
    }
}
