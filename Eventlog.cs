namespace App;

// Defines what kind of event happened
enum EventType
{
    GuestCheckIn,
    GuestCheckOut,
    RoomUnavailable,
    RoomAvailable,
    Error
}


// This static class handles saving and showing event logs.

    static class EventLogger
    {
        private static readonly string LogFilePath = "Eventlog.txt"; // The name of the log file

        // Stores a record of the event
        public static void AddEvent(EventType type, int roomNumber, string detail = "") // Log event type, room, and a note
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Get the current time and date
            string logEntry = $"[{timestamp}] {type} | Room {roomNumber} | {detail}"; // Format the message

            try
            {
                File.AppendAllText(LogFilePath, logEntry + Environment.NewLine); // Add the message to the file
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Logger Error] Could not write to log: {ex.Message}"); // Show an error if saving the log fails
            }
        }

        // Displays the log to the user
        public static void ShowLog() // Function to read and show the whole log
        {
            Console.Clear();
            Console.WriteLine("===== EVENT LOG =====");
            try
            {
                if (File.Exists(LogFilePath)) // Check if the log file exists
                {
                    string[] lines = File.ReadAllLines(LogFilePath); // Read all lines from the log
                    if (lines.Length == 0)
                    {
                        Console.WriteLine("The event log is currently empty."); // If the file is empty
                    }
                    else
                    {
                        foreach (string line in lines) // Loop through and print each log line
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Event log file not found. Starting a new log."); // If no log file exists
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading event log: {ex.Message}"); // If reading the file fails
            }
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ReadLine();
        }
    }

