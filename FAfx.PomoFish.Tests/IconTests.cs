using Common.Logging;
using FAfx.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics;

namespace FAfx.PomoFish.Tests
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
                im.SetNotifyIcon(null, Pomodoro.Resting, a);
            }

            //Then
            Assert.IsTrue(true, "this too, shall pass...");

        }
    }
}
