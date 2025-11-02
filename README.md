🏨 Hotel Management System – Reflection & Report
(How the Program Works)

Start Program

Reads users.csv for login credentials.

Asks for username and password.

If login info matches → program continues to the main menu.

Load Rooms

Reads data from rooms.csv.

Stores room details into memory using a list.

Main Menu Options

Show occupied rooms

Show available rooms

Check in a guest

Check out a guest

Mark room unavailable / available

Show event log history

Exit

User Chooses an Option

Program uses if or switch to decide what to do.

When Any Change Happens

Saves all updates automatically back to rooms.csv.

Loop Continues Until Exit

User can keep performing actions until they choose to exit the system.

Problems I Faced During Development and How I Fixed Them

‘rooms’ does not contain a definition for ‘Add’

I didn’t declare the rooms variable correctly — it must be a List, not a Dictionary.

Case Sensitivity Errors

C# is case-sensitive, so I had to match names exactly as declared.

File Locked During Build

The program couldn’t access hotelprogram.exe because it was still running in another process. Fixed by stopping the build and running again.

Null Warnings on Console.ReadLine()

I forgot to add ! after Console.ReadLine() to avoid null warnings.

List Declaration Error

Fixed by writing static List<Room> rooms = new List<Room>();

Adding Event Log (new idea)

I added EventLog.txt to store a running history of all actions (like check-in/check-out).

I used File.AppendAllText, which I learned from a classmate’s trading project.

.txt keeps a running history, while .csv stores the current state.

Nullable Room Object Warning

Used Room roomToFind = null!; to avoid null warnings and safely check if the room exists.

Adding More Rooms in CSV

When I tried to save Rooms.csv, I got the message “The content of the file is newer”.

I learned that this doesn’t affect the code — my LoadRooms() function just loads them automatically.

Adding Icons for Better UX

I added utf 8 Unicode symbols like ❌ or ⚠️ ,etc  to make it more attractive and readable (similar to what I did in my Python Project).

EventLog Not Updating

Found out I named the file Eventlog.txt instead of EventLog.txt. Fixed by correcting the filename.

Check-In and Check-Out Finally Work Properly

Now it correctly shows room number, guest name, and timestamp in both console and log file.

More Issues I Encountered Later

All Rooms Showed as “Available” Even When Some Weren’t

Inline comments like 103,Unavailable // AC needs fixing broke the CSV reading.

Fixed by cleaning the CSV and trimming input strings.

Show All Rooms Displayed Duplicates

Caused by not clearing the list before loading.

Fixed by adding rooms.Clear() at the start of LoadRooms().

Missing Curly Braces Caused Compile Errors

Fixed by checking indentation and braces carefully.

Check-Out Didn’t Change Room Status

Fixed by updating room.Status = RoomStatus.Available and saving immediately after check-out.

Option 6 (Show Event Log) Showed Nothing

Fixed by ensuring all actions properly called EventLogger.AddEvent() with the right EventType.

Additional User-Flow Problems and Fixes

Check-In Flow Issue

Before: The system still asked for a guest name even if the room was Unavailable (under maintenance).

✅ Fixed by restructuring the CheckIn() method — added early return statements when the room is Occupied or Unavailable.

Now it only asks for a name when the room is truly Available.

Guest Name Not Showing in Messages

Before: When checking into an already occupied room, it only said “already occupied” but didn’t show who was staying there.

✅ Fixed by improving the LoadRooms() function using .Trim() and string.IsNullOrWhiteSpace() to correctly read guest names from the CSV.

Now the system shows:

❌ Sorry, Room 100 is already occupied by Kevin.

Check-Out Logging Showed Empty Guest Name

Before: The log showed no guest name because it was cleared too early.

✅ Fixed by storing the guest’s name in a temporary variable before resetting the room object, and passing that to EventLogger.

Other Small Fixes

Added user feedback when entering invalid menu options (e.g., “Invalid choice. Please select 1–7.”)

Added a feature to toggle room status back to Available after maintenance (option 4).

Improved messages for unavailable or occupied rooms to make the system more user-friendly.

Fixed event log entries to show consistent and readable output.

Final System Behavior

✅ Option 1 – Shows all available rooms correctly.
✅ Option 2 – Allows check-in only if the room is available and logs the event.
✅ Option 3 – Checks out the guest, updates CSV, and records the event.
✅ Option 4 – Marks room unavailable or available again for maintenance.
✅ Option 5 – Shows all rooms with current status and guest names.
✅ Option 6 – Displays full event log history from EventLog.txt.

Conclusion

This project was challenging but fun to build.
I learned a lot about file handling, enums, and how to handle real system logic like check-in/out, maintenance, and logging.
I also improved my debugging skills — especially understanding errors, fixing logical flow, and keeping my code clean and readable.
The final program now runs smoothly and realistically like a simple hotel reception system.


becoz there the system doesnt work well after testing .. I couldnt find the way to set unmarkable back to available again (ex. room is done with maintaining) so i had to find the way to make it work , so i add one more static RoomAvailable(int roomNumber)
so i add SetRoomUnavailable() and SetRoomAvailable() as a  helper functions , dorsnt need any input same as MarkUnavailable()