using Microsoft.EntityFrameworkCore;
using RaspBier.Helper;
using RaspBier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaspBier.Database
{
    public class CustomDBContext : DbContext
    {
        //Important Commands (run in packet manager console):
        //Add-Migration InitialCreate --> InitalCreate (this creates an "action" in the Migrations-folder
        //Update-Database -Verbose --> Update DB (run this to apply the created action on the database)

        //Command for puplish to rpi
        //dotnet publish -r linux-arm -c Release


        public CustomDBContext(DbContextOptions<CustomDBContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = SettingsHelper.Settings.DBPath;
            optionsBuilder.UseSqlite(String.Format("Data Source={0}", dbPath));
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorValue> SensorValues { get; set; }
        public DbSet<RaspBier.Models.Error> Errors { get; set; }
        public DbSet<RaspBier.Models.NotificationEntry> NotificationEntry { get; set; }
    }
}
