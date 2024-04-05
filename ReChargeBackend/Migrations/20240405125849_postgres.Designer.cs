﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReCharge.Data;

#nullable disable

namespace ReChargeBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240405125849_postgres")]
    partial class postgres
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Entities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivityAdminTg")
                        .HasColumnType("text")
                        .HasColumnName("activity_admin_tg");

                    b.Property<string>("ActivityAdminWa")
                        .HasColumnType("text")
                        .HasColumnName("activity_admin_wa");

                    b.Property<string>("ActivityDescription")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("activity_description");

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("activity_name");

                    b.Property<string>("CancelationMessage")
                        .HasColumnType("text")
                        .HasColumnName("cancelation_message");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.Property<bool>("ShouldDisplayWarning")
                        .HasColumnType("boolean")
                        .HasColumnName("should_display_warning");

                    b.Property<string>("WarningText")
                        .HasColumnType("text")
                        .HasColumnName("warning_text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LocationId");

                    b.ToTable("activity");
                });

            modelBuilder.Entity("Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("Data.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressBuildingNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_building_number");

                    b.Property<string>("AddressCity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_city");

                    b.Property<double>("AddressLatitude")
                        .HasColumnType("double precision")
                        .HasColumnName("address_latitude");

                    b.Property<double>("AddressLongitude")
                        .HasColumnType("double precision")
                        .HasColumnName("address_longitude");

                    b.Property<string>("AddressNearestMetro")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_nearest_metro");

                    b.Property<string>("AddressStreet")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_street");

                    b.Property<string>("AdminTG")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("admin_ig");

                    b.Property<string>("AdminWA")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("admin_wa");

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("LocationDescription")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location_description");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location_name");

                    b.HasKey("Id");

                    b.ToTable("location");
                });

            modelBuilder.Entity("Data.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsOver")
                        .HasColumnType("boolean")
                        .HasColumnName("is_over");

                    b.Property<int>("SlotId")
                        .HasColumnType("integer")
                        .HasColumnName("slot_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.HasIndex("UserId");

                    b.ToTable("reservation");
                });

            modelBuilder.Entity("Data.Entities.Slot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer")
                        .HasColumnName("activity_id");

                    b.Property<int>("FreePlaces")
                        .HasColumnType("integer")
                        .HasColumnName("free_places");

                    b.Property<int>("LengthMinutes")
                        .HasColumnType("integer")
                        .HasColumnName("length_minutes");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("print");

                    b.Property<DateTime>("SlotDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_time");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("slot");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessHash")
                        .HasColumnType("text")
                        .HasColumnName("access_hash");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthdate");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Gender")
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Surname")
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Data.Entities.VerificationCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_datetime");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("session_id");

                    b.HasKey("Id");

                    b.ToTable("verification_code");
                });

            modelBuilder.Entity("Data.Entities.Activity", b =>
                {
                    b.HasOne("Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Data.Entities.Reservation", b =>
                {
                    b.HasOne("Data.Entities.Slot", "Slot")
                        .WithMany()
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Entities.Slot", b =>
                {
                    b.HasOne("Data.Entities.Activity", "Activity")
                        .WithMany("Slots")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("Data.Entities.Activity", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
