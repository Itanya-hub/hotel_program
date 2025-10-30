using App;
 
class Room   //The variables (the data the class will hold)
{
    public int RoomNumber;   
    public string GuestName;

    public RoomStatus status ;   // status (Free, Occupied, etc.)
    public DateTime? CheckIn;  // date/time when guest checked in

    public Room(int number)  //The constructor
{
    RoomNumber =number;
    GuestName = "";  //start empty (no guest yet).
    RoomStatus =roomstatus;
    CheckIn = null; //
}

}
