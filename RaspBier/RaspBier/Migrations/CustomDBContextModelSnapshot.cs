﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RaspBier.Database;

namespace RaspBier.Migrations
{
    [DbContext(typeof(CustomDBContext))]
    partial class CustomDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("RaspBier.Models.Error", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ErrorType");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<int>("SensorID");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("ID");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("RaspBier.Models.Sensor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ActionThreshold");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("RaspBier.Models.SensorValue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SensorID");

                    b.Property<int>("SensorType");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<decimal>("Value");

                    b.HasKey("ID");

                    b.ToTable("SensorValues");
                });
#pragma warning restore 612, 618
        }
    }
}
