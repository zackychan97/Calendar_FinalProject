namespace Calendar_FinalProject
{
	public class DateTimeRangeModel
	{
		/// <summary>
		/// Start date/time of an event
		/// </summary>
		public DateTime Start { get; set; }

		/// <summary>
		/// End date/time of an event
		/// </summary>
		public DateTime End { get; set; }

		/// <summary>
		/// Total time of an event
		/// </summary>
		public TimeSpan EimeSpan
		{
			get
			{
				return End - Start;
			}
		}

		/// <summary>
		/// An object used to represent 
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public DateTimeRangeModel(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
		}
	}
}
