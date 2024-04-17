using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar_FinalProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_FinalProject.Tests
{
	[TestClass()]
	public class CalendarTests
	{
		[TestMethod()]
		public void AddEventTest()
		{
			var cal = new Calendar("Test");

			cal.AddEvent("Test", DateTime.Now, DateTime.Now);

			Assert.IsTrue(cal.GetEvents().Any());
		}
	}
}