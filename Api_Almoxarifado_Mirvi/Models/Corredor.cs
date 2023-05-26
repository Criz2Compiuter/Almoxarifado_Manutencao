using System.Collections.Generic;
namespace Api_Almoxarifado_Mirvi.Models
{
    public class Corredor
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Almoxarifado Almoxarifado { get; set; }
        public int AlmoxarifadoId { get; set; }
        public ICollection<Prateleira> Prateleiras { get; set; } = new List<Prateleira>();

        public Corredor()
        {
        }

        public Corredor(int id, string descricao, Almoxarifado almoxarifado)
        {
            Id = id;
            Descricao = descricao;
            Almoxarifado = almoxarifado;
        }

        public void AddCorredor(Prateleira cr)
        {
            Prateleiras.Add(cr);
        }

        public void RemoveCorredor(Prateleira cr)
        {
            Prateleiras.Remove(cr);
        }
    }
}
