using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Prateleira
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira o Nome da Prateleira para cadastro")]
        [StringLength(20, ErrorMessage = "O {0} da prateleira deve ter {1} caracter")]
        public string Nome { get; set; }
        public Corredor Corredor { get; set; }
        public int CorredorId { get; set; }
        public Almoxarifado Almoxarifado { get; set; }
        public ICollection<Endereco> Endereco { get; set; }
        public ICollection<Produto>? Produto { get; set; }

        public Prateleira()
        {
        }

        public Prateleira(int id, string nome, Corredor corredor, Almoxarifado almoxarifado)
        {
            Id = id;
            Nome = nome;
            Corredor = corredor;
            Almoxarifado = almoxarifado;
        }



        public void AddProdutoPrateleira(Produto pr)
        {
            Produto.Add(pr);
        }
        
        public void RemoveProdutoPrateleira(Produto pr)
        {
            Produto.Remove(pr);
        }

        public void AddEnderecoPrateleira(Endereco end)
        {
            Endereco.Add(end);
        }

        public void RemovePrateleira(Endereco end)
        {
            Endereco.Remove(end);
        }

        public double TotalObjeto(DateTime initial, DateTime final)
        {
            return Produto.Where(sr => sr.Data >= initial && sr.Data <= final).Sum(sr => sr.Quantidade);
        }
    }
}
