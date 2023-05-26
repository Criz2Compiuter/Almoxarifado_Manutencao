namespace Api_Almoxarifado_Mirvi.Models
{
    public class Almoxarifado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Corredor> Corredor { get; set; } = new List<Corredor>();

        public Almoxarifado()
        {

        }

        public Almoxarifado(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
