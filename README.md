This README describes my Hotel Management System built in C#. It fulfills all project criteria:

- Uses enums, file I/O, and console-based UI
- Includes login, room management, check-in/out, maintenance, and event logging
- Automatically saves data to CSV files and logs all actions
- Features detailed comments, documentation, and multiple tested commits in Git
- I have documented my debugging process, testing, and reflections below.




                                            🏨 -----Hotel Management System — ----🏨


A simple C# console-based hotel management system for receptionists, featuring login, room management, check-in/out, maintenance, and logging.


 💻Tech Stack I used in this projects :

1. C# (.NET)

    🧰C# structure, enums, file handling, and logical flow part

        🔶Used enum RoomStatus { Available, Occupied, Unavailable }

        🔶Correctly declared List<Room> rooms = new List<Room>();

        🔶Built methods for every real-world action: CheckIn, CheckOut, SetRoomUnavailable, SetRoomAvailable, SaveRooms, LoadRooms

        🔶Fixed common logical errors myself (reload overwriting data, null safety, etc.)

        🔶Added validation (if (room == null), if (room.Status == RoomStatus.Occupied) etc.)

        🔶Used clean control structures (switch, foreach, early returns)
 
    📁 With file system management

        🔷Implemented LoadRooms() and SaveRooms() with safe file operations.

        🔷Used a temporary file and atomic replace pattern in SaveRooms (using .tmp then move) 

        🔷Handled exceptions with try/catch.

        🔷Saved user actions in EventLog.txt with timestamps.

        🔷Cleaned up input with .Trim() and fixed CSV parsing errors.


2. CSV file storage

