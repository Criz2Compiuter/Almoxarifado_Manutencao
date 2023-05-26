namespace Api_Almoxarifado_Mirvi.Models
{
    public class Prateleira
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Corredor Corredor { get; set; }
        public int CorredorId { get; set; }
        public ICollection<Endereco> Endereco { get; set; }
        public ICollection<Produto>? Produto { get; set; }

        public Prateleira()
        {
        }

        public Prateleira(int id, string nome, Corredor corredor)
        {
            Id = id;
            Nome = nome;
            Corredor = corredor;
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
    }
}
