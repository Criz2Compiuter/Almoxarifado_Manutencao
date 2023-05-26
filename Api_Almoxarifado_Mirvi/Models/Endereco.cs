namespace Api_Almoxarifado_Mirvi.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Prateleira Prateleiras { get; set; }
        public int PrateleirasId { get; set; }
        public ICollection<Produto>? Produto { get; set; }

        public Endereco()
        {
            
        }

        public Endereco(int id, string nome, Prateleira prateleiras)
        {
            Id = id;
            Nome = nome;
            Prateleiras = prateleiras;
        }
        public void AddEnderecos(Produto pr)
        {
            Produto.Add(pr);
        }

        public void RemoveEnderecos(Produto pr)
        {
            Produto.Remove(pr);
        }
    }
}