3. Console-based UI, Enchanced with UTF-8 icons (I got the icon resource from https://www.w3schools.com/charsets/ref_emoji_symbols.asp )

4. Github, I keep updated multiples commits with descriptive messages anytime I was done with coding.




💡 Overview (What This Program Does)

This is a small console-based hotel management system built in C#. It simulates what a receptionist does in a real hotel:

=> Log in using credentials from Users.csv

=> View and manage room availability

=> Check guests in and out

=> Mark rooms as "Unavailable" (under maintenance) or change the status back to "Available".

=> Automatically saves all updates to Rooms.csv

=> Keep a full event history with timestamps in EventLog.txt

=> The system uses no external libraries, only core C#, and everything runs in the terminal.



📂 Files Used

⚙️ Program.cs                 =>       Main logic and menu system.
💾 Users.csv	              =>       Stores login credentials (username, password).
🚪 Rooms.csv	              =>       Tracks all room data — RoomNumber, Status, GuestName.
🚪 Room.cs / RoomStatus.cs	  =>       Models and Enum for Available, Occupied, Unavailable.
📜 EventLog.txt 	          =>       Appends every action (check-in/out, maintenance, etc.) with date & time.
📜 EventLogger.cs	          =>       Helper class to log actions and timestamps.



▶️ How to Run

1.Open the terminal in the project folder. Run: "dotnet run"
2.Log in with a user from Users.csv.
3.Follow the numbered menu to perform actions.
4.The system saves automatically after every action.


🧠 How It Works (Core Functions)

    ⚙️Enums define room status (Available, Occupied, Unavailable)

    ⚙️LoadRooms() reads data from Rooms.csv into memory (with .Trim() cleanup)

    ⚙️SaveRooms() writes updated data safely back to file

    ⚙️EventLogger.AddEvent() writes a timestamped log to EventLog.txt

    ⚙️MarkUnavailable() / SetRoomAvailable() toggle maintenance mode

    ⚙️CheckIn() / CheckOut() manage guest stays and keep log entries



🧾 Main Menu (Options menu 1–8)


1	Show Available Rooms	✅ ShowAvailableRooms()	            Displays rooms that are ready
2	Check In Guest	        ✅ CheckIn()	                        Handles new guest
3	Check Out Guest	        ✅ CheckOut()	                    Frees room
4	Mark Room Unavailable	✅ MarkUnavailable()               → Uses SetRoomUnavailable() Maintenance mode
5	Show All Rooms	        ✅ ShowAllRooms()	                Status overview
6	Return Room to Service	✅ SetRoomAvailable()	            Makes unavailable room available
7	Save and Log Out	    ❌ (inline code)                     Handles saving & logout logic inside MainMenu without calling a separate function.
8	View Event Log	        ✅ EventLogger.ShowLog()             Verified all updates reflected correctly in both files




😅 Problems I Faced & How I Fixed Them
1. "rooms does not contain a definition for Add"

❌ Declared rooms incorrectly as a dictionary.
✅ Fixed by defining it as static List<Room> rooms = new List<Room>();.


2. EventLog Not Updating

❌ File named Event_log.txt instead of EventLog.txt.
✅ Fixed filename and ensured correct argument order in EventLogger.AddEvent().


3. Check-In Asked for Guest Name Even When Room status was "Unavailable".

❌ The system still kept asking for the guest's name when I tried to check in to an "Unavailable" room. It shouldnt be possible to check-in a
guest in an "unavailable" room. Now if you try to check-in a guest you get the following message "Please choose another room (use option 1 to see the available rooms).
✅ Added early return statements inside CheckIn() when the room is Unavailable.


4. Check-Out Logged Empty Guest Name.

❌ No information about guestname was shown in the eventlog.
✅ Stored guest’s name in a temporary variable before clearing it so EventLog correctly shows the guest name upon checkout.


5. Rooms Not Switching Between Available and Unavailable (This was one of the hardest part)

❌ Couldn't find the way to switch room status between Available and Unavailable. I can change from Available 
to Unavailable but I couldn’t make it work the other way around. I spent hours trying to make a room return from Unavailable to Available again, 
✅ Fixed by removing LoadRooms() from helper functions and adding explicit methods:
SetRoomUnavailable() and SetRoomAvailable() and add into case logic block (option5)which is the easiest way for the user to reset a room status to Available 

6. Menu Options Reordered

❌ The original order (option login from main menu as 1,5,2,4...) was confusing.
✅ I restructured the switch block to follow natural logic flow from Available → Check-In → Check-Out → Maintenance → Available For Booking (again)→  Logs.
This makes testing and user experience much smoother and I think this makes more sense.



🧩 Testing Process I have been doing 🧩

📌 Logged in with test user from Users.csv and recheck if all room status match with Rooms.csv file, I have learned that when I accidentially click on or has recently updated the same Rooms.csv file while I ran terminal in VScode, the Room.csv file did not update correctly. The system asking about overwrite or compare before saving ” So it is better to make it all done and save before I do dotnet run in terminal.

📌 Tried marking a room Unavailable → Confirmed Rooms.csv updated

📌 Used Option 6 to make that room Available again (Which I had been trying to fix for many hours)

📌 Checked in a guest → Room changed to Occupied

📌 Checked out → Room became Available again

📌 Verified all updates reflected correctly in both files

📌 Tested wrong inputs → Correct error messages shown

📌 Tested unavailable and occupied states → System blocked actions correctly

📌 Everything now works as expected — both logically and visually with icons like
⚠️, ❌, ✅, 🚧 etc.



💬 Personal Reflection

This project honestly took me through a rollercoaster of frustration and learning. Every time I thought I fixed one bug, another appeared.
I spent hours staring at CSV data, wondering why rooms didn’t update — only to realize I was overwriting them myself.
But each small victory felt great.

I learned that debugging isn’t just coding — it’s patience, observation, logic and even small things such as spelling errors and case-sensitive.
I also saw how important proper file handling, validation, and control flow are in real-world systems.
Even small details like icons (⚠️, ❌, ✅) made the program feel more alive and friendly.
It reminded me of the projects I did in Python and pushed me to think about user experience — even in console apps.

After I have been doing this project, there are so many idea started to show up in my head and I even asked my friend (who has no coding exp but manager skill) to test my code and give me some feedback If the program have missed something or if I should make it better, then I got so many ideas, for example:
 He asked that ....
🔍Why there is no name of guest when its occupied room (actually it was about code I missed this part {guestName} in that logic block) -but I fixed
🔍How to set the room from unavailable to available again so the hotel can get more room to check in, he did not see the option to do that (and this was why I added option 6 later )
🔍Can you add more users for log-in, as manager and reception name because there is not only one person that works as reception for 24/7. 
I totally agree with the idea but it would be more complex than this project by adding different permission method and user login. As we did in the group project about the Health care system and I will keep it in mind for the future project. 


🚀 Future Development Ideas 

🔦Add user roles (Admin vs. Receptionist) with different permissions , Include Receptionist Name in every log entry (for accountability) so the manager can see who is the reception at the current time, there is something happen, it is easier to deal direct to the right person as...

         In professional systems, every log entry should include who performed the action — not just what happened.
         ------------------------------------------------------------------------------------------------------------
           Example:        GuestCheckIn: Room 101, Time: 2025-11-01 23:15:30, By User: Tanya_L
           
           This turns the log from a basic history file into a true audit trail, improving accountability and real-world usability.
        ------------------------------------------------------------------------------------------------------------------------------

🔦Create a search function for guests by name or room.

🔦Add daily revenue summary or report generation.

🔦Use text-based color themes or a small GUI for better visualization , I used to work as a graphic design so I would love to see my code look like a real nice colorful app one day .



🏁 Final Thoughts ❤️

This project started as a simple idea — but became a real test of persistence ,From syntax issues, file overwrites, to missing or add too many of braces {}, adding logic block ion the wrong order and so on , but in the end, everything finally works the way I expected.

It feels like a real hotel reception system — small and reliable.

“Every error message taught me something.” And that’s what programming really is a long conversation between me and my code until it finally understands what it means. 



Thank you for using my  -----Hotel Management System — ----

Tanyaluk Larsson ( App Developer System )


