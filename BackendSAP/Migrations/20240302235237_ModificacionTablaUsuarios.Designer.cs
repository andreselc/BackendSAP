﻿// <auto-generated />
using System;
using BackendSAP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendSAP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240302235237_ModificacionTablaUsuarios")]
    partial class ModificacionTablaUsuarios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackendSAP.Modelos.Ciudades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Ciudades");
                });

            modelBuilder.Entity("BackendSAP.Modelos.Estados", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("BackendSAP.Modelos.Usuarios", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Calle_Av")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CiudadId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripcionPsicologo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Experiencia")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Formacion")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<byte[]>("ImagenPerfil")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("ImagenTitulo")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumeroColegiatura")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TelefonOficina")
                        .HasColumnType("int");

                    b.Property<string>("TipoTerapia")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("BackendSAP.Modelos.Ciudades", b =>
                {
                    b.HasOne("BackendSAP.Modelos.Estados", "Estados")
                        .WithMany("Ciudades")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estados");
                });

            modelBuilder.Entity("BackendSAP.Modelos.Usuarios", b =>
                {
                    b.HasOne("BackendSAP.Modelos.Ciudades", "Ciudades")
                        .WithMany("Usuarios")
                        .HasForeignKey("CiudadId");

                    b.Navigation("Ciudades");
                });

            modelBuilder.Entity("BackendSAP.Modelos.Ciudades", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("BackendSAP.Modelos.Estados", b =>
                {
                    b.Navigation("Ciudades");
                });
#pragma warning restore 612, 618
        }
    }
}
