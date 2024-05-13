﻿// <auto-generated />
using ChessAPI.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChessAPI.Migrations
{
    [DbContext(typeof(ChessAPIDbContext))]
    [Migration("20240513100010_NewConnectionString")]
    partial class NewConnectionString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ChessAPI.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BlackPlayer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TurnCount")
                        .HasColumnType("int");

                    b.Property<string>("WhitePlayer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("ChessAPI.Models.Piece", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Pieces");
                });

            modelBuilder.Entity("ChessAPI.Models.Piece", b =>
                {
                    b.HasOne("ChessAPI.Models.Game", "Game")
                        .WithMany("Pieces")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("ChessAPI.Models.Game", b =>
                {
                    b.Navigation("Pieces");
                });
#pragma warning restore 612, 618
        }
    }
}