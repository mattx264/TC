# TC
## Local setup
### What you need to start:
 1. visual studio 2019
 2. visual code
 3. Mssql
 4. WSL - how to setup https://docs.microsoft.com/en-us/windows/wsl/install-win10#install-your-linux-distribution-of-choice
 5. Angular https://angular.io/guide/setup-local
 6. .Net core 3.0 https://dotnet.microsoft.com/download
 
 ## Setup:
 1. Clone git
 2. Create local database
 3. Run in console `dotnet tool install dotnet-ef --global`
 3a. If dotnet-ef is installed run update `dotnet tool update dotnet-ef --global`
 4. Run TC/recreate-db.bat - script will create database and seed data.
 5. Setup redis (required WSL)
 6. Follow setup in TC/Redis-setup.txt
 
 # Start coding - front end
 1. Open vscode 
 2. Go to \TC\TC.FrontEnd\Angular
 4. Run in console `npm install`
 5. Run in console `ng serve` local service will start on port 4200, go to website http://localhost:4200/ - don't forget about backend setup 
 ### Setup chrome extension 
 1. Go to \TC\TC.FrontEnd\Angular
 2. Run command `ng build tc-browser-recorder`
 1. Open Chrome
 2. Settings -> Extnesnions -> turn on "Developer mode"
 3. "Load unpacked" set path \TC\TC.FrontEnd\Angular\dist\tc-browser-recorder
 
 # Back end
 Project architecture:
 ...
 ### Start backend
 1. Open wsl ubuntu console and run `redis-server` (if redis is not setup - follow instruction in TC/Redis-setup.txt)
 1. Open TestingCenter.sln project file in Visual Studio (as a administrator)
 2. Set TC.WebService as a default project to start (right click)
 3. Start IIS express project TC.WebService (it should be by default)
 
  ### Database connection
  name of database you can find in:
  ..\TC.WebService\appsettings.Development.json
  

 
 #### Technology:
* .NetCore
* SignalR
* EntityFramework - code first 
* JWT
* WebApi
* NUnit
* Redis
