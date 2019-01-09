﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SamuraiCore.Data;

namespace SamuraiCore.Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20181203104723_samuraicore.initial")]
    partial class samuraicoreinitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SamuraiCore.Entity.Battle", b =>
                {
                    b.Property<int>("BattleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("BattleId");

                    b.ToTable("Battle");
                });

            modelBuilder.Entity("SamuraiCore.Entity.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SamuraiId");

                    b.Property<string>("Text");

                    b.HasKey("QuoteId");

                    b.HasIndex("SamuraiId");

                    b.ToTable("Quote");
                });

            modelBuilder.Entity("SamuraiCore.Entity.Samurai", b =>
                {
                    b.Property<int>("SamuraiId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleId");

                    b.Property<string>("Name");

                    b.HasKey("SamuraiId");

                    b.HasIndex("BattleId");

                    b.ToTable("Samurai");
                });

            modelBuilder.Entity("SamuraiCore.Entity.Quote", b =>
                {
                    b.HasOne("SamuraiCore.Entity.Samurai", "Samurai")
                        .WithMany("Quotes")
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SamuraiCore.Entity.Samurai", b =>
                {
                    b.HasOne("SamuraiCore.Entity.Battle")
                        .WithMany("Samurais")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}