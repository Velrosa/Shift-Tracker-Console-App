# Shift Tracker Console App
- A C# Console Application, Allows the user to consume an external API.
- User can view, create, update and delete "Shifts" via the API.
- Given a Start time and End time, minutes worked and pay is calculated, then user may enter a location the shift was performed.
- API consumed is: https://github.com/Velrosa/Shift-Tracker-API

# Features
* ![Shift-Project-Layout2](https://user-images.githubusercontent.com/101323127/163813472-8a97c5d0-bc87-4a95-95b9-ae373670880e.png)

* Class: Shift
  - Model object for handling/moving data
  - ![API-Shift-Model](https://user-images.githubusercontent.com/101323127/163813333-9074b46f-48c1-42b2-bf83-22cce9bf8385.png)

* Class: ShiftService
  - Making requests to the external API

* Class: TableVisuals
  - Displaying of data in a user-friendly way using ConsoleTableExt package
  - ![Shift-ConsoleTableExt](https://user-images.githubusercontent.com/101323127/163816050-41366261-5f4f-4061-a7ba-e2f8f571bcc8.png)


* Class: UserInput
  - Taking inputs from the user, then passing it to the service layer

* Class: Validator
  - Validation of user inputs

* Method: CalculateMinutesAndPay
  - Calculates the amount of minutes worked from the start and end time, and the pay from the minutes
  - ![Calculate-Method](https://user-images.githubusercontent.com/101323127/163814513-75e2cc23-5b0f-4c52-a13b-ac7a07c8203d.png)

* Method: MainMenu
  - Displays a menu for navigation of the application
  - ![Shift-MainMenu](https://user-images.githubusercontent.com/101323127/163816033-fce5dc86-006f-42da-80d9-0bfe43b91619.png)


* Uses RestSharp package to handle requests to and from the API

* App.config to store API connection string

# Packages
- ConsoleTableExt
- RestSharp
- System.Configuration.ConfigurationManager
