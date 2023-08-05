﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pijze.Infrastructure.Data.EntityFramework;

#nullable disable

namespace Pijze.Infrastructure.Data.EntityFramework.Migrations
{
    [DbContext(typeof(PijzeDbContext))]
    [Migration("20230309185608_AddBreweries")]
    partial class AddBreweries
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Pijze.Domain.Beers.Beer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BreweryId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("Pijze.Domain.Breweries.Brewery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Breweries");
                });

            modelBuilder.Entity("Pijze.Domain.Beers.Beer", b =>
                {
                    b.HasOne("Pijze.Domain.Breweries.Brewery", null)
                        .WithMany()
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Pijze.Domain.Beers.BeerImage", "Image", b1 =>
                        {
                            b1.Property<string>("BeerId")
                                .HasColumnType("TEXT");

                            b1.Property<byte[]>("Bytes")
                                .IsRequired()
                                .HasColumnType("BLOB")
                                .HasColumnName("Image");

                            b1.HasKey("BeerId");

                            b1.ToTable("Beers");

                            b1.WithOwner()
                                .HasForeignKey("BeerId");
                        });

                    b.Navigation("Image")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}