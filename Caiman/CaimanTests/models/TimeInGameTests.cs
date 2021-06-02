using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caiman.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.models.Tests
{
    [TestClass()]
    public class TimeInGameTests
    {
        [TestMethod()]
        public void TimeInGameTest()
        {
            //arrange
            TimeInGame target = new TimeInGame();

            //act 
            //assert
            Assert.AreNotEqual(null, target);
        }

        [TestMethod()]
        public void TimeInGameTest1()
        {
            //arrange
            TimeInGame target = new TimeInGame(50);

            //act 
            //assert
            Assert.AreEqual(50, target.minutes);
        }
        [TestMethod()]
        public void TimeInGameProperties()
        {
            //arrange
            TimeInGame target0 = new TimeInGame();
            TimeInGame target50 = new TimeInGame(50);
            TimeInGame target60 = new TimeInGame(60);
            TimeInGame target130 = new TimeInGame(130);

            //act 
            //assert
            Assert.AreEqual("00h50", target50.TimeHoursMinutes);
            Assert.AreEqual("01h00", target60.TimeHoursMinutes);
            Assert.AreEqual("02h10", target130.TimeHoursMinutes);
            Assert.AreEqual("00h00", target0.TimeHoursMinutes);
        }
    }
}