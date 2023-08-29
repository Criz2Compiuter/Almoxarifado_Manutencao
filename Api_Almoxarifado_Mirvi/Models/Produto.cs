using Api_Almoxarifado_Mirvi.Models.Enums;
using Api_Almoxarifado_Mirvi.Services;
using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Produto
    {

        public int? Id { get; set; }
        public string? Endereco { get; set; }
        public Prateleira Prateleiras { get; set; }
        public int? PrateleirasId { get; set; }
        public Corredor Corredor { get; set; }
        public int? CorredorId { get; set; }
        public Repartição? Repartição { get; set; }
        public int? RepartiçãoId { get; set; }
        public Maquina? Maquina { get; set; }
        public int? MaquinaId { get; set; }

        public Almoxarifado Almoxarifado { get; set; }
        public int? AlmoxarifadoId { get; set; }
        [Required(ErrorMessage = "{0} nao informado")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "A {0} do produto deve ter de {2} a {1} caracter")]
        public string Descricao { get; set; }
        public string? Categoria { get; set; }
        public ProdutoStatus Status { get; set; }
        public int? Minimo { get; set; }
        public int? Maximo { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} não informado")]
        public DateTime Data { get; set; }
        public string? CodigoDeCompra { get; set; }
        public string? Local { get; set; }
        public string? Linha { get; set; }
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
        public decimal Valor { get; set; }
        public string? Modelo { get; set; }
        [DisplayFormat(DataFormatString = "{0}")]
        [Range(1, 1000, ErrorMessage = "a {0} do produto deve ter no minimo {1} e no maximo {2}")]
        [Required(ErrorMessage = "{0} não informado")]
        public int Quantidade { get; set; }
        public string? QuantidadeTotalInstalada { get; set; }
        public byte[] FotoPDF { get; set; }
        public List<HistoricoDesconto> HistoricoDescontos { get; set; }

        public Produto()
        {
        }

        public Produto(int? id, string? Endereco, Prateleira prateleiras, int? prateleirasId, Corredor corredor, int? corredorId, Repartição? repartição, int? repartiçãoId, Maquina? maquina, int? maquinaId,
            Almoxarifado almoxarifado, int? almoxarifadoId, string descricao, string? categoria, ProdutoStatus status, int? minimo, int? maximo, DateTime data, string? codigoDeCompra, string? local, string? linha, string? c_STalisca,
            string? hpn, string? referencia, string? h225_H300, string? fornecedor, string? diametro, string? comprimento, string? conexao, string? medida, string? fabricante, string? marca, string? n, decimal valor, string? modelo,
            int quantidade, string? quantidadeTotalInstalada, byte[] foto)
        {
            Id = id;
            Prateleiras = prateleiras;
            PrateleirasId = prateleirasId;
            Corredor = corredor;
            CorredorId = corredorId;
            Repartição = repartição;
            RepartiçãoId = repartiçãoId;
            Maquina = maquina;
            MaquinaId = maquinaId;
            Almoxarifado = almoxarifado;
            AlmoxarifadoId = almoxarifadoId;
            Descricao = descricao;
            Categoria = categoria;
            Status = status;
            Minimo = minimo;
            Maximo = maximo;
            Data = data;
            CodigoDeCompra = codigoDeCompra;
            Local = local;
            Linha = linha;
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
            Quantidade = quantidade;
            QuantidadeTotalInstalada = quantidadeTotalInstalada;
            HistoricoDescontos = new List<HistoricoDesconto>();
            FotoPDF = foto;
        }

        public void RegistrarDesconto(string nomeUsuario, int quantidadeDescontada)
        {
            if (HistoricoDescontos == null)
                HistoricoDescontos = new List<HistoricoDesconto>();

            var historicoDesconto = new HistoricoDesconto
            {
                NomeUsuario = nomeUsuario,
                DataDesconto = DateTime.Now,
                ProdutoId = Id.Value,
                QuantidadeDescontada = quantidadeDescontada
            };

            HistoricoDescontos.Add(historicoDesconto);
        }
        public void AtualizarStatus()
        {
            if (Quantidade < Minimo)
            {
                Status = ProdutoStatus.Indisponivel;
                Data = DateTime.Now;
            }
            else if (Quantidade >= Minimo && Quantidade < Maximo)
            {
                Status = ProdutoStatus.LimiteBaixo;
                Data = DateTime.Now;
            }
            else
            {
                Status = ProdutoStatus.Disponivel;
                Data = DateTime.Now;
            }
        }
    }
}
