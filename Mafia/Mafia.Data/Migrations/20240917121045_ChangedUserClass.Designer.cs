﻿// <auto-generated />
using System;
using Mafia.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mafia.Data.Migrations
{
    [DbContext(typeof(MafiaContext))]
    [Migration("20240917121045_ChangedUserClass")]
    partial class ChangedUserClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mafia.Domain.DbModels.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FriendUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FriendUserId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("Mafia.Domain.DbModels.GameRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleName")
                        .HasColumnType("int");

                    b.Property<string>("Statuses")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("countVotes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Mafia.Domain.DbModels.Statistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("Mafia.Domain.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int?>("StatisticId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameRoleId");

                    b.HasIndex("StatisticId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Mafia.Domain.DbModels.Friend", b =>
                {
                    b.HasOne("Mafia.Domain.DbModels.User", "FriendUser")
                        .WithMany()
                        .HasForeignKey("FriendUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mafia.Domain.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mafia.Domain.DbModels.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId1");

                    b.Navigation("FriendUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mafia.Domain.DbModels.User", b =>
                {
                    b.HasOne("Mafia.Domain.DbModels.GameRole", "GameRole")
                        .WithMany()
                        .HasForeignKey("GameRoleId");

                    b.HasOne("Mafia.Domain.DbModels.Statistic", "Statistic")
                        .WithMany()
                        .HasForeignKey("StatisticId");

                    b.Navigation("GameRole");

                    b.Navigation("Statistic");
                });

            modelBuilder.Entity("Mafia.Domain.DbModels.User", b =>
                {
                    b.Navigation("Friends");
                });
#pragma warning restore 612, 618
        }
    }
}
