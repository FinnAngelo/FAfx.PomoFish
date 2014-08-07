using FAfx.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace FAfx.PomoFish.Tests
{
    [TestClass]
    public class IconTests
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
        public void SetupBeforeEachTest()
        {
            IoC.Register<TraceSource>(null, () => new TraceSource("FAfx.PomoFish"));
        }

        [TestMethod]
        [Ignore]
        public void GivenDestroyMethod_WhenGet10000Icons_ThenNoException()
        {
            //Given
            var im = new IconManager();

            //When
            for (int a = 0; a < 15000; a++)
            {
                im.SetNotifyIcon(null, Pomodoro.Resting, a);
            }

            //Then
            Assert.IsTrue(true, "this too, shall pass...");

        }
    }
}
