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
    static Dictionary<string, string> users = new Dictionary<string, string>();  //Create storage (Dictionary = list of users which I have 3users)
    static List<Room> rooms = new List<Room>();
    static void Main()    //“static” means it belongs to the class itself, not to an object and main is for run here first
    {
        Console.WriteLine("== Hotel Manager!==");
        LoadUsers(); // read users.csv
        LoadRooms(); // read rooms.csv
        if (!Login()) return;  // (early exit) If Login fails (returns false), exit the program immediately.

        Console.WriteLine("Login successful");
    }



    //Define LoadUsers that I have mentioned above for storage in csv
    static void LoadUsers()
    {
        try
        {
            string[] lines = File.ReadAllLines("Users.csv");  // for read all rows
            for (int i = 0; i < lines.Length; i++) // use loop for users
            {
                if (lines[i].Trim() == "") continue;    // for skipping empty line 
                string[] parts = lines[i].Split(',');    // for splitting username and password

                if (parts.Length < 2) continue;   //Check if there are enough parts (username and password)

                string user = parts[0].Trim();
                string pass = parts[1].Trim();

                users[user] = pass;    // use dictionary method
            }
            Console.WriteLine("Users loaded succesfully.");
        }
        catch
        {
            Console.WriteLine("Error! Try to log in again");
        }
    }



    //Define LoadRooms I have mentioned above for storage in csv

    static void LoadRooms()
    {
        try
        {
            string[] lines = File.ReadAllLines("Rooms.csv");  // for read all rows
            for (int i = 0; i < lines.Length; i++) // use loop for rooms
            {
                if (lines[i].Trim() == "") continue;        // for skipping empty line 
                string[] parts = lines[i].Split(',');       // for splitting room number and status

                if (parts.Length < 2) continue;             //// Ensure that it has at least room number and status

                int number;
                if (!int.TryParse(parts[0].Trim(), out number)) continue;     // If conversion fails ,room number is invalid, skip this line and continue to the next CSV row.
                string statusText = parts[1].Trim().ToLower();

                RoomStatus status;          // use if-satsar because rooms has 3 status
                if (statusText.Contains("occupied"))            // have guest
                    status = RoomStatus.Occupied;
                else if (statusText.Contains("unavailable"))  // no guest but room has a problem, need to be fixed
                    status = RoomStatus.Unavailable;
                else
                    status = RoomStatus.Available;              // room is free/available to book

                Room room = new Room(number, "", status);    //“Create a new Room object with its number, guest name, status — then put it into the list called rooms
                rooms.Add(room);     // save this room to the list
            }
            Console.WriteLine("Rooms loaded succesfully.Here you can see which is avaible at the moment");
        }
        catch
        {
            Console.WriteLine("Error! Try to log in again");
        }
    }



    // Define User login ,use bool because login only has two outcomes: success or fail
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

        }  // it locked after user already got 3 attempts failure
        Console.WriteLine("Too many tries, You cant access anymore !");
        return false;
    }

}
    








