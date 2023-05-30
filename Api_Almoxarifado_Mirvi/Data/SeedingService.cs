using Api_Almoxarifado_Mirvi.Models.Enums;
namespace Api_Almoxarifado_Mirvi.Models;

public class SeedingService
{
    private Api_Almoxarifado_MirviContext _context;

    public SeedingService(Api_Almoxarifado_MirviContext context)
    {
        _context = context;
    }

    public void Seed(IServiceProvider serviceProvider)
    {
        if (_context.Almoxarifado.Any() ||
            _context.Corredor.Any() ||
            _context.Prateleira.Any() ||
            _context.Endereco.Any() ||
            _context.Produto.Any())
        {
            return; //O Banco de dados ja foi populado
        }

        Almoxarifado a1 = new Almoxarifado(1, "Mirvi Brasil");

        Corredor c1 = new Corredor(1, "A", a1);
        Corredor c2 = new Corredor(2, "B",a1);
        Corredor c3 = new Corredor(3, "C", a1);

        Prateleira p1 = new Prateleira(1, "A1", c1);
        Prateleira p2 = new Prateleira(2, "A2", c2);
        Prateleira p3 = new Prateleira(3, "A3", c3);

        Endereco e1 = new Endereco(1, "AF1", p1);
        Endereco e2 = new Endereco(2, "AF2", p2);
        Endereco e3 = new Endereco(3, "AF3", p3);

        Produto pr1 = new Produto(1, e1, p1, "M8", "", ProdutoStatus.NoLimite, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 1);
        Produto pr2 = new Produto(2, e2, p2, "M5", "", (ProdutoStatus)1, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 1);
        Produto pr3 = new Produto(3, e3, p3, "M6", "", (ProdutoStatus)2, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 1);

        _context.Almoxarifado.AddRange(a1);

        _context.Corredor.AddRange(c1, c2, c3);

        _context.Prateleira.AddRange(p1, p2, p3);

        _context.Endereco.AddRange(e1, e2, e3);

        _context.Produto.AddRange(pr1, pr2, pr3);

        _context.SaveChanges();
    }
}