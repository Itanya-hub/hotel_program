namespace App;

public enum RoomStatus
{
    Available,
    Occupied,
    Unavailable
}

public class Room
{
    public int RoomNumber;
    public string GuestName;
    public RoomStatus Status;

    public Room(int number, string guest, RoomStatus status)
    {
        RoomNumber = number;
        GuestName = guest;
        Status = status;
    }
}
