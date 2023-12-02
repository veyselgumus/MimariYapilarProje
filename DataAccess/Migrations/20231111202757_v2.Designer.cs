﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(MimariYapilarContext))]
    [Migration("20231111202757_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Entities.Kullanici", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AktifMi")
                        .HasMaxLength(50)
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("Sifre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("DataAccess.Entities.Mimar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("DogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("YasiyorMu")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Mimarlar");
                });

            modelBuilder.Entity("DataAccess.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roller");
                });

            modelBuilder.Entity("DataAccess.Entities.Tur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Turler");
                });

            modelBuilder.Entity("DataAccess.Entities.Yapi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BulunduğuUlke")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MimarId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("YapimYili")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MimarId");

                    b.ToTable("Yapilar");
                });

            modelBuilder.Entity("DataAccess.Entities.YapiDetay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InsaatAlani")
                        .HasColumnType("int");

                    b.Property<string>("Konsepti")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("TasiyiciSistem")
                        .HasColumnType("int");

                    b.Property<int>("YapiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("YapiId")
                        .IsUnique();

                    b.ToTable("YapiDetaylar");
                });

            modelBuilder.Entity("DataAccess.Entities.YapiTur", b =>
                {
                    b.Property<int>("TurId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("YapiId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.HasKey("TurId", "YapiId");

                    b.HasIndex("YapiId");

                    b.ToTable("YapiTurler");
                });

            modelBuilder.Entity("DataAccess.Entities.Kullanici", b =>
                {
                    b.HasOne("DataAccess.Entities.Rol", "Rol")
                        .WithMany("Kullanicilar")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("DataAccess.Entities.Yapi", b =>
                {
                    b.HasOne("DataAccess.Entities.Mimar", "Mimar")
                        .WithMany("Yapilari")
                        .HasForeignKey("MimarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mimar");
                });

            modelBuilder.Entity("DataAccess.Entities.YapiDetay", b =>
                {
                    b.HasOne("DataAccess.Entities.Yapi", "Yapi")
                        .WithOne("YapiDetay")
                        .HasForeignKey("DataAccess.Entities.YapiDetay", "YapiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Yapi");
                });

            modelBuilder.Entity("DataAccess.Entities.YapiTur", b =>
                {
                    b.HasOne("DataAccess.Entities.Tur", "Tur")
                        .WithMany("YapiTurler")
                        .HasForeignKey("TurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Yapi", "Yapi")
                        .WithMany("YapiTurler")
                        .HasForeignKey("YapiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tur");

                    b.Navigation("Yapi");
                });

            modelBuilder.Entity("DataAccess.Entities.Mimar", b =>
                {
                    b.Navigation("Yapilari");
                });

            modelBuilder.Entity("DataAccess.Entities.Rol", b =>
                {
                    b.Navigation("Kullanicilar");
                });

            modelBuilder.Entity("DataAccess.Entities.Tur", b =>
                {
                    b.Navigation("YapiTurler");
                });

            modelBuilder.Entity("DataAccess.Entities.Yapi", b =>
                {
                    b.Navigation("YapiDetay");

                    b.Navigation("YapiTurler");
                });
#pragma warning restore 612, 618
        }
    }
}
