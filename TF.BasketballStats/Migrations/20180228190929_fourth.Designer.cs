﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TF.BasketballStats;
using TF.BasketballStats.Database;

namespace TF.BasketballStats.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180228190929_fourth")]
    partial class fourth
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TF.BasketballStats.Database.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("TF.BasketballStats.Database.GameEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("GameTimeStamp");

                    b.Property<int>("MatchId");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Quarter");

                    b.Property<long>("QuarterTimeMS");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GameEvents");
                });

            modelBuilder.Entity("TF.BasketballStats.Database.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsHome");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("OpponentClubId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("OpponentClubId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("TF.BasketballStats.Database.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<DateTime>("MemberSince");

                    b.Property<string>("MiddleName");

                    b.Property<byte[]>("ProfilePicture");

                    b.Property<string>("ProfilePictureMime");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("TF.BasketballStats.Database.GameEvent", b =>
                {
                    b.HasOne("TF.BasketballStats.Database.Match", "Match")
                        .WithMany("GameEvents")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TF.BasketballStats.Database.Player", "Player")
                        .WithMany("GameEvents")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("TF.BasketballStats.Database.Match", b =>
                {
                    b.HasOne("TF.BasketballStats.Database.Club", "OpponentClub")
                        .WithMany("Matches")
                        .HasForeignKey("OpponentClubId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}