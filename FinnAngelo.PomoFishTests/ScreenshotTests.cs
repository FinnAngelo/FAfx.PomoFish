using Common.Logging;
using FAfx.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FAfx.PomoFish.Tests
{
    [TestClass]
    public class ScreenshotTests
    {
        [TestMethod]
        //[Ignore]
        public void GivenDestroyMethod_WhenGet1000Screenshots_ThenNoException()
        {
            //Given
            var moqLog = new Mock<ILog>();
            var im = new ScreenshotManager(moqLog.Object);
            var fn = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            //When
            for (int a = 0; a < 150; a++)
            {
                im.WriteScreenShot(Screen.PrimaryScreen, @"c:\temp\" + fn, a.ToString());
            }

            //Then
            Assert.IsTrue(true, "this too, shall pass...");

        }
    }
}
