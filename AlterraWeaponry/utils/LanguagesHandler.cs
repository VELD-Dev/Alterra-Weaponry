namespace VELD.AlterraWeaponry.Utils
{
    namespace LocalizationHandler
    {
        [XmlRoot("LocalizationPackages")]
        public class LocalizationPackages
        {
            [XmlElement("LocalizationPackage")]
            public LocalizationPackage[] Localizations;
        }

        /// <summary>
        /// LocalizationPackage is a Localization Package ref - 
        /// </summary>
        public class LocalizationPackage
        {
            [XmlAttribute("Lang")]
            public string Lang;

            [XmlElement("Text")]
            public Text[] Texts;
        }

        public class Text
        {
            [XmlAttribute]
            public string key;
            [XmlText]
            public string value;
        }
    }

    public class LanguagesHandler
    {
        private static string ModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string filename = "Localizations.xml";
        public static void LanguagePatch()
        {
            Main.logger.LogInfo("Starting patching the languages !");
            XmlSerializer serializer = new(typeof(LocalizationHandler.LocalizationPackages));

            FileStream fs = new(Path.Combine(ModPath, filename), FileMode.Open);
            LocalizationHandler.LocalizationPackages lps;

            Main.logger.LogInfo(Language.main.GetCurrentLanguage());

            lps = (LocalizationHandler.LocalizationPackages)serializer.Deserialize(fs);

            foreach (LocalizationHandler.LocalizationPackage localizationpack in lps.Localizations)
                Main.logger.LogInfo(localizationpack.Lang);
            Main.logger.LogInfo("All LPs logged.");

            foreach (LocalizationHandler.Text text in lps.Localizations.Single(lp => lps.Localizations.Any(lp1 => lp1.Lang == Language.main.GetCurrentLanguage()) ? lp.Lang == Language.main.GetCurrentLanguage() : lp.Lang == Language.defaultLanguage).Texts)
            {
                Main.logger.LogInfo($"Checking string, key {text.key}");
                if (Language.main.Get(text.key) != null)
                {
                    LanguageHandler.SetLanguageLine(text.key, text.value);
                    Main.logger.LogInfo($"Patched key {text.key} with text '{(text.value.Length > 50 ? text.value.Substring(50) : text.value)}'");
                }
                else
                {
                    Main.logger.LogInfo($"Key {text.key} does not reference any key in game. Please check the case.");
                }
            }
            Main.logger.LogInfo("Language patching done.");
        }
    }
}
