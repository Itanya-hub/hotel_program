using App;
 
class Room   //The variables (the data the class will hold)
{
    public int RoomNumber;   
    public string GuestName;

    public RoomStatus Status;   // status (Free, Occupied, etc.)
    public DateTime? CheckIn;  // date/time when guest checked in

    public Room(int roomNumber, string guestName, RoomStatus status)  //The constructor
    {
    RoomNumber =roomNumber;
    GuestName = "";  //start empty (no guest yet).
    RoomStatus Status =status;
        CheckIn = DateTime.Now;
    }

}
