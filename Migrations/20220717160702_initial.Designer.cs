﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppinglistApp.Data;

namespace ShoppinglistApp.Migrations
{
    [DbContext(typeof(ShoppingListContext))]
    [Migration("20220717160702_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShoppinglistApp.Models.ShoppingListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ShoppingListItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingListItemId");

                    b.ToTable("ShoppingListItems");
                });

            modelBuilder.Entity("ShoppinglistApp.Models.Shoppinglist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ListName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID1");

                    b.ToTable("Shoppinglists");
                });

            modelBuilder.Entity("ShoppinglistApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShoppinglistApp.Models.ShoppingListItem", b =>
                {
                    b.HasOne("ShoppinglistApp.Models.ShoppingListItem", null)
                        .WithMany("items")
                        .HasForeignKey("ShoppingListItemId");
                });

            modelBuilder.Entity("ShoppinglistApp.Models.Shoppinglist", b =>
                {
                    b.HasOne("ShoppinglistApp.Models.User", null)
                        .WithMany("Shoppinglists")
                        .HasForeignKey("UserID1");
                });

            modelBuilder.Entity("ShoppinglistApp.Models.ShoppingListItem", b =>
                {
                    b.Navigation("items");
                });

            modelBuilder.Entity("ShoppinglistApp.Models.User", b =>
                {
                    b.Navigation("Shoppinglists");
                });
#pragma warning restore 612, 618
        }
    }
}