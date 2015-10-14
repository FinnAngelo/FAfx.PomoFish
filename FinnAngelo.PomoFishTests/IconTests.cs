using FinnAngelo.PomoFish;
using FinnAngelo.PomoFish.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FinnAngelo.PomoFishTests
{
    [TestClass]
    public class IconTests
    {
        //[Ignore]
        [TestMethod]
        [TestCategory("NotifyIcon")]
        public void GivenDestroyMethod_WhenGet10000Icons_ThenNoException()
        {
            //Given

            //When
            for (int a = 0; a < 150; a++)
            {
                NotifyIconExtensions.SetText(null, a.ToString());
            }

            //Then
            Assert.IsTrue(true, "this too, shall pass...");

        }
    }
}
