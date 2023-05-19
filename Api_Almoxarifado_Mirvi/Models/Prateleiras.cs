namespace Api_Almoxarifado_Mirvi.Models
{
    public class Prateleiras
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Corredores Corredores { get; set; }
        public ICollection<Enderecos> Enderecos { get; set; }
        public ICollection<Produto> Produto { get; set; }

        public Prateleiras()
        {
        }

        public Prateleiras(int id, string nome, Corredores corredores)
        {
            Id = id;
            Nome = nome;
            Corredores = corredores;
        }



        public void AddProdutoPrateleira(Produto pr)
        {
            Produto.Add(pr);
        }

        public void RemoveProdutoPrateleira(Produto pr)
        {
            Produto.Remove(pr);
        }

        public void AddEnderecoPrateleira(Enderecos end)
        {
            Enderecos.Add(end);
        }

        public void RemoveCorredor(Enderecos end)
        {
            Enderecos.Remove(end);
        }
    }
}
