namespace App;

// This is a list of all the different actions I want to keep track of.

enum EventType
{
    // When a guest checks in (corresponds to Main Menu Option 2)
    GuestCheckIn,

    // When a guest checks out (corresponds to Main Menu Option 3)
    GuestCheckOut,

    // When a reception marks a room as needing repair/cleaning (corresponds to Main Menu Option 4)
    RoomUnavailable
}

// This is a helper class that manages saving and showing the history (the Event Log).

static class EventLogger
{
    // This is the name of the file where all the actions will be saved.
    private static readonly string eventLogFilePath = "EventLog.txt";


    //1. This part saves an action  Guest Name (for Check In/Out) ---
    static void AddEvent(EventType action, int floorNum, int roomNum, string guestName)
    {

        // Format of logEvent
        string logEvent = $"{action}: Floor {floorNum} Room {roomNum}: Guest: {guestName}: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";


        // This command opens the log file, writes the new line of text, and then closes the file automatically.
        File.AppendAllText(eventLogFilePath, logEvent + Environment.NewLine);
    }



    // 2.This part saves an action only room number (for Mark Unavailable)---
    static void AddEvent(EventType action, int floorNum, int roomNum)
    {
        string logEvent = $"{action}: Floor {floorNum} Room {roomNum}: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

        // Save the new log entry to the file.
        File.AppendAllText(eventLogFilePath, logEvent + Environment.NewLine);
    }



    // 3. This part saves log history of the Users ---
    static void ShowLog()
    {
        Console.WriteLine("\n===== EVENT LOG HISTORY =====");

        // First, check if the file actually exists on the computer.
        if (File.Exists(eventLogFilePath))
        {
            // If it exists, read every single line from the file into an array of strings.
            string[] lines = File.ReadAllLines(eventLogFilePath);

            // Check if the file has any content inside it.
            if (lines.Length > 0)
            {
                // Go through each line one by one and print it to the screen.
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                // If the file is empty, tell the user.
                Console.WriteLine("The event log is currently empty.");
            }
        }
        else
        {
            // If the file doesn't exist yet, explain it will be created later.
            Console.WriteLine("Event log file not found. A new one will be created upon the first event.");
        }
        // Wait for the user to press Enter before returning to the main menu.
        Console.WriteLine("\nPress ENTER to return to the Main Menu...");
        Console.ReadLine();
    }

        







}       
