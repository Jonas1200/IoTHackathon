﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RaspBier.Database;

namespace RaspBier.Migrations
{
    [DbContext(typeof(CustomDBContext))]
    [Migration("20181103165425_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("RaspBier.Models.Sensor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

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

                    b.Property<int>("Value");

                    b.HasKey("ID");

                    b.ToTable("SensorValues");
                });
#pragma warning restore 612, 618
        }
    }
}
