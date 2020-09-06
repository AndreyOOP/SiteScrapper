using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Controllers.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Services;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class LoggerSettingsProviderTests
    {
        LoggerSettingsProvider settingsProvider;

        [TestInitialize]
        public void Initialize()
        {
            settingsProvider = new LoggerSettingsProvider();
        }

        [TestMethod]
        public void Settings_DefaultValue_ExceptionLoggerSettings()
        {
            Assert.AreEqual(typeof(ExceptionLoggerSettings), settingsProvider.Settings.GetType());
        }

        [TestMethod]
        public void Update_VerboseLoggerSettings_VerboseLoggerSettings()
        {
            var verbose = new VerboseLoggerSettings();
            
            settingsProvider.Update(verbose);

            Assert.AreEqual(verbose, settingsProvider.Settings);
        }

        [TestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public void Update_InMemoryLoggerLevel_WorkerLogSettings(InMemoryLoggerLevel loggerLevel, Type expected)
        {
            settingsProvider.Update(loggerLevel);

            Assert.AreEqual(expected, settingsProvider.Settings.GetType());
        }

        static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { InMemoryLoggerLevel.VerboseLogger, typeof(VerboseLoggerSettings) };
            yield return new object[] { InMemoryLoggerLevel.VerboseParseLoggerSettings, typeof(VerboseParseLoggerSettings) };
            yield return new object[] { InMemoryLoggerLevel.ExceptionLoggerSettings, typeof(ExceptionLoggerSettings) };
        }
    }
}
