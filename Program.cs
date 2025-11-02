//SIMPLE WORKFLOW:
// 1. Login using users.csv
// 2. Load room data from rooms.csv
// 3. Show main menu (show rooms, check in/out, mark unavailable)
// 4. Save any changes automatically
//5. Repeat until user chooses Exit


using System.Collections.Generic;


namespace App;

class HotelManager
{
    static Dictionary<string, string> users = new Dictionary<string, string>();  //Stores usernames and passwords from Users.csv. which I have 2users)
    static List<Room> rooms = new List<Room>();    // Stores all room information (number, guest, status) from Rooms.csv
    static void Main()    //“static” means it belongs to the class itself, not to an object and main is for run here first
    {
        Console.WriteLine("== Hotel Manager!==");
        LoadUsers(); // read users.csv
        if (!Login()) return;  // (early exit) If Login fails (returns false), exit the program immediately.
        Console.WriteLine("Login successful");
        LoadRooms();  //Reads rooms.csv again
        Mainmenu();   // Calls the menu
    }



    //1.Define LoadUsers that I have mentioned above for storage in csv
    static void LoadUsers()
    {
        try
        {
            string[] lines = File.ReadAllLines("Users.csv");  // for read all rows (external data)
            for (int i = 0; i < lines.Length; i++) // use loop for users
            {
                if (lines[i].Trim() == "") continue;    ////Skips empty lines — avoids errors.
                string[] parts = lines[i].Split(',');    // for splitting username and password

                if (parts.Length < 2) continue;   //Check if there are enough parts (username and password)

                string user = parts[0].Trim();     //Stores data inside the dictionary.
                string pass = parts[1].Trim();

                users[user] = pass;    // use dictionary method
            }

        }
        catch
        {
            Console.WriteLine("Error! Try to log in again");
        }
    }



    //2.Define LoadRooms I have mentioned above for storage in csv


    static void LoadRooms()    //Reads the room list from Rooms.csv.
    {
        try
        {
            rooms.Clear();   //Clears old data and loads each room line from file..
            string[] lines = File.ReadAllLines("Rooms.csv");  // for read all rows (external data)
            for (int i = 0; i < lines.Length; i++) // use loop for rooms
            {
                if (lines[i].Trim() == "") continue;        // For skipping empty line 
                string[] parts = lines[i].Split(',');       // Splits each line at the comma 

                if (parts.Length < 2) continue;             //// Ensure that it has at least room number and status,

                int number;
                if (!int.TryParse(parts[0].Trim(), out number)) continue;    //Converts the text as room number into an integer..
                string statusText = parts[1].Trim().ToLower();  //ignores uppercase/lowercase).

                // Read guest name if present, trim, and set to empty string if only whitespace
                    string guestName = parts.Length > 2 ? parts[2].Trim() : ""; 
                    if (string.IsNullOrWhiteSpace(guestName)) 
                    { 
                        guestName = ""; 
                    }
                    

                // use if-satsar because rooms has 3 status

                RoomStatus status;
                if (statusText.Contains("occupied"))            //Having guest checked in 
                    status = RoomStatus.Occupied;
                else if (statusText.Contains("unavailable"))  // no guest but room has a problem, need to be fixed
                    status = RoomStatus.Unavailable;
                else
                    status = RoomStatus.Available;              // room is free/available to book

                Room room = new Room(number, guestName, status);    //Create a new Room object with its number, guest name, status — then put it into the list called rooms
                rooms.Add(room);     // save this room to the list
            }
            Console.WriteLine("Rooms loaded successfully. Ready to manage.");
        }
        catch
        {
            Console.WriteLine("Error! Try to log in again");
        }
    }



    //3. Define User login ,use bool because login only has two outcomes: success or fail
    static bool Login()
    {
        for (int i = 0; i < 3; i++)        // gives the user 3 attempts to login
        {
            Console.Write("Username : ");                       //gets username input
            string name = Console.ReadLine()!;
            Console.Write("Password : ");                       //gets password input
            string pass = Console.ReadLine()!;

            if (users.ContainsKey(name) && users[name] == pass)   //checks if the username exists and password matches
            {
                Console.WriteLine("Welcome, " + name + "!");       //if it matches
                return true;                                       //login successful, exit the loop
            }
            else
            {
                Console.WriteLine("Wrong username or password please try again!");
            }

        }  // No more login option after user already got 3 attempts failure
        Console.WriteLine("Too many tries, You cant access anymore !");
        return false;
    }


