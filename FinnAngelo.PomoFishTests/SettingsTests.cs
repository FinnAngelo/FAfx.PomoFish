using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinnAngelo.PomoFish.Properties;

namespace FinnAngelo.PomoFishTests
{
    [TestClass]
    public class SettingsTests
    {
        [TestMethod]
        public void GivenSetting_WhenSave_ThenItChangesTheConfigFile()
        {
            //given
            Assert.IsTrue(Settings.Default.PlaySound);
            //when
            Settings.Default.PlaySound = false;
            Settings.Default.Save();
            //then
            Assert.IsFalse(Settings.Default.PlaySound);
        }

        [TestInitialize]
        public void SetupTheConfigFile()
        {
            Settings.Default.PlaySound = true;
            Settings.Default.Save();
        }

        [TestCleanup]
        public void ResetTheConfigFile()
        {
            Settings.Default.PlaySound = true;
            Settings.Default.Save();
        }
    }
}
