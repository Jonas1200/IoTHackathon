using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace RaspWeed
{
    class RaspWeedManager
    {

        GpioPin doorPin = Pi.Gpio.Pin29;
        DateTime lastDoorOpened;


        public RaspWeedManager()
        {
            doorPin.PinMode = GpioPinDriveMode.Input;
        }

        public void Run()
        {
            while (true)
            {
                if (DoorIsOpen())
                    DoorOpen();

                Thread.Sleep(1000);
            }
        }

        public bool DoorIsOpen()
        {
            var value = !doorPin.Read();
            Console.WriteLine(String.Format("Value: {0}", value));
            return value;
        }

        public void DoorOpen()
        {
            if ((DateTime.Now - lastDoorOpened).TotalMinutes < 1)
            {
                Console.WriteLine("Door is open, but Time under 1 Minute");
                return;
            }

            lastDoorOpened = DateTime.Now;
            SendMail();
        }

        public void SendMail()
        {
            Console.WriteLine("Door is open, send mail!");

            var client = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 25,
                EnableSsl = true
            };

            client.Credentials = new NetworkCredential("raspbier@gmail.com", "123PpP++");

            var mailMessage = new MailMessage
            {
                From = new MailAddress("raspbier@gmail.com")
            };

            mailMessage.To.Add("mol@acadon.de");
            mailMessage.Body = "Geiler SHIT!";
            mailMessage.Subject = "GEILER SHITTTY!";
            client.Send(mailMessage);
        }
    }
}
