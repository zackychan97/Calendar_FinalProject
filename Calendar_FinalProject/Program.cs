using Calendar_FinalProject;

Console.WriteLine($"Welcome to the Calendar App!!");
Console.Write($"Please enter a description for your calendar: ");

var desc = Console.ReadLine();

var calendar = new Calendar(desc);

var exit  = false;

while (!exit)
{
	Console.WriteLine();
	Console.WriteLine($"Please enter a selection or 'x' to quit:");
	Console.WriteLine($"1) Add an event");
	Console.WriteLine($"2) Display all events");
	Console.WriteLine($"3) Display events within a range");
    Console.WriteLine($"4) Display calendar with a year and month (numeric)");


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
	Console.WriteLine($"Enter the start date/time in xxxxx format"); //fix me, needs proper date time formatting 
	
	var start = DateTime.Now.AddDays(-2); // fix me, test value, need user input
	var end = DateTime.Now.AddDays(-1); // fix me, test value, need user input

	var ev = calendar.AddEvent(desc, start, end);

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

