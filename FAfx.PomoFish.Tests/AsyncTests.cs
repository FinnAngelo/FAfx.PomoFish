using System;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading;

namespace FAfx.PomoFish.Tests
{
    [TestFixture]
    [Category("Async")]
    public class AsyncTests
    {
        //http://caffeineoncode.com/2012/12/async-unit-tests-in-visual-studio-2012/
        [Test]
        public async Task GivenAnAsyncUnitTest_WhenTaskRun_ThenGetResults()
        {
            //Given
            int a = 0;
            //When
            await Task.Run(() => a += 10);
            //Then
            Assert.AreEqual(a, 10);
        }

        [Test]
        //http://msdn.microsoft.com/en-us/library/vstudio/0yd65esw.aspx
        public async Task GivenABunchOfAsyncFunctions_WhenTasksRun_ThenGetResults()
        {
            //Given
            var start = DateTime.Now;
            //When
            Task allTasks = Task.WhenAll(
                SleepAsync(1),
                SleepAsync(1),
                SleepAsync(1),
                SleepAsync(1),
                SleepAsync(1)
                );
            await allTasks;
            if (allTasks.Exception != null) throw allTasks.Exception;
            //Then
            var end = DateTime.Now;
            var actual = end.Subtract(start).Seconds;
            Assert.IsTrue(actual < 2);
        }

        async Task SleepAsync(int seccondsToWait)
        {
            await Task.Delay(seccondsToWait * 1000);
        }

        [Test]
        [ExpectedException(typeof(AggregateException))]
        //http://msdn.microsoft.com/en-us/library/vstudio/0yd65esw.aspx
        public async Task GivenABunchOfAsyncFunctions_WhenExceptionTasksRun_ThenGetError()
        {
            //Given
            var start = DateTime.Now;
            //When
            Task allTasks = Task.WhenAll(
                ExceptionAsync("1"),
                ExceptionAsync("2")
                );
            try
            {
                await allTasks;
            }
            catch
            {
                if (allTasks.Exception != null) throw allTasks.Exception;
            }
            //Then
            Assert.Fail("It shall not pass!!! (there was supposed to be an exception)");
        }

        async Task ExceptionAsync(string message)
        {
            await Task.Run(() => { throw new Exception(message); });
        }

    }
}
