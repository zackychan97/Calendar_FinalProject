namespace Calendar_FinalProject
{
	internal class Calendar
	{
		/// <summary>
		/// Description of the calendar.
		/// </summary>
		private string _description { get; set; }

		/// <summary>
		/// The list of all events saved to the calendar.
		/// </summary>
		private List<CalendarEvent> _events { get; set; }

		/// <summary>
		/// Creates a new instance of a calendar.
		/// </summary>
		/// <param name="description">Description of the calendar</param>
		public Calendar(string description) 
		{ 
			_description = description ?? "CALENDAR"; // if description null, assign generic name
			_events = new List<CalendarEvent>();
		}

		/// <summary>
		/// Adds a new event to the calendar.
		/// </summary>
		/// <param name="desc">Description of event</param>
		/// <param name="start">Date and start time of event</param>
		/// <param name="end">Date and end time of event</param>
		/// <returns>The added event</returns>
		public CalendarEvent AddEvent(string desc, DateTime start, DateTime end)
		{
			try
			{
				var ev = new CalendarEvent
				{
					Description = desc,
					EventStart = start,
					EventEnd = end,
				};
				
				_events.Add(ev);
				return ev;
			}
			catch (Exception ex) 
			{
                Console.WriteLine($"There was an error: {ex.Message}");
				return new CalendarEvent();
            }
		}

		/// <summary>
		/// Gets all events of the calendar.
		/// </summary>
		/// <returns>A list of CalendarEvents</returns>
		public List<CalendarEvent> GetEvents()
		{
			return _events;
		}

		/// <summary>
		/// Gets events within a date range
		/// </summary>
		/// <param name="start">Start date</param>
		/// <param name="end">End date</param>
		/// <returns>List of calendar events</returns>
		public List<CalendarEvent> GetEventsInDateRange(DateTime start, DateTime end)
		{
			// this query will probably need to be fixed, it's a start
			return _events.Where(q => q.EventStart.Date >= start && q.EventEnd.Date <= end).ToList();
		}

		/// <summary>
		/// Gets events for a given day
		/// </summary>
		/// <param name="day"></param>
		/// <returns>List of calendar events</returns>
		public List<CalendarEvent> GetEventsByDay(DateTime day)
		{
			// this query will probably need to be fixed, it's a start
			return _events.Where(q => q.EventStart.Day >= day.Day && q.EventEnd.Day <= day.Day).ToList();
		}

		/// <summary>
		/// Gets the calendar description.
		/// </summary>
		/// <returns>The string of the calendar description</returns>
		public string GetDescription()
		{
			return _description;
		}

		/// <summary>
		/// Update the calendar description.
		/// </summary>
		/// <param name="description">New string of the description</param>
		public void UpdateDescription(string description)
		{
			_description = description ?? "CALENDAR"; // if description null, assign generic name
		}

        public void DisplayMonthlyView(int year, int month)
        {
            // Get the first day of the month
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            // Get the last day of the month
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Get the events for the month
            List<CalendarEvent> events = GetEventsInDateRange(firstDayOfMonth, lastDayOfMonth);

            // Print the month and year
            Console.WriteLine($"{firstDayOfMonth.ToString("MMMM yyyy")}\n");

            // Print the days of the week
            Console.WriteLine("Sun Mon Tue Wed Thu Fri Sat");

            // Print the days of the month
            for (int i = 0; i < (int)firstDayOfMonth.DayOfWeek; i++)
            {
                Console.Write("    ");
            }

            for (int day = 1; day <= lastDayOfMonth.Day; day++)
            {
                // Print the day
                Console.Write($"{day,3} ");

                // Check if there are any events for this day
                List<CalendarEvent> dayEvents = events.Where(e => e.EventStart.Day == day).ToList();
                if (dayEvents.Count > 0)
                {
                    // Print the events
                    foreach (CalendarEvent ev in dayEvents)
                    {
                        Console.WriteLine($"  {ev.Description}");
                    }
                }

                // Start a new line for each Sunday
                if ((day + (int)firstDayOfMonth.DayOfWeek) % 7 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
