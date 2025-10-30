//SIMPLE WORKFLOW:
// 1. Login using users.csv
// 2. Load room data from rooms.csv
// 3. Show main menu (show rooms, check in/out, mark unavailable)
// 4. Save any changes automatically
//5. Repeat until user chooses Exit


using System.Net.NetworkInformation;
using System.Collections.Generic;


namespace App;

class HotelManager
{
    static Dictionary<string, string> users = new Dictionary<string, string>();  //Create storage (Dictionary = list of users which I have 3users)
    static List<Room> rooms = new List<Room>();    
    static void Main()    //“static” means it belongs to the class itself, not to an object and main is for run here first
    {
        Console.WriteLine("== Hotel Manager!==");
        LoadUsers(); // read user.csv
        LoadRooms(); // read loadrooms.csv
        if (!Login()) return;  //to make sure only a valid user can continue otherwise the program stops.

        Console.WriteLine("Login successful" );
    }
    static void LoadUsers()   //Define LoadUsers that I have mentioned above for storage in csv
    {
        try
        {
            string[] lines = File.ReadAllLines("users.csv");  // for read all rows
            for (int i = 0; i < lines.Length; i++) // use loop for users
            {
                if (lines[i].Trim() == "") continue;    // for skipping empty line 
                string[] parts = lines[i].Split(',');    // for splitting username and password
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
    
    static void LoadRooms() //Define LoadRooms I have mentioned above for storage in csv
    {
        try                                                    
        {
            string[] lines = File.ReadAllLines("rooms.csv");  // for read all rows
            for (int i = 0; i < lines.Length; i++) // use loop for rooms
            {
                if (lines[i].Trim() == "") continue;        // for skipping empty line 
                string[] parts = lines[i].Split(',');       // for splitting room number and status
                int number = int.Parse (parts[0].Trim());     // using int because it's room number then convert to string
                string statusText = parts[1].Trim().ToLower();

                RoomStatus status;          // use if-satsar because rooms has 3 status
                if (statusText.Contains("occupied"))            // have guest
                    status = RoomStatus.Occupied;
                else if (statusText.Contains("unavailable"))  // no guest but room has a problem, need to be fixed
                    status = RoomStatus.Unavialable;
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
    
        
    
}







