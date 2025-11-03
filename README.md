Hotel Management System — README 
What this program does---

A small console app for a hotel receptionist.
It lets a receptionist:

log in with usernames from Users.csv,

view current rooms and their statuses,

check guests in and out,

mark rooms Unavailable for maintenance and make them Available again,

save every change to Rooms.csv, and

keep a timestamped event history in EventLog.txt.

Files used

Users.csv — login credentials (username,password)

Rooms.csv — room state lines as RoomNumber,Status[,GuestName]
Example: 100,Occupied,Kevin or 103,Unavailable

EventLog.txt — append-only log of actions (check-in, check-out, maintenance)

Program.cs / HotelManager.cs — main code

EventLogger.cs — small logger helper

Room.cs / RoomStatus enum — models

How to run

Open terminal in the project folder.

dotnet run

Log in (use entries in Users.csv).

Use the menu (1–8) to perform actions.

Main menu (what each option does)

Show available rooms

Check in a guest (only works if room status is Available)

Check out a guest (room becomes Available)

Mark Room Unavailable / Return to Service (toggle)

Show all rooms (full status + guest names)

View event log (EventLog.txt)

Save and Log Out (saves file, return to login/exit)

Return Room to Service (Set Available) — explicit helper to mark a room available

Note: option numbering was adjusted so the user can choose either toggle (4) or explicit return-to-service (8).

Key design choices & small notes



Enums used for room status: Available, Occupied, Unavailable.

File format: Rooms.csv stores the current state; EventLog.txt stores a running history (timestamped).

Atomic save: the program writes to a temporary file then moves it to Rooms.csv to avoid partial writes and save errors.

No external libraries — everything is done with core C#.

Problems I faced & how I fixed them (short)

rooms was declared wrong → changed to List<Room> so rooms.Add() works.

CSV parsing issues (inline comments) → cleaned CSV lines and .Trim() values while parsing.

Duplicate rooms shown → solved by calling rooms.Clear() before loading.

Event log not updating → fixed mis-typed filename and ensured correct method calls.

File locking / “content is newer” → avoided reloads in helpers and used atomic SaveRooms(); close other running instances or reload editor.

Check-in asked for guest even when Unavailable → added early return checks.

Check-out logged empty names → stored guest name in a temporary variable then logged, before clearing the room.

Marking available/unavailable sometimes overwritten → removed LoadRooms() after save and added explicit SetRoomAvailable() and SetRoomUnavailable() helper functions.

How I tested 

Start program and log in.

Option 5 to see full room list.

Use Option 4 to mark a room Unavailable; verify Rooms.csv updated.

Use Option 8 to set it Available again; verify Rooms.csv.

Option 2 to check-in a guest into an Available room; check Rooms.csv shows Occupied,GuestName and EventLog.txt has entry.

Option 3 to check out; Rooms.csv should show Available and EventLog.txt check-out entry.

**gonna rewrite all later ,,now just add everything so i wont forget what i have been doing