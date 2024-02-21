# term-deposit-calculator
A term deposit calculator, which using a specified start deposit amount, interest rate, investment term and how often the interest should be paid, returns the total final balance of your term deposit account given the input parameters. The method is currently accessed through the Main method inside Program.cs. This calculator was based on Bendigo Bank's deposit and savings calculator. See https://www.bendigobank.com.au/calculators/deposit-and-savings/ 

# Built With
- .NET 7.0.203
- C#

# Getting Started
## Prerequesites 
You need to have .NET installed, preferably the latest version. You can download it here: https://dotnet.microsoft.com/download  
If using VSCode as your IDE, make sure you have the C# extension installed.  

## Installation
1. Clone the repository
2. Open the project in your chosen IDE.  
3. You can build and run the app using the CLI.  
To build: 
```
dotnet build  
```
To run: 
```
dotnet run
```
Inside of the main method, you can edit the input parameters to test the term deposit calculator and find out your final balance.

Enjoy!

## What I Would do if I had more time
### Unit Tests
I would add unit tests so that the code can safely be refactored while ensuring the code's functionality has not been altered and is behaving as expected. 

### Rounding
I would take more time looking into the best rounding methods and data types to utilise in this application. Floats and doubles are both approximations and when dealing with money, I wouldn't want a program to estimate even my cents. 
I would research if there are better data types to use when dealing with currency and check in with the client whether they want the result to be rounded to the nearest cent or the nearest dollar. 

### Command Line Usability
If I had more time, I would've made the method easier to use and directly accessible through the command line rather than having to go into the code in order to use it. When the app is run, the command line would prompt for each input it needs. This way the user would also get feedback instantly when entering invalid information, compared to the current solution where they enter all input at once but can only get feedback about one invalid input. 

If even more time was allowed, I would've created a user interface and set it up in JavaScript, HTML and CSS.  
