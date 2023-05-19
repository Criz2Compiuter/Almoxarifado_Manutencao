namespace Api_Almoxarifado_Mirvi.Models
{
    public class Almoxarifado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Corredores> Corredores { get; set; } = new List<Corredores>();

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
