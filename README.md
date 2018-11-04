# IoTHackathon

## Aquaponik

### Getting started

A Raspberry Pi 3 is needed for the Project.

1. Change Email Receiver and Email Sender Account in Visual Studio Project in RaspBier/customSettings.json

     `"MailHost": "raspbier@mail.com",`
     `"MailHostPassword": "<insert hostmail Password>"`
     `"MailReceiver": "test@test.com"`

2. [Install ASP .Net Core on Raspberry Pi 3](https://github.com/dotnet/core/blob/master/samples/RaspberryPiInstructions.md#linux)
3. Create Database:

  * (Optional) Change Foldername for Database if wanted in /RaspBier/customSettings.json:
	 
	 e.g: `"DBPath": "/home/pi/DB.db"`
	 
  * Open Visual Studio Projekt
  * Open packet Manager console View->More Windows->Packet Manager Console
  * Run following Commands in Packet Manager Console:
  1. InitalCreate (this creates an "action" in the Migrations-folder):
    
	`Add-Migration InitialCreate`
	
  2. Update DB (run this to apply the created action on the database)

    `Update-Database -Verbose`
	
4. Copy Released Project to Raspberry Pi

  1. Run following Commands in Packet Manager Console in Visual Studio:
	
  1. Publish Project with the following Command:

     `dotnet publish -r linux-arm -c Release`

  1. Copy ~\RaspBier\RaspBier\bin\Release\netcoreapp2.1\linux-arm\publish to /home/pi/RaspBier
	
  1.  On Raspberry Pi change URL in /home/pi/RaspBier/customSettings.json to Raspberry IP Address(Get IP Address on Pi with Command `ifconfig`)!!!

     e.g: `"Url": "http://192.168.10.118:50000",`

  1. Don't forget to set Rights for Project:

     `chmod 755 ./RaspBier`
	 
5. Start Raspberry Pi Webpage:

     `./RaspBier`
	