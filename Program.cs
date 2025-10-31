//SIMPLE WORKFLOW:
// 1. Login using users.csv
// 2. Load room data from rooms.csv
// 3. Show main menu (show rooms, check in/out, mark unavailable)
// 4. Save any changes automatically
//5. Repeat until user chooses Exit


using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;


namespace App;

class HotelManager
{
    static Dictionary<string, string> users = new Dictionary<string, string>();  //Create storage (Dictionary = list of users which I have 3users)
    static List<Room> rooms = new List<Room>();    // data storage as internal data which use with foreach (var:Room room in rooms)
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
                if (lines[i].Trim() == "") continue;    // for skipping empty line 
                string[] parts = lines[i].Split(',');    // for splitting username and password

                if (parts.Length < 2) continue;   //Check if there are enough parts (username and password)

                string user = parts[0].Trim();
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
            rooms.Clear();   //This prevents duplicating the entire room list every time LoadRooms() is called.
            string[] lines = File.ReadAllLines("Rooms.csv");  // for read all rows (external data)
            for (int i = 0; i < lines.Length; i++) // use loop for rooms
            {
                if (lines[i].Trim() == "") continue;        // for skipping empty line 
                string[] parts = lines[i].Split(',');       // for splitting room number and status

                if (parts.Length < 2) continue;             //// Ensure that it has at least room number and status

                int number;
                if (!int.TryParse(parts[0].Trim(), out number)) continue;     // If conversion fails ,room number is invalid, skip this line and continue to the next CSV row.
                string statusText = parts[1].Trim().ToLower();


                // use if-satsar because rooms has 3 status

                RoomStatus status;
                if (statusText.Contains("occupied"))            //Having guest checked in 
                    status = RoomStatus.Occupied;
                else if (statusText.Contains("unavailable"))  // no guest but room has a problem, need to be fixed
                    status = RoomStatus.Unavailable;
                else
                    status = RoomStatus.Available;              // room is free/available to book

                Room room = new Room(number, "", status);    //Create a new Room object with its number, guest name, status — then put it into the list called rooms
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
            Console.WriteLine("--- MAIN MENU ---");
            Console.WriteLine("1. Show All Rooms");
            Console.WriteLine("2. Check In Guest");
            Console.WriteLine("3. Check Out Guest");
            Console.WriteLine("4. Mark Room Unavailable");
            Console.WriteLine("5. Save Changes and Exit");
            Console.Write("Enter option (1-5): ");

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    ShowRooms();
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
                    SaveRooms();
                    Console.WriteLine("Saving changes and exiting. Goodbye!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }


    //5. Defines option1 from main menu
    static void ShowRooms()
    {
        Console.WriteLine("\n---Current room status---");  // Prints a formatted header to the console.
        foreach (var room in rooms)                         // Loops through every Room object in the static rooms list,load from Rooms.csv
        {
            Console.WriteLine($"Room {room.RoomNumber}: {room.Status}"); // Prints the room number and its status
        }
        Console.WriteLine("-------------------------------\n");    // Prints a formatted footer.
    }


    //6. Defines option2 -check in from main menu

    static void CheckIn()
    {
        Console.WriteLine("\n--- Check in guest ---");
        Console.Write("Enter room number to check in: ");
        string input = Console.ReadLine()!;


        //Safely convert the input string to an integer from user input.
        int roomNumber;
        if (!int.TryParse(input, out roomNumber))
        {
            Console.WriteLine("Invalid room number format. Please enter a number.");
            return; // Exit the method and go back to the main menu.
        }


        // Find the room that matches the number
        Room? roomToFind = null;

        foreach (var room in rooms)     // Loop through every room in the static 'rooms' list.
        {
            if (room.RoomNumber == roomNumber)
            {
                roomToFind = room;       // Found the matching room!
                break;                  // Stop the loop immediately (if user found the first match).
            }
        }

        // If room doesn’t exist
        if (roomToFind == null)
        {
            Console.WriteLine($"Error: Room {roomNumber} does not exist.");
            return;
        }


        // If room is not available
        if (roomToFind.Status == RoomStatus.Occupied)
        {
            Console.WriteLine($"Room {roomNumber} is already occupied!");
            return;
        }
        else if (roomToFind.Status == RoomStatus.Unavailable)
        {
            Console.WriteLine($"Room {roomNumber} is unavailable (needs maintenance).");
            return;
        }


        // If available, check in a guest
        Console.Write("Enter guest name: ");
        string guestName = Console.ReadLine()!;
        roomToFind.GuestName = guestName;      // store guest name
        roomToFind.Status = RoomStatus.Occupied; // change room status

        Console.WriteLine($"Guest {guestName} has checked into room {roomNumber}.");
            
        

        //Handle the case where the room is not found in the list.
        if (roomToFind == null)
        {
            Console.WriteLine($"Error: Room {roomNumber} does not exist.");
            return;
        }
        else
        {
            Console.WriteLine($"Room {roomNumber} is Available. Ready for Check-In.");
            SaveRooms();      // Save the updated data to the CSV file
        }
    }

        //7. Defines option3-check out from main menu
        static void CheckOut()
        {
            Console.WriteLine("\n--- Check Out Guest () ---");
            string input = Console.ReadLine()!;
            SaveRooms();
        }


        //8. Defines option4- mark unavailable room from main menu
        static void MarkUnavailable()
        {
             Console.WriteLine("\n--- Mark Room Unavailable () ---");
             string input = Console.ReadLine()!;
             SaveRooms();
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


        
            





    

    








