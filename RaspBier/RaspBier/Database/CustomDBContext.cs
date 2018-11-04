using Microsoft.EntityFrameworkCore;
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

        //private const string DATABASE_PATH = @"/home/pi/DB.db";

        private const string DATABASE_PATH = @"C:\TMP\DB.db";


        public CustomDBContext(DbContextOptions<CustomDBContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(String.Format("Data Source={0}", DATABASE_PATH));
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorValue> SensorValues { get; set; }
        public DbSet<RaspBier.Models.Error> Errors { get; set; }
    }
}
