namespace Api_Almoxarifado_Mirvi.Models
{
    public class Enderecos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Prateleiras Prateleiras { get; set; }
        public ICollection<Produto>? Produto { get; set; }

        public Enderecos()
        {
            
        }

        public Enderecos(int id, string nome, Prateleiras prateleiras)
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
