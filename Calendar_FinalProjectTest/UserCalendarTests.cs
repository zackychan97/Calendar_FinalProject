using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar_FinalProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json.Linq;

namespace Calendar_FinalProject.Tests
{
    [TestClass()]
    public class UserCalendarTests
    {
        [TestMethod()]
        public void AddEventTest()
        {
            var now = DateTime.Now;
            var c = new UserCalendar("Test");

            var ev = c.AddEvent("Test", now.AddDays(-1), now);

            Assert.IsInstanceOfType(ev, typeof(CalendarEvent));
            Assert.IsTrue(ev.Description == "Test");
            Assert.IsTrue(ev.EventEnd == now && ev.EventStart == now.AddDays(-1));
        }

        [TestMethod()]
        public void GetEventsTest()
        {
            var now = DateTime.Now;
            var c = new UserCalendar("Test");

            var ev = c.AddEvent("Test", now.AddDays(-1), now);
            var ev2 = c.AddEvent("Test2", now.AddDays(-2), now.AddDays(-1));

            Assert.IsTrue(c.GetEvents().Count == 2);
        }

        [TestMethod()]
        public void GetEventsInDateRangeTest()
        {
            var now = DateTime.Now;
            var c = new UserCalendar("Test");

            c.AddEvent("Test", now.AddDays(-10), now.AddDays(-9));
            c.AddEvent("Test", now.AddDays(-12), now.AddDays(-11));
            c.AddEvent("Test", now.AddDays(-10), now.AddDays(-7));
            c.AddEvent("Test", now.AddDays(-1), now);
            c.AddEvent("Test", now.AddDays(-5), now.AddDays(-4));

            Assert.IsTrue(c.GetEventsInDateRange(now.AddDays(-3), now).Count == 1);
        }

        [TestMethod()]
        public void NoEventsTest()
        {
            var c = new UserCalendar("Test");
            Assert.IsFalse(c.GetEvents().Count > 0);
        }

        [TestMethod()]
        public void GetEventsByDayTest()
        {
            var now = DateTime.Now;
            var c = new UserCalendar("Test");

            c.AddEvent("Test", now.AddDays(-10), now.AddDays(-9));
            c.AddEvent("Test", now.AddDays(-12), now.AddDays(-11));
            c.AddEvent("Test", now.AddDays(-10), now.AddDays(-7));
            c.AddEvent("Test", now.AddDays(-1), now);
            c.AddEvent("Test", now.AddDays(-5), now.AddDays(-4));
            
            Assert.IsTrue(c.GetEventsByDay(now.AddDays(-1)).Count == 1);
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            var c = new UserCalendar("Test");

            Assert.AreEqual(c.GetDescription(), "Test");
        }

        [TestMethod()]
        public void UpdateDescriptionTest()
        {
            var c = new UserCalendar("Test");
            c.UpdateDescription("NewDescription");

            Assert.AreEqual(c.GetDescription(), "NewDescription");
        }

        [TestMethod()]
        public void DisplayMonthlyViewTest()
        {
            var now = DateTime.Now;
            var c = new UserCalendar("Test");

            c.AddEvent("TestEvent", now.AddDays(-10), now.AddDays(-9));
            c.AddEvent("Hello!", now.AddDays(-12), now.AddDays(-11));

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                c.DisplayMonthlyView(2024, 4);

                Assert.IsTrue(sw.ToString().Contains("Sun Mon Tue Wed Thu Fri Sat"));
                Assert.IsTrue(sw.ToString().Contains("TestEvent"));
                Assert.IsTrue(sw.ToString().Contains("Hello!"));
            }
        }

        // Tests the different types of user input based on if they're entering a valid or 
        // invalid date based on performance requirements
        [DataTestMethod]
        [DataRow("1/24/23", "check start time", false)]
        [DataRow("01/24/2023 15:00", "check start time", true)]
        [DataRow("9/20 13:00", "check end time", false)]
        [DataRow("08/20/2024 13:00", "check end time", true)]
        [DataRow("3/3/24", "week", false)]
        [DataRow("03/03/2024", "week", true)]
        public void UserInputTest(string date, string perform, bool expected)
        {
            Assert.AreEqual(expected, Program.userDateValid(date, perform));
        }
    }
}