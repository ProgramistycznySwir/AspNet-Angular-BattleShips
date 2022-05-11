﻿// <auto-generated />
using System;
using HappyTeam_BattleShips.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HappyTeam_BattleShips.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("HappyTeam_BattleShips.Models.Game", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastMove")
                        .HasColumnType("TEXT");

                    b.Property<int>("Result")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Turn")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("HappyTeam_BattleShips.Models.GamePlayer", b =>
                {
                    b.Property<int>("subID")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Game_ID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Player_ID")
                        .HasColumnType("TEXT");

                    b.HasKey("subID", "Game_ID");

                    b.HasIndex("Game_ID");

                    b.HasIndex("Player_ID");

                    b.ToTable("GamePlayer");
                });

            modelBuilder.Entity("HappyTeam_BattleShips.Models.Player", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUsedTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PublicID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("PublicID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("HappyTeam_BattleShips.Models.TileData", b =>
                {
                    b.Property<Guid>("Game_ID")
                        .HasColumnType("TEXT");

                    b.Property<byte>("X")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Game_ID", "X", "Y");

                    b.ToTable("TileData");
                });

            modelBuilder.Entity("HappyTeam_BattleShips.Models.GamePlayer", b =>
                {
                    b.HasOne("HappyTeam_BattleShips.Models.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("Game_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HappyTeam_BattleShips.Models.Player", "Player")
                        .WithMany("Games")
                        .HasForeignKey("Player_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("HappyTeam_BattleShips.Models.TileData", b =>
                {
                    b.HasOne("HappyTeam_BattleShips.Models.Game", "Game")
                        .WithMany("BoardData")
                        .HasForeignKey("Game_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("HappyTeam_BattleShips.Models.Game", b =>
                {
                    b.Navigation("BoardData");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("HappyTeam_BattleShips.Models.Player", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
