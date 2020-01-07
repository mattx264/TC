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
 
 
 # Back end
 Project architecture:
 ![alt text](https://raw.githubusercontent.com/mattx264/TC/master/diagrams/Untitled%20Diagram.png)

 ...
 ### Start backend
 1. Open wsl ubuntu console and run `redis-server` (if redis is not setup - follow instruction in TC/Redis-setup.txt)
 1. Open TestingCenter.sln project file in Visual Studio (as a administrator)
 2. Set TC.WebService as a default project to start (right click)
 3. Start IIS express project TC.WebService (it should be by default)
 
 ### TC Browser Plugin
 #### Recording Web Session
 1. Open Chrome and a new tab
 2. Within the new tab, navigate to any website (e.g., www.google.com)
 3. Click on TC browser extension (you may have to login or create an account)
 4. Click on Sandbox button
 5. Navigate the website from the newly opened tab from step 2
 #### Testing Web Session
 1. Make sure backend is started (see above *Start Backend* step)
 2. Right-click on the project *TC.BrowserEngine* --> *Debug* --> *Start New Instance*
 3. When done recording web session (from above steps), click the *run test* button
 
 
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
