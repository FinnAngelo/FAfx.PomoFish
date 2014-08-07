using FAfx.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FAfx.PomoFish.Tests
{
    [TestClass]
    public class ScreenshotTests
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
        public void SetupBeforeEachTest()
        {
            IoC.Register<TraceSource>(null, () => new TraceSource("FAfx.PomoFish"));
        }

        [TestMethod]
        [Ignore]
        public void GivenDestroyMethod_WhenGet1000Screenshots_ThenNoException()
        {
            //Given
            var im = new ScreenshotManager();
            var fn = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            //When
            for (int a = 0; a < 15000; a++)
            {
                im.WriteScreenShot(Screen.PrimaryScreen, @"c:\temp\"+fn, a.ToString());
            }

            //Then
            Assert.IsTrue(true, "this too, shall pass...");

        }
    }
}
