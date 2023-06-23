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
        Almoxarifado a2 = new Almoxarifado(2, "Tetra Pak");

        Repartição r1 = new Repartição(1, "Servo Valvula", a1, 1);

        Maquina m1 = new Maquina(1, "Gima Helicap", a1, 1);

        Corredor c1 = new Corredor(1, "A", a1);
        Corredor c2 = new Corredor(2, "B",a1);
        Corredor c3 = new Corredor(3, "C", a1);

        Prateleira p1 = new Prateleira(1, "A1", c1, a1, 1);
        Prateleira p2 = new Prateleira(2, "A2", c2, a1, 1);
        Prateleira p3 = new Prateleira(3, "A3", c3, a2, 2);

        Endereco e1 = new Endereco(1, "AF1", p1, 1, a1, 1, c1, 1);
        Endereco e2 = new Endereco(2, "AF2", p2, 2, a1, 1, c2, 2);
        Endereco e3 = new Endereco(3, "AF3", p3, 3, a1, 1, c3, 3);

        Produto pr1 = new Produto(1, e1, 1, p1, 1, c1, 1, r1, 1, m1, 1, a1, 1, "M1", "", ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 5);
        Produto pr2 = new Produto(2, e2, 2, p2, 2, c2, 2, r1, 1, m1, 1, a1, 1, "M2", "", ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 5);
        Produto pr3 = new Produto(3, e3, 3, p3, 3, c3, 3, r1, 1, m1, 1, a1, 1, "M3", "", ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 5);
        Produto pr4 = new Produto(4, e3, 3, p3, 3, c3, 3, r1, 1, m1, 1, a2, 2, "M4", "", ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 5);
        Produto pr5 = new Produto(5, e3, 3, p3, 3, c3, 3, r1, 1, m1, 1, a2, 2, "M5", "", ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 5);
        Produto pr6 = new Produto(6, e3, 3, p3, 3, c3, 3, r1, 1, m1, 1, a2, 2, "M6", "", ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 5);

        _context.Almoxarifado.AddRange(a1);

        _context.Corredor.AddRange(c1, c2, c3);

        _context.Prateleira.AddRange(p1, p2, p3);

        _context.Endereco.AddRange(e1, e2, e3);

        _context.Produto.AddRange(pr1, pr2, pr3, pr4, pr5, pr6);

        _context.SaveChanges();
    }
}