﻿using Api_Almoxarifado_Mirvi.Models.Enums;
using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public string? Categoria { get; set; }
        public ProdutoStatus status { get; set; }
        public DateTime Data { get; set; }
        public string? CodigoDeCompra { get; set; }
        public string? Uso { get; set; }
        public string? C_STalisca { get; set; }
        public string? Hpn { get; set; }
        public string? Referencia { get; set; }
        public string? H225_H300 { get; set; }
        public string? Fornecedor { get; set; }
        public string? Diametro { get; set; }
        public string? Comprimento { get; set; }
        public string? Conexao { get; set; }
        public string? Medida { get; set; }
        public string? Fabricante { get; set; }
        public string? Marca { get; set; }
        public string? S_N { get; set; }
        public string? Valor { get; set; }
        public string? Modelo { get; set; }
        public Enderecos? Enderecos { get; set; }
        public Prateleiras Prateleiras { get; set; }

        public Produto()
        {
        }

        public Produto(int id, string local, string descricao, string categoria, ProdutoStatus status,
            DateTime data, string codigoDeCompra, string uso, string c_STalisca, string hpn,
            string referencia, string h225_H300, string fornecedor, string diametro, string comprimento,
            string conexao, string medida, string fabricante, string marca, string n, string valor,
            string modelo, Enderecos? enderecos, Prateleiras prateleiras)
        {
            Id = id;
            Local = local;
            Descricao = descricao;
            Categoria = categoria;
            this.status = status;
            Data = data;
            CodigoDeCompra = codigoDeCompra;
            Uso = uso;
            C_STalisca = c_STalisca;
            Hpn = hpn;
            Referencia = referencia;
            H225_H300 = h225_H300;
            Fornecedor = fornecedor;
            Diametro = diametro;
            Comprimento = comprimento;
            Conexao = conexao;
            Medida = medida;
            Fabricante = fabricante;
            Marca = marca;
            S_N = n;
            Valor = valor;
            Modelo = modelo;
            Enderecos = enderecos;
            Prateleiras = prateleiras;
        }
    }
}
