﻿using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Asd.AbpZeroTemplate.Localization
{
    public static class AbpZeroTemplateLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    AbpZeroTemplateConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AbpZeroTemplateLocalizationConfigurer).GetAssembly(),
                        "Asd.AbpZeroTemplate.Localization.AbpZeroTemplate"
                    )
                )
            );
        }
    }
}