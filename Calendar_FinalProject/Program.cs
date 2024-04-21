using Calendar_FinalProject;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Globalization;

Console.WriteLine($"Welcome to the Calendar App!!");
Console.Write($"Please enter a description for your calendar: ");

var desc = Console.ReadLine();

var calendar = new UserCalendar(desc);

var exit  = false;

while (!exit)
{
	Console.WriteLine();
	Console.WriteLine($"Please enter a selection or 'x' to quit:");
	Console.WriteLine($"1) Add an event");
	Console.WriteLine($"2) Display all events");
	Console.WriteLine($"3) Display events within a range");
    Console.WriteLine($"4) Display calendar with a year and month (numeric)");
	Console.WriteLine($"5) Display in a weekly view");


    var entry = Console.ReadLine();

	switch (entry)
	{
		case "1":
			AddEvent();
			break;
		case "2":
			DisplayEvents(calendar.GetEvents());
			break;
		case "3":
			DisplayEventsInRange();
			break;
		case "4":
			DisplayMonth();
			break;
		case "5":
			Console.WriteLine($"Enter the week you would like to view in the XX/XX/XXXX format.");
            var start = Console.ReadLine();
            // I need to add a function to ensure the user start time has the correct format - H
            DateTime startTime = DateTime.ParseExact(start, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            // Creating an end time to display in the weekly view header
            DateTime endTime = startTime.AddHours(144.99);
            DisplayWeeklyView(startTime, endTime, calendar.GetEventsInDateRange(startTime, endTime));
            break;
		case "x":
		case "X":
			exit = true; 
			break;
		default:
			Console.WriteLine($"Invalid selection, please try again!");
				break;
	}
}

void DisplayEventsInRange()
{
    Console.WriteLine($"Please enter a start date:");
    Console.WriteLine($"Please enter an end date:");

	var start = DateTime.Now.AddDays(-1); // fix me, test value, need user input
	var end = DateTime.Now; // fix me, test value, need user input

	var ev = calendar.GetEventsInDateRange(start, end);

	DisplayEvents(ev);
}


void AddEvent()
{
    Console.WriteLine();
    Console.WriteLine("Enter the event description: ");
    var desc = Console.ReadLine();
    Console.WriteLine($"Enter the day and start time in the format of MM/DD/YYYY 00:00");
    var start = Console.ReadLine();
    // I need to add a function to ensure the user start time has the correct format - H
    DateTime startTime = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
    Console.WriteLine($"Enter the day and end time in the format of MM/DD/YYYY 00:00");
    var end = Console.ReadLine();
    // I need to add a function to ensure the user start time has the correct format - H
    DateTime endTime = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
    var ev = calendar.AddEvent(desc, startTime, endTime);
    Console.WriteLine($"Your event {ev.Description} has been added!");
}

void DisplayEvents(List<CalendarEvent> events)
{
    Console.WriteLine();
    Console.WriteLine($"All events for the current calendar [{calendar.GetDescription()}]:");
    
	foreach (var ev in events)
	{
        // this needs better formatting, just a sample to get started
		Console.WriteLine($"Description: {ev.Description} Start: {ev.EventStart} End: {ev.EventEnd}");
    }
}

void DisplayMonth()
{
    Console.WriteLine();
    //Console.WriteLine("Enter the year of the Calendar: ");
	calendar.DisplayMonthlyView(2024, 4);
}

void DisplayWeeklyView(DateTime start, DateTime end, List<CalendarEvent> events)
{
	Console.WriteLine();
	Console.WriteLine($"-----------------------------------------------Weekly View----------------------------------------------");
	Console.WriteLine($"----------------------------------------{start.ToString("MM/dd/yyyy")} - {end.ToString("MM/dd/yyyy")}-----------------------------------------");

	string[] dayHeaders = { "Sun.", "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." };
	int startDayIndex = ((int)start.DayOfWeek) % 7; // Adjust to start from Sunday
	string dayHeaderLine = string.Join("     |     ", dayHeaders.Skip(startDayIndex).Concat(dayHeaders.Take(startDayIndex)));
	Console.WriteLine("     " + dayHeaderLine);
	Console.WriteLine("________________________________________________________________________________________________________");
    // Trying to display to where it's Sun. | Mon.   | Tues. | .....
    //								 Event1 | Event1 |
    //										| Event2

    List<CalendarEvent> sortedByStartTime = events.OrderBy(e => e.EventStart).ToList();

	// Loops through the events to display in weekly format
	foreach (var ev in sortedByStartTime)
	{
		for (int j = 0; j < 7; j++)
		{
			if ((int)ev.EventStart.DayOfWeek == ((int)start.DayOfWeek+j)%7)
			{
				Console.Write($"{ev.Description,14}|");
			}
			else if(j < 6)
			{
				Console.Write($"{string.Empty,14}|");
			}
			else
			{
				Console.Write($"{string.Empty,14}|\n");
			}
		}
	}
}

