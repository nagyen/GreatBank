## Great Bank
* Features
    - Create a new account
    - Login
    - Record a deposit
    - Record a withdrawal
    - Check balance
    - See transaction history
    - Log out

## Database
The application uses Entity Framework Core In-Memory database.

## Tables
* Auth
    - Id (long)
    - UserId (long)
    - Authkey (guid)
    - CreateDateTime (DateTime)
* User
    - Id (long)
    - Username (string)
    - FirstName (string)
    - LastName (stirng)
    - Password (string)
    - CreateDateTime (DateTime)
* Transaction
    - Id (long)
    - UserId (long)
    - Date (Datetime)
    - Type (enum: deposit, withdrawal)
    - Amount (decimal)
    - PrevBalance (decimal)
    - CurrentBalance (decimal)

## Environment
The application is written using .NET Core(console app), ASP.NET MVC Core(web app), Entity Framework Core.

## Steps to run applications locally (Requires dotnet core environement installed on computer)
* Console App: Run any of the following commands from project root 
    - $ sh run-console.sh
    - $ dotnet restore && cd ./console_app && dotnet run
* Web App: Run any of the following commands from project root 
    - $ sh run-web.sh
    - $ dotnet restore && cd ./web && dotnet run

## Steps to run applications using docker (Requires docker installed on computer)
* Console App: Run the following commands from project root 
    - $ docker run --rm -it nyendluri/greatbank-console:latest
* Web App: Run the following commands from project root 
    - $ docker run --rm -p 5000:5000 nyendluri/greatbank-web:latest
 
Note: If port 5000 is not available please map to any other available port