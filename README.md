 (How Program Works)

1. **Start Program**
   - Reads `users.csv`
   - Asks for username and password
   - If login info matches → continue

2. **Load Rooms**
   - Reads `rooms.csv`
   - Saves data into memory (list or dictionary)

3. **Main Menu**
   1. Show occupied rooms  
   2. Show free rooms  
   3. Check in a guest  
   4. Check out a guest  
   5. Mark room unavailable  
   6. Exit

4. **User Chooses Option**
   - Program uses `if` or `switch` to decide what to do

5. **When Changes Happen**
   - Save data back into `rooms.csv` automatically

6. **Loop continues until Exit**



**problem during the code **
1.'rooms' does not contain a definition for 'Add'
I didn’t declare the rooms variable correctly.
It must be a List, not a Dictionary.
2.Error spell with sensitive case
3.File Locked During Build ,The process cannot access the file 'hotelprogram.exe' because it is being used by another process.
4.Forget to Add ! after Console.ReadLine() for Null warnings
5.Compiler error: 'rooms' does not contain a definition for 'Add'. so i tried to Corrected the global variable declaration to use static List<Room> rooms = new List<Room>();.
6. its good to have a record in eventlog same concecpt as my group assignment in healthcare but i will do this in.txt to just store a running history of actions. and the Users never need to go back and edit a log entry; they just append a new one which is different from .csv which is for store the application's current state as structured data and i use File.AppendAllText which I got an idea from one of my classmate that he used this method in trading assignment.
7.I used this funtion Room roomToFind = null!; for check in part ***
“roomToFind might not exist, so check for null before using it.

8. new thing I have learned is I tried to add more room in Rooms.csv the system doesnt let me save , it shows THe content of the file is newer, please compare your version with the file contents or overwrite the content of the file with your changes  , I was worried if it would effect any code I have been doing but it seems fine because I have I have LoadRooms() function will just load them automatically.
9. I also can add icon to match with the method , I did with Python when i made a bus project and i try on c# , it works , i think its more fun to have some icon for some menu ex."❌ Sorry, Room {roomNumber} is already occupied." or ⚠️ Room {roomNumber} is unavailable (under maintenance). 



still underconstruction
6. All rooms were incorrectly loaded as Available. that should be set to Unavailable follow by the rooms list i made in Rooms.csv
i assume that The Rooms.csv file contained inline comments (e.g., 103,Unavailable // AC needs to be fixed). The string.Split(',') operation included the comment as part of the status string: "Unavailable // AC needs to be fixed" .. ...

7.The Show All Rooms display listed every room twice. This caused occupied rooms (e.g., Room 101) to appear available during check-in because the search found the second, uninitialized copy of the room.  , still not work so far even I added rooms.Clear() as the first line within the LoadRooms()

8. ShowRooms, CheckIn, check out are inside Mainmenu() , code doesnt run correctly 

**git commit**    SaveRooms();  into case 5 so its save every changes 
I have a misplaced closing curly brace ({}). When a brace is missing:


***mark som room available** 
if i check out guest 100 original is occu ,, is it suppose to change to available ? I neeed to find solution 
When a guest checks out of a room that was Occupied, the room's status must change back to Available so that it can be assigned to the next guest.
The static void CheckOut() method will essentially perform the reverse of the CheckIn() process.

** update 
show main menu ,even file

Since i call them from another class (HotelManager / Program), i must make them public static.

after update eventlog file, option 1 show 2 rooms avaible which is correct info I have in Room.csv
option 2  when i click check in to the occupied room as 100 , it doesnt show that the room not availble
and when i try to option2 with the available room is work and ask for add guest name in but in event log doesnt show the correct room number (room 105) 
I just check in , it shows only this RoomUnavailable: Room 1, Guest: Room 105, Time: 2025-11-01 23:02:29
then i choose option 4 and choose room 105 again just want to mark room unavilable 
-- Mark Room Unavailable / Return to Service ---
Enter Room Number: 105
Room 105 is now set to AVAILABLE again. 
So when you I pass multiple parameters (room number and guest name separately), it doesn’t combine them properly — the logger gets confused and only writes the first argument (1) as “room”.

it took time to find the right solution for me until i reliazed that there is some thiing wrong with EventLogger.AddEvent() call uses incorrect argument order.



Saving changes to Rooms.csv...
Room status has been sucessfully updated and saved!
Room status updated. Press ENTER to continue...

option6 nothing show up 


when i choose option2 ,check in i try to add room number that Its occupied but nothinh happen , i want system to show that Sorry, this room is taken, try another room number by press option1

but it goes well with 
option 2 to check in and add guestname then when i check sttus room again ,its not availble anymore
option3 works wih check out and eventlog also work well and room back to available again if use option1

but option4
when i choose the room status like 102 which is occupied ,system show unavailable instead and it shows like this with all room but eventlog nothing status change

option5 works , follow by option4 i try to mark unavailable room to available room status as 105 and it becomes unavailable(maintain process) but how do i unmark the room when its done fixing and set it become availble again





I created EventLogger.cs to record all activities inside the hotel system.

It automatically saves each action (Check In, Check Out, Mark Unavailable, etc.) into a text file called EventLog.txt.

The log also records the date and time of each event.

I used two AddEvent methods:

One for guest actions (needs guest name)

One for room-only actions (maintenance)

ShowLog() lets the receptionist see all history from the console window.



1.when the room is show unavialble status it shouldnt show the option enter guest name 
No checkout name is visable in the log. Checkin is showing though. ---solution is use enum to Defines what kind of event happened
    enum EventType { GuestCheckIn, GuestCheckOut, RoomUnavailable, RoomAvailable, Error }



2when user put the wrong option in menu it should give an error message and show its wrong number ,pl select 1-7 , i have this option but it didnt work 
  default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
                    
3.when the room show available - then check in it should show occupied not unavaible 