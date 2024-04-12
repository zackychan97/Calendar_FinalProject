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
	}
}
