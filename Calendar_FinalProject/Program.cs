using Calendar_FinalProject;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;

Console.ForegroundColor = ConsoleColor.White;
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
	Console.WriteLine($"3) Display events within a range"); // Do we need this in the program? -H
    Console.WriteLine($"4) Display calendar with a year and month (numeric)");
	Console.WriteLine($"5) Display calendar in a weekly view");
    Console.WriteLine($"6) Change the descirption of an event");
    Console.WriteLine($"7) Change the date and time of an event");
    Console.WriteLine($"8) Delete an event");

    var entry = Console.ReadLine();

	switch (entry)
	{
		case "1":
			AddEvent();
			break;
		case "2":
			DisplayEvents(calendar.GetEvents());
			break;
/*		case "3":
			DisplayEventsInRange();
			break;*/
		case "4":
			DisplayMonth();
			break;
		case "5":
            DisplayWeeklyView();
            break;
        case "6":
            DisplayEvents(calendar.GetEvents());
            changeEventName(calendar.GetEvents());
            break;
        case "7":
            DisplayEvents(calendar.GetEvents());
            changeEventTime(calendar.GetEvents());
            break;
        case "8":
            DisplayEvents(calendar.GetEvents());
            DeleteEvents();
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

// Not sure if we need this function
/*void DisplayEventsInRange()
{
    Console.WriteLine($"Please enter a start date:");
    Console.WriteLine($"Please enter an end date:");

	var start = DateTime.Now.AddDays(-1); // fix me, test value, need user input
	var end = DateTime.Now; // fix me, test value, need user input

	var ev = calendar.GetEventsInDateRange(start, end);

	DisplayEvents(ev);
}*/

void AddEvent()
{
    Console.WriteLine();
    Console.WriteLine("Enter the event description: ");
    var desc = Console.ReadLine();

    DateTime startTime = setTime("start");

    DateTime endTime = setTime("end");

    var valid = false;

    while(!valid) {
        if (startTime > endTime)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid end time. Please enter an end time on or after the start time.");
            Console.ForegroundColor = ConsoleColor.White;
            endTime = setTime("end");
        }
        else
        {
            valid = true;
        }
    }

    var ev = calendar.AddEvent(desc, startTime, endTime);
    Console.WriteLine($"Your event {ev.Description} has been added!");
}

void DisplayEvents(List<CalendarEvent> events)
{
    Console.WriteLine();
    Console.WriteLine($"All events for the current calendar [{calendar.GetDescription()}]:");

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    foreach (var ev in events)
    {
        // this needs better formatting, just a sample to get started
        Console.WriteLine($"Description: {ev.Description}\nStart: {ev.EventStart}\nEnd: {ev.EventEnd}\n");
    }
    Console.ForegroundColor = ConsoleColor.White;
}


void DeleteEvents()
{
    List<CalendarEvent> events = calendar.GetEvents();
    Console.Write($"Enter name of the event you would like to delete: ");
    var name = Console.ReadLine();

    foreach (var ev in events)
    {
        if (ev.Description == name)
        {
            bool deleted = calendar.DeleteEvent(ev.Description, ev.EventStart, ev.EventEnd);
            break;
        }
    }
}

void DisplayMonth()
{
    Console.WriteLine();
    //Console.WriteLine("Enter the year of the Calendar: ");
    calendar.DisplayMonthlyView(2024, 4);
}

void DisplayWeeklyView()
{
    var valid = false;
    var startTime = "";

    DateTime start = setTime("week");

    // Creating an end time to display in the weekly view header
    DateTime end = start.AddHours(144.99);

    List<CalendarEvent> events = calendar.GetEventsInDateRange(start, end);

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine();
    Console.WriteLine($"-----------------------------------------------Weekly View----------------------------------------------");
    Console.WriteLine($"----------------------------------------{start.ToString("MM/dd/yyyy")} - {end.ToString("MM/dd/yyyy")}-----------------------------------------");

    string[] dayHeaders = { "Sun.", "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." };
    // Adjusts depending on the start date the user asked for
    int startDayIndex = ((int)start.DayOfWeek) % 7;
    // Creates the string of headers
    string dayHeaderLine = string.Join("     |     ", dayHeaders.Skip(startDayIndex).Concat(dayHeaders.Take(startDayIndex)));
    Console.WriteLine("     " + dayHeaderLine);
    Console.WriteLine("________________________________________________________________________________________________________");
    Console.ForegroundColor = ConsoleColor.White;

    List<CalendarEvent> sortedByStartTime = events.OrderBy(e => e.EventStart).ToList();

    // Loops through the events to display in a horizontal weekly format
    foreach (var ev in sortedByStartTime)
    {
        // Loops through every day
        for (int j = 0; j < 7; j++)
        {

            if ((int)ev.EventStart.DayOfWeek == ((int)start.DayOfWeek + j) % 7)
            {
                // Keeps it in a neat format where its only 14 characters long in each box
                if (ev.Description.Length >= 14)
                {
                    if (j < 6)
                    {
                        Console.Write($"{ev.Description.Substring(0, 14)}|");
                    }
                    else
                    {
                        Console.Write($"{ev.Description.Substring(0, 14)}\n");
                    }
                }
                else if(ev.Description.Length < 14)
                {
                    int spaces = 13 - ev.Description.Length;
                    if (j < 6)
                    {
                        Console.Write($"{ev.Description} {new string(' ', spaces)}|");
                    }
                    else
                    {
                        Console.Write($"{ev.Description} {new string(' ', spaces)}\n");
                    }
                }
            }
            else if (j < 6)
            {
                Console.Write($"{string.Empty,14}|");
            }
            else 
            {
                // Last element so output spaces and add newline
                // I could make this a little more compact - H
                Console.Write($"{string.Empty,14}\n");
            }
        }
    }
}

