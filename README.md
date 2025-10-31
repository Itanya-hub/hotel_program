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


