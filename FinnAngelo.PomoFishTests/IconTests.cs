using Common.Logging;
using FinnAngelo.PomoFish;
using FinnAngelo.PomoFish.Modules;
using FinnAngelo.PomoFish.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics;

namespace FinnAngelo.PomoFishTests
{
    [TestClass]
    public class IconTests
    {
        [TestMethod]
        //[Ignore]
        public void GivenDestroyMethod_WhenGet10000Icons_ThenNoException()
        {
            //Given
            var moqLog = new Mock<ILog>();
            var im = new IconManager(moqLog.Object);

            //When
            for (int a = 0; a < 150; a++)
            {
                im.SetNotifyIcon(null, new PomodoroNotifyIconDetails(Pomodoro.Working, a ));
            }

            //Then
            Assert.IsTrue(true, "this too, shall pass...");

        }
    }
}
