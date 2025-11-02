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
        Console.WriteLine("🔑Login successful");
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


    static void LoadRooms()
    {
        try
        {
            rooms.Clear();
            string[] lines = File.ReadAllLines("Rooms.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] parts = lines[i].Split(',');
                if (parts.Length < 2) continue;

                // parse number
                if (!int.TryParse(parts[0].Trim(), out int number)) continue;

                // read status text and guest if present
                string statusText = parts[1].Trim().ToLower();
                string guestName = parts.Length > 2 ? parts[2].Trim() : "";
                if (string.IsNullOrWhiteSpace(guestName)) guestName = "";

                RoomStatus status;
                if (statusText.Contains("occupied"))
                    status = RoomStatus.Occupied;
                else if (statusText.Contains("unavailable"))
                    status = RoomStatus.Unavailable;
                else
                    status = RoomStatus.Available;

                Room room = new Room(number, guestName, status);
                rooms.Add(room);
            }
            Console.WriteLine("✅ Rooms loaded successfully. Ready to manage.");
        }
        catch
        {
            Console.WriteLine("Error loading Rooms.csv");
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
            Console.WriteLine("=====🏨 HOTEL MANAGEMENT SYSTEM 🏨 =====");
            Console.WriteLine("1. Show Available Rooms");
            Console.WriteLine("2. Check In Guest");
            Console.WriteLine("3. Check Out Guest");
            Console.WriteLine("4. Mark Room Unavailable / Return to Service");
            Console.WriteLine("5. Show ALL Rooms (Full Status Report)");
            Console.WriteLine("6. View Event Log");
            Console.WriteLine("7. Save and Log Out");
            Console.WriteLine("8. Return Room to Service (Set Available)");
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
                    EventLogger.ShowLog();
                    break;

                case "7":
                    SaveRooms();
                    Console.WriteLine("All changes saved. Logging out...");
                    running = false;
                    break;

                case "8":
                    Console.Write("Enter Unavailable Room Number to set AVAILABLE again: ");
                    if (int.TryParse(Console.ReadLine(), out int rn))
                        SetRoomAvailable(rn);
                    else
                        Console.WriteLine("Invalid number format.");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                    break;

                default:
                    Console.WriteLine($"⚠️ Invalid option '{choice}'. Please select 1 - 8.");
                    Console.WriteLine("Press ENTER to return to menu...");
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
        if (!int.TryParse(input, out int roomNumber))
        {
            Console.WriteLine("Invalid room number format. Please enter a number.");
            return;
        }

        // find room
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
            Console.WriteLine($"❌ Room {roomNumber} does not exist. Please try again.");
            Console.WriteLine("Tip: Use option 5 to view all valid room numbers.");
            Console.WriteLine("Press ENTER to return to the menu...");
            Console.ReadLine();
            return;
        }

        if (roomToFind.Status == RoomStatus.Occupied)
        {
            Console.WriteLine($"❌ Sorry, Room {roomNumber} is already occupied by '{roomToFind.GuestName}'.");
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

        // Now it's Available → ask for guest name and set Occupied
        Console.Write("Enter guest name: ");
        string guestName = Console.ReadLine()!;
        roomToFind.GuestName = guestName;
        roomToFind.Status = RoomStatus.Occupied;

        SaveRooms(); // save immediately
        EventLogger.AddEvent(EventType.GuestCheckIn, roomNumber, guestName);

        Console.WriteLine($"✅ Guest '{guestName}' checked into room {roomNumber} successfully!");
        Console.WriteLine("Tip: You can view current occupancy with option 5.");

        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
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



    //8. Defines option4 - Mark Room Unavailable / Return to Service--by user input
    static void MarkUnavailable()
        {
            Console.WriteLine("\n--- Mark Room Unavailable (Maintenance) ---");
            Console.Write("Enter Room Number: ");
            string input = Console.ReadLine()!;
            if (!int.TryParse(input, out int roomNumber))
            {
                Console.WriteLine("Invalid room number format.");
                return;
            }

            //  Search for the specific RoomNumber.
            Room? room = null;
            foreach (Room r in rooms)
            {
                if (r.RoomNumber == roomNumber)
                {
                    room = r;
                    break;
                }
            }


            if (room == null)
            {
                Console.WriteLine($"❌ Room {roomNumber} not found.");
                return;
            }

            if (room.Status == RoomStatus.Occupied)
            {
                Console.WriteLine($"⚠️ Room {roomNumber} is occupied by {room.GuestName}. Check out the guest first.");
                return;
            }
            
            // If the room is already Unavailable, remind the user to use Option 8.
            if (room.Status == RoomStatus.Unavailable)
            {
                Console.WriteLine($"🚧 Room {roomNumber} is already UNAVAILABLE.");
                Console.WriteLine("To set it back to AVAILABLE, please use **Option 8**.");
                
            }
            else // Room is Available, set it to Unavailable
            {
                SetRoomUnavailable(roomNumber);
            }
            
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        // Helper function to set a room's status to Available. Used by Option 8 and CheckOut.
        static void SetRoomAvailable(int roomNumber)
        {
            // Search for the specific RoomNumber.
            Room? room = null;
            foreach (Room r in rooms)
            {
                if (r.RoomNumber == roomNumber)
                {
                    room = r;
                    break;
                }
            }

            if (room == null)
            {
                Console.WriteLine($"❌ Room {roomNumber} not found.");
                return;
            }

            if (room.Status == RoomStatus.Occupied)
            {
                Console.WriteLine($"⚠️ Room {roomNumber} is occupied. Check out before marking available.");
                return;
            }

            room.Status = RoomStatus.Available;
            room.GuestName = "";
            SaveRooms();
            EventLogger.AddEvent(EventType.RoomAvailable, roomNumber);
            Console.WriteLine($"✅ Room {roomNumber} is now AVAILABLE again.");
        }

        // Helper function to set a room's status to Unavailable. Used by Option 4.
        static void SetRoomUnavailable(int roomNumber)
        {
            // Search for the specific RoomNumber.
            Room? room = null;
            foreach (Room r in rooms)
            {
                if (r.RoomNumber == roomNumber)
                {
                    room = r;
                    break;
                }
            }

            if (room == null)
            {
                Console.WriteLine($"❌ Room {roomNumber} not found.");
                return;
            }

            if (room.Status == RoomStatus.Occupied)
            {
                Console.WriteLine($"⚠️ Room {roomNumber} is occupied. Check out first.");
                return;
            }

            room.Status = RoomStatus.Unavailable;
            room.GuestName = "";
            SaveRooms();
            EventLogger.AddEvent(EventType.RoomUnavailable, roomNumber);
            Console.WriteLine($"🚧 Room {roomNumber} marked as UNAVAILABLE (under maintenance).");
        }



    //10. Defines SaveRooms to update and save all room data back into Rooms.csv
    static void SaveRooms()
    {
        try
        {
            List<string> lines = new List<string>();
            foreach (var room in rooms)
            {
                string guestPart = room.Status == RoomStatus.Occupied ? $",{room.GuestName}" : string.Empty;
                string line = $"{room.RoomNumber},{room.Status}{guestPart}";
                lines.Add(line);
            }

            string tempPath = "Rooms.csv.tmp";
            File.WriteAllLines(tempPath, lines);

            if (File.Exists("Rooms.csv"))       // If Rooms.csv exists, replace it; otherwise move the temp file.
            {
                File.Delete("Rooms.csv");
            }
            File.Move(tempPath, "Rooms.csv");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Error saving rooms: {ex.Message}");
        }
    }

}


        
            





    

    