    //4. Defines Mainmenu
    static void Mainmenu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("===== HOTEL MANAGEMENT SYSTEM =====");
            Console.WriteLine("1. Show Available Rooms (Status: Available only)");
            Console.WriteLine("2. Check In Guest (Book Room)");
            Console.WriteLine("3. Check Out Guest");
            Console.WriteLine("4. Mark Room Unavailable / Return to Service");
            Console.WriteLine("5. Show ALL Rooms (Full Status Report)");
            Console.WriteLine("6. View Event Log");
            Console.WriteLine("7. Exit");
            Console.WriteLine("=================================");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    ShowAvailableRooms();
                    break;

                case "2":
                    CheckIn();
                    break;

                case "3":
                    CheckOut();
                    break;

                case "4":
                    MarkUnavailable();
                    break;

                case "5":
                    ShowAllRooms();
                    break;

                case "6":
                    EventLogger.ShowLog();           // Option 6 shows the event log 
                    break;

                case "7":
                    SaveRooms();                      // Save all changes and exit
                    Console.WriteLine("Saving changes and exiting. Goodbye!");
                    running = false;
                    break;

                default:
                    Console.WriteLine($"Invalid option '{choice}'. Please select a number from 1-7.");
                    Console.WriteLine("Press ENTER to return to the menu..."); 
                    Console.ReadLine();
                    break;
            }
        }
    }


    //5. Defines option1 from main menu ,Show all rooms status
    static void ShowAvailableRooms()
    {
     
        Console.WriteLine("\n===== AVAILABLE ROOMS (STATUS: Available) =====");

        bool anyAvailable = false;

        foreach (Room room in rooms)
        {
            if (room.Status == RoomStatus.Available)
            {
                Console.WriteLine($"Room {room.RoomNumber} is ready for booking.");
                anyAvailable = true;
            }
        }

        if (!anyAvailable)
        {
            Console.WriteLine("No available rooms right now.");
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
           Console.Clear();
    }


    static void ShowAllRooms()
    {
        
        Console.WriteLine("\n===== FULL ROOM STATUS REPORT =====");

        if (rooms.Count > 0)
        {
            foreach (Room room in rooms)
            {
            string guestInfo = room.Status == RoomStatus.Occupied ? $" (Guest: {room.GuestName})" : "";
            Console.WriteLine($"Room {room.RoomNumber}: {room.Status}{guestInfo}");
            }
        }
        else
        {
            Console.WriteLine("No rooms found.");
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
        Console.Clear();
    }


    //6. Defines option2 -Check in a guest to an available room

    static void CheckIn()
    {
        
        Console.WriteLine("\n--- Check In Guest ---");
        Console.Write("Enter Room Number to check in: ");
        string input = Console.ReadLine()!;

        int roomNumber;
        if (!int.TryParse(input, out roomNumber))
        {
            Console.WriteLine("Invalid room number format. Please enter a number.");
            return;
        }

        // Allow roomToFind to be null if not found
        Room? roomToFind = null;

        foreach (Room room in rooms)
        {
            if (room.RoomNumber == roomNumber)
            {
                roomToFind = room;
                break;
            }
        }

        if (roomToFind == null)
        {
             Console.WriteLine($"❌ Room {roomNumber} does not exist. Please try again.");
             Console.WriteLine("Tip: Use option 5 to view all valid room numbers.");
             Console.WriteLine("Press ENTER to return to the menu...");
             Console.ReadLine();
         return;
        }

        // Properly handle unavailable and occupied
        if (roomToFind.Status == RoomStatus.Occupied)
        {
            Console.WriteLine($"❌ Sorry, Room {roomNumber} is already occupied.by '{roomToFind.GuestName}'.");
            Console.WriteLine("Tip: Use option 1 to check which rooms are available.");
            Console.WriteLine("Press ENTER to return to the menu...");
            Console.ReadLine();
            return;
        }

        if (roomToFind.Status == RoomStatus.Unavailable)
        {
            Console.WriteLine($"⚠️ Room {roomNumber} is unavailable (under maintenance).");
            Console.WriteLine("Please choose another room (use option 1).");
            Console.WriteLine("Press ENTER to return to the menu...");
            Console.ReadLine();
            return;
        }
             // only if available, continue with check-in
             Console.Write("Enter guest name: ");
             string guestName = Console.ReadLine()!;
             roomToFind.GuestName = guestName;
             roomToFind.Status = RoomStatus.Occupied;

             SaveRooms();
             EventLogger.AddEvent(EventType.GuestCheckIn, roomNumber, guestName);

         Console.WriteLine($"✅ Guest '{guestName}' checked into room {roomNumber} successfully!");
         Console.WriteLine("Press ENTER to continue...");
         Console.ReadLine();
         Console.Clear();
    }


    //7. Defines option3-check out from main menu
    static void CheckOut()
    {
      
        Console.WriteLine("\n--- Check Out Guest ---");
        Console.Write("Enter Room Number to check out: ");
        string input = Console.ReadLine()!;

        int roomNumber;
        if (!int.TryParse(input, out roomNumber))
        {
            Console.WriteLine("Invalid room number format.");
            return;
        }


        Room roomToFind = null!;
        foreach (Room room in rooms)
        {
            if (room.RoomNumber == roomNumber)
            {
                roomToFind = room;
                break;
            }
        }

        if (roomToFind == null)
        {
            Console.WriteLine($"Room {roomNumber} not found.");
            return;
        }

        if (roomToFind.Status != RoomStatus.Occupied)
        {
            Console.WriteLine($"Room {roomNumber} is not currently occupied.");
            return;
        }

        Console.WriteLine($"Guest '{roomToFind.GuestName}' has checked out of room {roomNumber}.");

        // Update status
        string guestName = roomToFind.GuestName;
        roomToFind.GuestName = "";
        roomToFind.Status = RoomStatus.Available;

        // Save and log
        SaveRooms();
        EventLogger.AddEvent(EventType.GuestCheckOut, roomNumber, guestName);


        Console.WriteLine("Check-out successful. Press ENTER to continue...");
        Console.ReadLine();
        Console.Clear();
    }



    //8. Defines option4 - Mark Room Unavailable / Return to Service
    static void MarkUnavailable()
    {
        
        Console.WriteLine("\n--- Mark Room Unavailable / Return to Service ---");
        Console.Write("Enter Room Number: ");
        string input = Console.ReadLine()!;

        if (!int.TryParse(input, out int roomNumber))
        {
            Console.WriteLine("Invalid room number format.");
            return;
        }

        Room? roomToFind = null;
        foreach (Room r in rooms)
        {
            if (r.RoomNumber == roomNumber)
            {
                roomToFind = r;
                break;
            }
        }

        if (roomToFind == null)
        {
            Console.WriteLine($"Room {roomNumber} not found.");
            return;
        }

        if (roomToFind.Status == RoomStatus.Occupied)
        {
            Console.WriteLine($"⚠️ Room {roomNumber} is occupied. You must check out the guest first.");
            Console.ReadLine();
            return;
        }

        if (roomToFind.Status == RoomStatus.Unavailable)
        {
            roomToFind.Status = RoomStatus.Available;
            Console.WriteLine($"✅ Room {roomNumber} is now AVAILABLE for booking again.");
            EventLogger.AddEvent(EventType.RoomAvailable, roomNumber);
        }
        else
        {
            roomToFind.Status = RoomStatus.Unavailable;
            Console.WriteLine($"🚧 Room {roomNumber} is now marked as UNAVAILABLE (maintenance).");
            EventLogger.AddEvent(EventType.RoomUnavailable, roomNumber);
        }

        SaveRooms();
        Console.ReadLine();
        Console.Clear();
    }



    //9. Defines SaveRooms to update and save all room data back into Rooms.csv
    static void SaveRooms()
    {
        Console.WriteLine("Saving changes to Rooms.csv...");
        try
        {
            // Prepare a list of strings to write to the file
            List<string> lines = new List<string>();
            foreach (var room in rooms)
            {
                // Format: RoomNumber,Status,GuestName
                // We will use the GuestName only if the room is occupied, otherwise it's empty.
                string guestPart = room.Status == RoomStatus.Occupied ? $",{room.GuestName}" : string.Empty;
                string line = $"{room.RoomNumber},{room.Status}{guestPart}";
                lines.Add(line);
            }
            // Write all the lines back to the file
            File.WriteAllLines("Rooms.csv", lines);
            Console.WriteLine("Room status has been sucessfully updated and saved!");
        }
        catch (Exception)
        {
            Console.WriteLine($"Error saving rooms");
        }

    }
}


        
            





    

    








