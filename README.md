# IoTHackathon

##Aquaponik

###Getting started

Important Commands (run in packet manager console):

1. InitalCreate (this creates an "action" in the Migrations-folder:

    Add-Migration InitialCreate

1. Update DB (run this to apply the created action on the database)
	
    Update-Database -Verbose 
	
1. Command for puplish to rpi

    dotnet publish -r linux-arm -c Release

1. copy (Project)\bin\Release\netcoreapp2.1\linux-arm\publish to /home/pi/RaspBier
	
1.  On Raspberry Pi change URL in /home/pi/RaspBier/customSettings.json!!!

1. (optional) Change database Foldername/Email

1. Install ASP .Net Core on Raspberry Pi 3

1. Set Rights for Project:

    chmod 755 ./RaspBier