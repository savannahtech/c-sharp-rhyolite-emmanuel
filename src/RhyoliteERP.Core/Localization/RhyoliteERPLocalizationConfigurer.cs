using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace RhyoliteERP.Localization
{
    public static class RhyoliteERPLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(RhyoliteERPConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(RhyoliteERPLocalizationConfigurer).GetAssembly(),
                        "RhyoliteERP.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
