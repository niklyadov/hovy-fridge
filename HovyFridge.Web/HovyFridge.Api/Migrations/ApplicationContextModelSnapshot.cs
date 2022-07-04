﻿// <auto-generated />
using HovyFridge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.Fridge", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("DeleteTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Fridges");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.FridgeAccessLevel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("AccessToEdit")
                        .HasColumnType("boolean");

                    b.Property<bool>("AccessToEditProductsList")
                        .HasColumnType("boolean");

                    b.Property<bool>("AccessToRead")
                        .HasColumnType("boolean");

                    b.Property<bool>("AccessToRemove")
                        .HasColumnType("boolean");

                    b.Property<long?>("DeleteTimestamp")
                        .HasColumnType("bigint");

                    b.Property<long>("FridgeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("FridgeAccessLevels");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

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

                    b.Property<long?>("FridgeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long?>("LastEditedTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("RecipeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ShoppingListId")
                        .HasColumnType("bigint");

                    b.Property<int>("UnitType")
                        .HasColumnType("integer");

                    b.Property<int>("UnitValue")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FridgeId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.ProductSuggestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

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

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.Recipe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

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

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.ShoppingList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

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

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

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

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.Product", b =>
                {
                    b.HasOne("HovyFridge.Api.Data.Entity.Fridge", "Fridge")
                        .WithMany("Products")
                        .HasForeignKey("FridgeId");

                    b.HasOne("HovyFridge.Api.Data.Entity.Recipe", null)
                        .WithMany("Products")
                        .HasForeignKey("RecipeId");

                    b.HasOne("HovyFridge.Api.Data.Entity.ShoppingList", null)
                        .WithMany("Products")
                        .HasForeignKey("ShoppingListId");

                    b.Navigation("Fridge");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.Fridge", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.Recipe", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("HovyFridge.Api.Data.Entity.ShoppingList", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
