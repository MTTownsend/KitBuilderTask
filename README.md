# KitBuilderTask
## Setup instructions
1. Open the project in visual studio.
2. Press F5 to start the application.
3. The app is ready to accept command in the debug console.

## Run commands
- 'PLACE' is the first command required in order to be able to report the position and facing direction of the RoBoFriend.  
  The syntax for the command is `PLACE X Y DIRECTION`.
  - `X` and `Y` must interger values between 0 and 5.
  - `Direction` must be a cardinal direction: `NORTH`, `EAST`, `SOUTH`, or `WEST`.
  e.g. `PLACE 0 4 NORTH`.
- 'MOVE' will increment the coordinate by 1 in the direction that it is relevant for e.g. if the RoBoFriend is facing `NORTH` and moves the `Y` coordinate will be incremented.  
  If the command would move the RoBoFriend outside the table boundary the command is ignored.
- 'RIGHT' will rotate the direction clockwise 90 degrees e.g. `NORTH` will become `EAST`.
- 'LEFT' will rotate the direction counter-clockwise 90 degrees e.g. `NORTH` will become `WEST`.
- 'REPORT' will write the `X`, `Y`, and `DIRECTION` of the RoBoFriend to the terminal.
- 'END' will exit the app.
Note: all commands are not case sensitive.

## Test execution
1. Open the test folder in the preoject.
2. Right click on `RoBoFriendTests.cs` and select run tests.
3. In the terminal, go to the TEST RESULTS tab to see test results.

## Design decisions
I decided to break the app down into 4 main classes in order to help separate the class responsibilities. These were `Table`, `RoBoFriend`, `RoBoParser`, and `RoBoReportGenerator`. `Table` is responsible for setting the `X` and `Y` coordinate boundaries used by `RoBoFriend`. `RoBoFriend` is repsonsible for moving itself within the boundary set by `Table`.
`RoBoParser` is responsible for parsing the user input for `RoBoFriend`. Finally `RoBoReportGenerator` is responsible for writing the report to the terminal.

Interfaces were used for all these classes to ensure only required functionality was made available.

Due to there being no error logging and the requirement for errors to be handled silently, no exceptions are thrown when users make and error. If there had been a requirement to do something when a suser made an error this would have been implemented.

An issue arose with testing the commands as they are all private as they are not required to be public. This mean when testing in order to validate the tests mulitple commands were required at minimum, These were 'PLACE', 'REPORT', and 'END'. I felt this was a good tradeof as i didnt want to expose unneeded funtionality just for testing. This was also backed by the fact that both the 'REPORT' and 'END' commands to not change any data so i felt it was worth the tradeof for keeping access functionality as need to know as possible.

In order to account for potential future changes i ensured that where appropriate values were stored as variables instead of hard coded into a function, such as the table boundary values and the movement speed. Should there be a future requirement these values could be changed either by extending the classes or adding new functions to alter these values. This means the functions that use them such as 'MOVE' and 'PLACE' will still work without the requirement to edit them.

## Assumptions made
- Commands may be entered one at a time or as a comma separated list.
- When an invalid command is entered the command is ignored, but the app does not exit. The app will move on to the next command if multiple were entered.
- The 'END' command should still function to exit the app even if it is the first command made.
