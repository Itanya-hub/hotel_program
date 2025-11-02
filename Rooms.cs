namespace App;

public enum RoomStatus
{
    Available,
    Occupied,
    Unavailable
}

    // Defines the structure for a single hotel room
    class Room 
    {
        public int RoomNumber; // Stores the number of the room (e.g., 101)
        public string GuestName; // Stores the name of the person staying here
        public RoomStatus Status; // Stores the current state of the room (using the list above)

        // Constructor to easily create a new Room object
        public Room(int number, string guestName, RoomStatus status) 
        {
            RoomNumber = number; // Set the room number
            GuestName = guestName; // Set the guest name
            Status = status; // Set the room status
        }
    }