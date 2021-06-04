using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caiman.logique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Caiman.logique.Tests
{
    [TestClass()]
    public class GameTimerTests
    {


        [TestMethod()]
        public void GameTimerTest1()
        {
            //arrange
            GameTimer target = new GameTimer();

            //act 
            //assert
            Assert.AreNotEqual(null, target);

        }



        [TestMethod()]
        public void UpdateTimerTest()
        {
            //arrange

            GameTimer target50s = new GameTimer();
            target50s.counter = 49;

            GameTimer target11m48s = new GameTimer();
            target11m48s.counter = 47;
            target11m48s.minutes = 11;
            //act

            target50s.UpdateTimer(new object(), new EventArgs());
            target11m48s.UpdateTimer(new object(),new EventArgs());
            //assert

            Assert.AreEqual("00 min 50 s", target50s.ToString());
            Assert.AreEqual("11 min 48 s", target11m48s.ToString());
        }

        [TestMethod()]
        public void TimeInGameTest()
        {
            //arrange
            GameTimer target10m = new GameTimer();
            target10m.minutes = 10;

            GameTimer target59s = new GameTimer();
            target59s.counter = 59;

            GameTimer target50s = new GameTimer();
            target50s.counter = 50;

            GameTimer target11m48s = new GameTimer();
            target11m48s.counter = 48;
            target11m48s.minutes = 11;
            //act

            //assert
            Assert.AreEqual("10 min 00 s", target10m.ToString());
            Assert.AreEqual("00 min 59 s", target59s.ToString());
            Assert.AreEqual("00 min 50 s", target50s.ToString());
            Assert.AreEqual("11 min 48 s", target11m48s.ToString());
        }
    }
}