// Validates user input and returns bool
// Date var is the string the user entered 
// Start var checks start time
// End var checks end time
// Perform var checks which function we're performing on
bool userDateValid(string date, string perform)
{
    string pattern = "";

    // Validates date input for week input
    if (perform == "week")
    {
        pattern = @"^\d{2}/\d{2}/\d{4}$";
        if (Regex.IsMatch(date, pattern))
        {
            return true;
        }
    }
    else if (perform == "check start time" || perform == "check end time") // Validates for checking start and end times
    {
        // Uses the XX/XX/XXXX 00:00 format
        pattern = @"^\d{2}/\d{2}/\d{4} \d{2}:\d{2}$";

        if (Regex.IsMatch(date, pattern))
        {
            return true;
        }
    }

    // Display wrong input
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Invalid input format. Please enter the correct format.");
    Console.ForegroundColor = ConsoleColor.White;
    return false;
}

// Changes the event name
void changeEventName(List<CalendarEvent> events)
{
    var updated = false;
    Console.Write($"Enter name of the event you would like to change: ");
    var name = Console.ReadLine();

    foreach (var ev in events)
    {
        if (ev.Description == name)
        {
            Console.Write($"Event description found! Enter a new description for this event: ");
            var newname = Console.ReadLine();
            ev.Description = newname;
            Console.WriteLine($"Description updated successfully!");
            updated = true;
            break;
        }
    }
    if (!updated)
    {
        Console.WriteLine($"User description not found!");
    }
}

// Function to change the event time
void changeEventTime(List<CalendarEvent> events)
{
    // Var too keep track if the program successfully updated the time
    var updated = false;

    Console.Write($"Enter name of the event you would like to change the time and date for: ");
    var name = Console.ReadLine();

    foreach (var ev in events)
    {

        if (ev.Description == name) // Found the event in calendar
        {
            Console.WriteLine($"Event description found!");

            // Get's user input and check's user validity
            DateTime startTime = setTime("start");
            DateTime endTime = setTime("end");

            var valid = false;

            while (!valid)
            {
                if (startTime > endTime)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid end time. Please enter an end time on or after the start time.");
                    Console.ForegroundColor = ConsoleColor.White;
                    endTime = setTime("end");
                }
                else
                {
                    valid = true;
                }
            }

            // Update the event's start and end time
            ev.EventStart = startTime;
            ev.EventEnd = endTime;
            Console.WriteLine($"Date and time updated successfully!");
            updated = true;
            break;
        }
    }

    if (!updated)
    {
        Console.WriteLine($"User description not found!");
    }
}

// Gets the date from the user depending on what is being asked for
// Ex: Are we asking for a week, start time, or end time?
DateTime setTime(string perform){

    DateTime dateReturn = new DateTime();
    var valid = false;
    var start = "";
    var end = "";

    if (perform == "week")
    {
        while (!valid)
        {
            Console.Write($"Enter the week you would like to view in the XX/XX/XXXX format: ");
            start = Console.ReadLine();
            valid = userDateValid(start, "week");
        }
        
        dateReturn = DateTime.ParseExact(start, "MM/dd/yyyy", CultureInfo.InvariantCulture);

    } else if (perform == "start")
    {
        while (!valid)
        {
            Console.Write($"Enter the day and start time in the MM/DD/YYYY 00:00 format: ");
            start = Console.ReadLine();
            valid = userDateValid(start, "check start time");
        }
        
        dateReturn = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);

    } else if (perform == "end")
    {
        while (!valid)
        {
            Console.Write($"Enter the day and end time in the MM/DD/YYYY 00:00 format: ");
            end = Console.ReadLine();
            valid = userDateValid(end, "check end time");
        }
        
        dateReturn = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    return dateReturn;
}

