namespace Api_Almoxarifado_Mirvi.Models;

public class SeedingService
{
    private Api_Almoxarifado_MirviContext _context;

    public SeedingService(Api_Almoxarifado_MirviContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Almoxarifado.Any() ||
            _context.Corredores.Any() ||
            _context.Prateleiras.Any() ||
            _context.Enderecos.Any() ||
            _context.Produtos.Any())
        {
            return; //O Banco de dados ja foi populado
        }

        Almoxarifado a1 = new Almoxarifado(1, "Mirvi Brasil");

        Corredores c1 = new Corredores(1, "A", a1);
        Corredores c2 = new Corredores(2, "B", a1);
        Corredores c3 = new Corredores(3, "C", a1);

        Prateleiras p1 = new Prateleiras(1, "A1", c1);
        Prateleiras p2 = new Prateleiras(2, "A2", c2);
        Prateleiras p3 = new Prateleiras(3, "A3", c3);

        Enderecos e1 = new Enderecos(1, "AF1", p1);
        Enderecos e2 = new Enderecos(2, "AF2", p2);
        Enderecos e3 = new Enderecos(3, "AF3", p3);

        Produto pr1 = new Produto(1, p1, "Parafuso", 0, 17.15, );
    }
}
