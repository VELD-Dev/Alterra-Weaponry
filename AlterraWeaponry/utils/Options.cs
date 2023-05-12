namespace VELD.AlterraWeaponry.Utils;

[ConfigFile("aw_config")]
[Menu("Alterra Weaponry")]
public class Options : Nautilus.Json.ConfigFile
{

    [Toggle(LabelLanguageId = "Options.AW_DialogsBool", TooltipLanguageId = "Options.AW_DialogsBool.Tooltip", Order = 0)]
    public static bool allowDialogs = true;

    [Slider(Min = 0.05f, Max = 2.00f, Step = 0.05f, Format = "{0:F2}",
        LabelLanguageId = "Options.AW_dmgMultiplier", TooltipLanguageId = "Options.AW_dmgMultiplier.Tooltip", Order = 1)]
    public static float dmgMultiplier = 1.0f;

    [Keybind(LabelLanguageId = "Options.AW_KeybindPrawnSpecial", TooltipLanguageId = "Options.AW_KeybindPrawnSpecial.Tooltip", Order = 2)]
    public static KeyCode prawnActionKey = KeyCode.K;
}
