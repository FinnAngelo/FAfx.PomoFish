using FAfx.Utilities;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FAfx.PomoFish.Tests
{
    [TestFixture]
    public class ScreenshotTests
    {
        [SetUp]
        public void SetupBeforeEachTest()
        {
            IoC.Register<TraceSource>(null, () => new TraceSource("FAfx.PomoFish"));
        }

        [Test]
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
