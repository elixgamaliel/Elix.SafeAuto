# Elix.SafeAuto

# Introduction
This is POC/Demo to show my skills in some technologies, namely C#/.Net/React.
The problem that this is solving is:

The code will process an input file.
Each line in the input file will start with a command. There are two possible commands.
The first command is Driver, which will register a new Driver in the app. Example: Driver
Dan The second command is Trip, which will record a trip attributed to a driver. The line
will be space delimited with the following fields: the command (Trip), driver name, start
time, stop time, miles driven. Times will be given in the format of hours:minutes. We'll
use a 24-hour clock and will assume that drivers never drive past midnight (the start
time will always be before the end time). Example: Trip Dan 07:15 07:45 17.3 Discard any
trips that average a speed of less than 5 mph or greater than 100 mph. Generate a
report containing each driver with total miles driven and average speed. Sort the output
by most miles driven to least. Round miles and miles per hour to the nearest integer.
Example input:
Driver Dan
Driver Alex
Driver Bob
Trip Dan 07:15 07:45 17.3
Trip Dan 06:12 06:32 21.8
Trip Alex 12:01 13:16 42.0
Expected output:
Alex: 42 miles @ 34 mph
Dan: 39 miles @ 47 mph
Bob: 0 miles

# Technologies used
- ASP.Net Core
- C#
- React
- JavaScript
- CSS
- SpecFlow
- Autofac

# Description
I had some fun with this, I built it in using the structure I usually go with lately,
it may be a bit overkill for the needs but it allows me to add on it even if just for fun.
I used SpecFlow as the test engine since I've been using it for my projects recently and I've
been liking it a lot.
I'm not so good with UI so it looks ugly but I tried to at least improve a bit on the file
upload component.
Used Autofac cause I like it for IoC, not really needed here but it may allow me to change
the service to persist the data or have a completely different implementation.

# Possible Improvements
- Use SeriLog (I usually use this for logging)
- Persist the data
- Add more scenarios