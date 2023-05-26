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
    [Migration("20230524155317_ChavesEstrangeiras")]
    partial class ChavesEstrangeiras
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

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Corredor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlmoxarifadoId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AlmoxarifadoId");

                    b.ToTable("Corredor");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PrateleirasId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrateleirasId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Prateleira", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CorredorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CorredorId");

                    b.ToTable("Prateleira");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Diametro")
                        .HasColumnType("longtext");

                    b.Property<int?>("EnderecosId")
                        .HasColumnType("int");

                    b.Property<string>("Fabricante")
                        .HasColumnType("longtext");

                    b.Property<string>("Fornecedor")
                        .HasColumnType("longtext");

                    b.Property<string>("H225_H300")
                        .HasColumnType("longtext");

                    b.Property<string>("Hpn")
                        .HasColumnType("longtext");

                    b.Property<string>("Marca")
                        .HasColumnType("longtext");

                    b.Property<string>("Medida")
                        .HasColumnType("longtext");

                    b.Property<string>("Modelo")
                        .HasColumnType("longtext");

                    b.Property<int>("PrateleirasId")
                        .HasColumnType("int");

                    b.Property<string>("Referencia")
                        .HasColumnType("longtext");

                    b.Property<string>("S_N")
                        .HasColumnType("longtext");

                    b.Property<string>("Uso")
                        .HasColumnType("longtext");

                    b.Property<string>("Valor")
                        .HasColumnType("longtext");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnderecosId");

                    b.HasIndex("PrateleirasId");

                    b.ToTable("Produto");
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

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Endereco", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Prateleira", "Prateleiras")
                        .WithMany("Endereco")
                        .HasForeignKey("PrateleirasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prateleiras");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Prateleira", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Corredor", "Corredor")
                        .WithMany("Prateleiras")
                        .HasForeignKey("CorredorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Corredor");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Produto", b =>
                {
                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Endereco", "Enderecos")
                        .WithMany("Produto")
                        .HasForeignKey("EnderecosId");

                    b.HasOne("Api_Almoxarifado_Mirvi.Models.Prateleira", "Prateleiras")
                        .WithMany("Produto")
                        .HasForeignKey("PrateleirasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enderecos");

                    b.Navigation("Prateleiras");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Almoxarifado", b =>
                {
                    b.Navigation("Corredor");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Corredor", b =>
                {
                    b.Navigation("Prateleiras");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Endereco", b =>
                {
                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Api_Almoxarifado_Mirvi.Models.Prateleira", b =>
                {
                    b.Navigation("Endereco");

                    b.Navigation("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}
