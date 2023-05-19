﻿// <auto-generated />
using Api_Almoxarifado_Mirvi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api_Almoxarifado_Mirvi.Migrations
{
    [DbContext(typeof(Api_Almoxarifado_MirviContext))]
    [Migration("20230519123048_PrimeiraMigrationForDatabase")]
    partial class PrimeiraMigrationForDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Almoxarifado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Almoxarifado");
                });
#pragma warning restore 612, 618
        }
    }
}
