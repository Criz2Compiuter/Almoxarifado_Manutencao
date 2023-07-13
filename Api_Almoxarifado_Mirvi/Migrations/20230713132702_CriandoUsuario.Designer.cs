﻿// <auto-generated />
using System;
using Api_Almoxarifado_Mirvi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api_Almoxarifado_Mirvi.Migrations
{
    [DbContext(typeof(Api_Almoxarifado_MirviContext))]
    [Migration("20230713132702_CriandoUsuario")]
    partial class CriandoUsuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Almoxarifado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Almoxarifado");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Corredor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlmoxarifadoId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AlmoxarifadoId");

                    b.ToTable("Corredor");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Maquina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlmoxarifadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AlmoxarifadoId");

                    b.ToTable("Maquina");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Prateleira", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlmoxarifadoId")
                        .HasColumnType("int");

                    b.Property<int>("CorredorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AlmoxarifadoId");

                    b.HasIndex("CorredorId");

                    b.ToTable("Prateleira");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Produto", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AlmoxarifadoId")
                        .HasColumnType("int");

                    b.Property<string>("C_STalisca")
                        .HasColumnType("longtext");

                    b.Property<string>("Categoria")
                        .HasColumnType("longtext");

                    b.Property<string>("CodigoDeCompra")
                        .HasColumnType("longtext");

                    b.Property<string>("Comprimento")
                        .HasColumnType("longtext");

                    b.Property<string>("Conexao")
                        .HasColumnType("longtext");

                    b.Property<int?>("CorredorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Diametro")
                        .HasColumnType("longtext");

                    b.Property<string>("Endereco")
                        .HasColumnType("longtext");

                    b.Property<string>("Fabricante")
                        .HasColumnType("longtext");

                    b.Property<string>("Fornecedor")
                        .HasColumnType("longtext");

                    b.Property<string>("H225_H300")
                        .HasColumnType("longtext");

                    b.Property<string>("Hpn")
                        .HasColumnType("longtext");

                    b.Property<string>("Linha")
                        .HasColumnType("longtext");

                    b.Property<string>("Local")
                        .HasColumnType("longtext");

                    b.Property<int?>("MaquinaId")
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .HasColumnType("longtext");

                    b.Property<int?>("Maximo")
                        .HasColumnType("int");

                    b.Property<string>("Medida")
                        .HasColumnType("longtext");

                    b.Property<int?>("Minimo")
                        .HasColumnType("int");

                    b.Property<string>("Modelo")
                        .HasColumnType("longtext");

                    b.Property<int?>("PrateleirasId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("QuantidadeTotalIntalada")
                        .HasColumnType("longtext");

                    b.Property<string>("Referencia")
                        .HasColumnType("longtext");

                    b.Property<int?>("RepartiçãoId")
                        .HasColumnType("int");

                    b.Property<string>("S_N")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Valor")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AlmoxarifadoId");

                    b.HasIndex("CorredorId");

                    b.HasIndex("MaquinaId");

                    b.HasIndex("PrateleirasId");

                    b.HasIndex("RepartiçãoId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Repartição", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlmoxarifadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AlmoxarifadoId");

                    b.ToTable("Repartição");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Corredor", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Almoxarifado", "Almoxarifado")
                        .WithMany("Corredor")
                        .HasForeignKey("AlmoxarifadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Almoxarifado");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Maquina", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Almoxarifado", "Almoxarifado")
                        .WithMany()
                        .HasForeignKey("AlmoxarifadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Almoxarifado");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Prateleira", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Almoxarifado", "Almoxarifado")
                        .WithMany("Prateleira")
                        .HasForeignKey("AlmoxarifadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Corredor", "Corredor")
                        .WithMany("Prateleiras")
                        .HasForeignKey("CorredorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Almoxarifado");

                    b.Navigation("Corredor");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Produto", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Almoxarifado", "Almoxarifado")
                        .WithMany("Produto")
                        .HasForeignKey("AlmoxarifadoId");

                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Corredor", "Corredor")
                        .WithMany("Produto")
                        .HasForeignKey("CorredorId");

                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Maquina", "Maquina")
                        .WithMany("Produto")
                        .HasForeignKey("MaquinaId");

                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Prateleira", "Prateleiras")
                        .WithMany("Produto")
                        .HasForeignKey("PrateleirasId");

                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Repartição", "Repartição")
                        .WithMany("Produto")
                        .HasForeignKey("RepartiçãoId");

                    b.Navigation("Almoxarifado");

                    b.Navigation("Corredor");

                    b.Navigation("Maquina");

                    b.Navigation("Prateleiras");

                    b.Navigation("Repartição");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Repartição", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Almoxarifado", "Almoxarifado")
                        .WithMany()
                        .HasForeignKey("AlmoxarifadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Almoxarifado");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Almoxarifado", b =>
                {
                    b.Navigation("Corredor");

                    b.Navigation("Prateleira");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Corredor", b =>
                {
                    b.Navigation("Prateleiras");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Maquina", b =>
                {
                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Prateleira", b =>
                {
                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Repartição", b =>
                {
                    b.Navigation("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}
