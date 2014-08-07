using FAfx.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FAfx.PomoFish.Tests
{
    [TestClass]
    public class IoCTests
    {
        [TestCleanup()]
        public void CleanupContainer()
        {
            IoC.Clear();
        }

        #region Register Object
        [TestMethod]
        [TestCategory("Register Object")]
        public void GivenASingleRegisteredObject_WhenResolve_ThenGetTheObjectBack()
        {
            //given
            IoC.Register<IMessager>(new HelloMessager());
            //when
            var m = IoC.Resolve<IMessager>();
            //then
            Assert.AreEqual("Hello World", m.GetMessage("World"));
        }

        [TestMethod]
        [TestCategory("Register Object")]
        public void GivenMultipleRegisteredSameObjectType_WhenRegister_ThenGetSecondRegisteredObject()
        {
            //given
            IoC.Register<IMessager>(new HelloMessager());
            //when
            IoC.Register<IMessager>(new HowdyMessager());
            var actual = IoC.Resolve<IMessager>();
            //then
            Assert.AreEqual(typeof(HowdyMessager), actual.GetType());
        }

        [TestMethod]
        [TestCategory("Register Object")]
        public void GivenMultipleRegisteredDifferentObjectType_WhenRegister_ThenGetNoError()
        {
            //given
            IoC.Register<Int32>(Int32.MaxValue);
            IoC.Register<IMessager>(new HowdyMessager());
            //then
            Assert.IsTrue(true, "No exception!");
        }

        [TestMethod]
        [TestCategory("Register Object")]
        public void GivenNoRegisteredObjectType_WhenResolve_ThenGetError()
        {
            //given
            string exMessage = null;
            //when
            try
            {
                var actual = IoC.Resolve<IMessager>();
            }
            catch (KeyNotFoundException ex)
            {
                exMessage = ex.Message;
            }
            //then
            Assert.AreEqual("The given key was not present in the dictionary.", exMessage);

        }

        #endregion

        #region Register Func

        [TestMethod]
        [TestCategory("Register Func")]
        public void GivenASingleRegisteredFunc_WhenResolve_ThenGetTheObjectBack()
        {
            //given
            IoC.Register<IMessager>(() => { return new HelloMessager(); });
            //IoC.Register<IMessager>(() => { return 123; });
            //when
            var m = IoC.Resolve<IMessager>();
            //then
            Assert.AreEqual("Hello World", m.GetMessage("World"));
        }

        #endregion

        #region Register Named Func

        [TestMethod]
        [TestCategory("Register Named Func")]
        public void GivenARegisteredNamedFunction_WhenResolve_ThenGetTheObjectBack()
        {
            //given
            IoC.Register<IMessager>("HidihoMessenger", () => { return new GenericMessager("Hidiho"); });
            //when
            var m = IoC.Resolve<IMessager>("HidihoMessenger");
            //then
            Assert.AreEqual("Hidiho World", m.GetMessage("World"));
        }
        [TestMethod]
        [TestCategory("Register Named Func")]
        public void GivenARegisteredNullNamedFunction_WhenResolve_ThenGetTheObjectBack()
        {
            //given
            IoC.Register<IMessager>(null, () => { return new GenericMessager("Hidiho"); });
            //when
            var m = IoC.Resolve<IMessager>();
            //then
            Assert.AreEqual("Hidiho World", m.GetMessage("World"));
        }

        [TestMethod]
        [TestCategory("Register Named Func")]
        public void GivenARegisteredNamedObject_WhenResolve_ThenGetTheObjectBack()
        {
            //given
            IoC.Register<IMessager>("HidihoMessenger", new GenericMessager("Hidiho"));
            //when
            var m = IoC.Resolve<IMessager>("HidihoMessenger");
            //then
            Assert.AreEqual("Hidiho World", m.GetMessage("World"));
        }

        [TestMethod]
        [TestCategory("Register Named Func")]
        public void GivenARegisteredNullNamedObject_WhenResolve_ThenGetTheObjectBack()
        {
            //given
            IoC.Register<IMessager>(null, new GenericMessager("Hidiho"));
            //when
            var m = IoC.Resolve<IMessager>();
            //then
            Assert.AreEqual("Hidiho World", m.GetMessage("World"));
        }



        [TestMethod]
        [TestCategory("Register Named Func")]
        public void GivenMultipleNamedRegisteredFunction_WhenResolve_ThenGetTheObjectsBack()
        {
            //given
            IoC.Register<IMessager>("HidihoMessenger", () => { return new GenericMessager("Hidiho"); });
            IoC.Register<IMessager>("HowdyHoMessenger", () => { return new GenericMessager("HowdyHo"); });
            //when
            var m = IoC.Resolve<IMessager>("HidihoMessenger");
            var h = IoC.Resolve<IMessager>("HowdyHoMessenger");
            //then
            Assert.AreEqual("Hidiho World", m.GetMessage("World"));
            Assert.AreEqual("HowdyHo World", h.GetMessage("World"));
        }

        #endregion

        #region Register Func with ResolveFunc
        [TestMethod]
        [TestCategory("Register Func with ResolveFunc")]
        public void GivenRegisteredCountingMessagerObject_AndIncrementCountyThing_WhenResolve_ThenCountingMessagerWasCreatedBeforeCountyThingIsIncremented()
        {
            //given
            IoC.Register<IMessager>(new CountingMessager());
            //and
            CountyThing.Count++;
            //when
            var m = IoC.Resolve<IMessager>();
            //then
            Assert.AreEqual("1 World", m.GetMessage("World"));
        }

        [TestMethod]
        [TestCategory("Register Func with ResolveFunc")]
        public void GivenRegisteredFuncToResolveAtRegister_AndIncrementCountyThing_WhenResolve_ThenCountingMessagerWasCreatedBeforeCountyThingIsIncremented()
        {
            //given
            IoC.Register<IMessager>(toRegister: () => { return new CountingMessager(); }, resolveFunc: ResolveFunc.AtRegister);
            //and
            CountyThing.Count++;
            //when
            var m = IoC.Resolve<IMessager>();
            //then
            Assert.AreEqual("1 World", m.GetMessage("World"));
        }

        [TestMethod]
        [TestCategory("Register Func with ResolveFunc")]
        public void GivenRegisteredFuncToResolveAtFirstCallAndIncrement_WhenResolveAndIncrement_ThenCountingMessagerWasCreatedAfterCountyThingIsFirstIncremented()
        {
            //given
            IoC.Register<IMessager>(toRegister: () => { return new CountingMessager(); }, resolveFunc: ResolveFunc.AtFirstCall);
            //and
            CountyThing.Count = CountyThing.Count + 3;
            //when
            var m = IoC.Resolve<IMessager>();
            //and
            CountyThing.Count = CountyThing.Count + 5;
            //then
            Assert.AreEqual("5 World", m.GetMessage("World"));
            //and
            CountyThing.Count = CountyThing.Count + 7;
            //then
            Assert.AreEqual("12 World", m.GetMessage("World"));
        }

        [TestMethod]
        [TestCategory("Register Func with ResolveFunc")]
        public void GivenRegisteredFuncToResolveEveryTime_WhenResolve_ThenCountingMessagerIsCreatedEveryTimeResolved()
        {
            //given
            IoC.Register<IMessager>(toRegister: () => { return new CountingMessager(); }, resolveFunc: ResolveFunc.EveryTime);
            //and
            CountyThing.Count = CountyThing.Count + 3;
            //when
            var m = IoC.Resolve<IMessager>();
            //and
            CountyThing.Count = CountyThing.Count + 5;
            //then
            Assert.AreEqual("5 World", m.GetMessage("World"));
            //and
            m = IoC.Resolve<IMessager>();
            CountyThing.Count = CountyThing.Count + 7;
            //then
            Assert.AreEqual("7 World", m.GetMessage("World"));
        }


        #endregion
    }

    public interface IMessager
    {
        string GetMessage(string msg);
    }

    public class HelloMessager : IMessager
    {
        public string GetMessage(string msg)
        {
            return "Hello " + msg;
        }
    }
    public class HowdyMessager : IMessager
    {
        public string GetMessage(string msg)
        {
            return "Howdy " + msg;
        }
    }

    public class GenericMessager : IMessager
    {
        private string _greeting = null;

        public GenericMessager(string greeting)
        {
            this._greeting = greeting;
        }

        public string GetMessage(string msg)
        {
            return this._greeting + " " + msg;
        }
    }

    internal static class CountyThing
    {
        internal static int Count { get; set; }
    }
    public class CountingMessager : IMessager
    {
        public CountingMessager()
        {
            CountyThing.Count = 0;
        }

        public string GetMessage(string msg)
        {
            return CountyThing.Count.ToString() + " " + msg;
        }
    }
}
