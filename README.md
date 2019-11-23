# TC
 Setup local
 what you need to start:
 1. visual studio 2019
 2. visual code
 3. Mssql
 4. WSL - how to setup https://docs.microsoft.com/en-us/windows/wsl/install-win10#install-your-linux-distribution-of-choice
 5. Angular https://angular.io/guide/setup-local
 6. .Net core 3.0 https://dotnet.microsoft.com/download
 
 # Setup:
 1. Clone git
 2. Create local database
 2a. Run TC/recreate-db.bat - script will create database and seed data.
 3. Setup redis (required WSL)
 3a. Follow setup in TC/Redis-setup.txt
 
 # Start coding - front end
 1. Open vscode 
 2. Go to \TC\TC.FrontEnd\Angular
 4. npm install
 5. Run ng serve
 Setup chrome extension 
 1. Go to \TC\TC.FrontEnd\Angular
 2. Run ng build tc-browser-recorder
 1. Open Chrome
 2. Settings -> Extnesnions -> turn on "Developer mode"
 3. "Load unpacked" set path \TC\TC.FrontEnd\Angular\dist\tc-browser-recorder
 
 #Back end
 Project architecture:
 ...
 Technology:
 .NetCore
 SignalR
 EntityFramework
 JWT
 WebApi
 NUnit
 
