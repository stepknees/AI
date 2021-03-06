﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarSkill;
using CalendarSkill.Common;
using CalendarSkill.Models;
using CalendarSkill.ServiceClients;
using CalendarSkill.ServiceClients.GoogleAPI;
using CalendarSkill.ServiceClients.MSGraphAPI;
using CalendarSkillTest.API.Fakes;
using Microsoft.Bot.Solutions.Skills;
using Microsoft.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalendarSkillTest.API
{
    [TestClass]
    public class TimeConverterTests
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void TestInit()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void ConvertLuisLocalToUtcTest()
        {
            DateTime testTime = new DateTime(2020, 1, 1, 8, 0, 0, DateTimeKind.Local);
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime resultTime = TimeConverter.ConvertLuisLocalToUtc(testTime, timezone);

            DateTime expectTime = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(resultTime, expectTime);
        }

        [TestMethod]
        public void ConvertUtcToUserTimeTest()
        {
            DateTime testTime = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime resultTime = TimeConverter.ConvertUtcToUserTime(testTime, timezone);

            DateTime expectTime = new DateTime(2020, 1, 1, 8, 0, 0, DateTimeKind.Local);
            Assert.AreEqual(resultTime, expectTime);
        }

        [TestMethod]
        public void ConvertUtcToUserTimeTest_NotUtc_Throws()
        {
            try
            {
                DateTime testTime = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Local);
                TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
                DateTime resultTime = TimeConverter.ConvertUtcToUserTime(testTime, timezone);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Input time is not a UTC time.");
                return;
            }

            Assert.Fail("Should throw exception");
        }
    }
}
