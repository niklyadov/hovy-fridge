﻿// <auto-generated />
using System;
using HovyFridge.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HovyFridgeApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HovyFridgeApi.Data.Entity.Fridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long?>("DeleteTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Fridges");
                });

            modelBuilder.Entity("HovyFridgeApi.Data.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<short>("Amount")
                        .HasColumnType("smallint");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("CreatedTimestamp")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeleteTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("FridgeId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long?>("LastEditedTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("UnitType")
                        .HasColumnType("integer");

                    b.Property<int>("UnitValue")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FridgeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("HovyFridgeApi.Data.Entity.ProductSuggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("DeleteTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProductSuggestion");
                });

            modelBuilder.Entity("HovyFridgeApi.Data.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long?>("DeleteTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long?>("LastLoginTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("RegistrationTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("TelegramChatId")
                        .HasColumnType("text");

                    b.Property<long?>("UserRole")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HovyFridgeApi.Data.Entity.Product", b =>
                {
                    b.HasOne("HovyFridgeApi.Data.Entity.Fridge", "Fridge")
                        .WithMany("Products")
                        .HasForeignKey("FridgeId");

                    b.Navigation("Fridge");
                });

            modelBuilder.Entity("HovyFridgeApi.Data.Entity.Fridge", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
