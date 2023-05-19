namespace Api_Almoxarifado_Mirvi.Models
{
    public class Corredores
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Almoxarifado Almoxarifado { get; set; }
        public ICollection<Prateleiras> Prateleiras { get; set; } = new List<Prateleiras>();

        public Corredores()
        {
        }

        public Corredores(int id, string descricao, Almoxarifado almoxarifado)
        {
            Id = id;
            Descricao = descricao;
            Almoxarifado = almoxarifado;
        }

        public void AddCorredor(Prateleiras pr)
        {
            Prateleiras.Add(pr);
        }

        public void RemoveCorredor(Prateleiras pr)
        {
            Prateleiras.Remove(pr);
        }
    }
}
