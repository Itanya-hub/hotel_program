using App;
 
class Room   //The variables (the data the class will hold)
{
    public RoomStatus Status;   // status (Free, Occupied,Available ) and also class's permanent data storage
    public int RoomNumber;   
 
    public DateTime? CheckIn;  // date/time when guest checked in

    public Room(int roomNumber, string guestName, RoomStatus status)  //The constructor
    {
    RoomNumber =roomNumber;

    this.Status =status;
    CheckIn = DateTime.Now;
    }

}
