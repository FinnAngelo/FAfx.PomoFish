using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FAfx.PomoFish.Tests
{
    [TestFixture]
    [Category("NUnit")]
    class NUnitTests
    {
        [Test]
        public void GivenIhaveNUnitInstalled_WhenIRunTests_ThisShouldShowInTheTestRunner()
        {
            Assert.Pass();
        }

    }
}
