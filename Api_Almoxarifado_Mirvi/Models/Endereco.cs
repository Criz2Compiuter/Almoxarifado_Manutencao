using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Insira o {0} do endereco para cadastro")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "O {0} do endereco deve ter no maximo 20 caracter")]
        public string Nome { get; set; }
        public Prateleira Prateleiras { get; set; }
        public int? PrateleirasId { get; set; }
        public Corredor Corredor { get; set; }
        public int? CorredorId { get; set; }
        public Almoxarifado Almoxarifado { get; set; }
        public int? AlmoxarifadoId { get; set; }
        public ICollection<Produto>? Produto { get; set; }

        public Endereco()
        {
            
        }

        public Endereco(int id, string nome, Prateleira prateleiras, int? prateleiraId, Almoxarifado almoxarifado, int? almoxarifadoId, Corredor corredor, int? corredorId)
        {
            Id = id;
            Nome = nome;
            PrateleirasId = prateleiraId;
            Prateleiras = prateleiras;
            Almoxarifado = almoxarifado;
            AlmoxarifadoId = almoxarifadoId;
            Corredor = corredor;
            CorredorId = corredorId;
        }
        public void AddEnderecos(Produto pr)
        {
            Produto.Add(pr);
        }

        public void RemoveEnderecos(Produto pr)
        {
            Produto.Remove(pr);
        }
        public double TotalObjeto(DateTime initial, DateTime final)
        {
            return Produto.Where(sr => sr.Data >= initial && sr.Data <= final).Sum(sr => sr.Quantidade);
        }
    }
}
