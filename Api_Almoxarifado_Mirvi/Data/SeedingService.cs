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
            _context.Produto.Any())
        {
            return;
        }

        string imagePath = "C:\\Users\\Cristian\\Downloads\\download (3).jpg";
        byte[] fotoBytes = File.ReadAllBytes(imagePath);

        Almoxarifado a1 = new Almoxarifado(1, "Mirvi Brasil");
        Almoxarifado a2 = new Almoxarifado(2, "Tetra Pak");

        Repartição r1 = new Repartição(1, "SERVO VALVULA", a1, 1);
        Repartição r2 = new Repartição(2, "PNEUMATICOS", a1, 1);
        Repartição r3 = new Repartição(3, "MATERIAIS ELETRICOS", a1, 1);
        Repartição r4 = new Repartição(4, "SENSORES", a1, 1);
        Repartição r5 = new Repartição(5, "CONEXOES HIDRAULICAS", a1, 1);
        Repartição r6 = new Repartição(6, "CORREIAS-V", a1, 1);
        Repartição r7 = new Repartição(7, "CORREIAS-V", a1, 1);
        Repartição r8 = new Repartição(8, "NETSAL", a1, 1);
        Repartição r9 = new Repartição(9, "FUSIVEIS", a1, 1);
        Repartição r10 = new Repartição(10, "CILINDROS", a1, 1);
        Repartição r11 = new Repartição(11, "CORREIAS-ELETRICAS", a1, 1);
        Repartição r12 = new Repartição(12, "FILTROS", a1, 1);
        Repartição r13 = new Repartição(13, "KASE-MOTORES", a1, 1);
        Repartição r14 = new Repartição(14, "HUSKY", a1, 1);
        Repartição r15 = new Repartição(15, "MANGUEIRAS", a1, 1);
        Repartição r16 = new Repartição(16, "PERIFERICOS", a1, 1);
        Repartição r17 = new Repartição(17, "ROLAMENTOS", a1, 1);

        Maquina m1 = new Maquina(1, "Gima Helicap", a2, 2);
        Maquina m2 = new Maquina(2, "Intravis Helicap", a2, 2);
        Maquina m3 = new Maquina(3, "Gima Recap", a2, 2);
        Maquina m4 = new Maquina(4, "IMD - ScrewCap", a2, 2);
        Maquina m5 = new Maquina(5, "Molde 065 070 line", a2, 2);
        Maquina m6 = new Maquina(6, "Molde 073 - 077 line", a2, 2);
        Maquina m7 = new Maquina(7, "Molde dc 475 line", a2, 2);
        Maquina m8 = new Maquina(8, "Molde LightCap 24-30", a2, 2);
        Maquina m9 = new Maquina(9, "Molde Recap 764 766", a2, 2);
        Maquina m10 = new Maquina(10, "Molde StreamCap", a2, 2);
        Maquina m11 = new Maquina(11, "StreamCap", a2, 2);
        Maquina m12 = new Maquina(12, "Gefit - DreamCap", a2, 2);

        Corredor c1 = new Corredor(1, "A", a1);
        Corredor c2 = new Corredor(2, "B",a1);
        Corredor c3 = new Corredor(3, "C", a1);

        Prateleira p1 = new Prateleira(1, "A1", c1);
        Prateleira p2 = new Prateleira(2, "A2", c2);
        Prateleira p3 = new Prateleira(3, "A3", c3);

        Produto pr1 = new Produto(1, null, p1, 1, c1, 1, r1, 1, m1, 1, a1, 1, "M1", null, ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 5, null, fotoBytes);
        Produto pr2 = new Produto(2, null, p2, 2, c2, 2, r1, 1, m1, 1, a1, 1, "M2", null, ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 5, null, fotoBytes);
        Produto pr3 = new Produto(3, null, p3, 3, c3, 3, r1, 1, m1, 1, a1, 1, "M3", null, ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 5, null, fotoBytes);
        Produto pr4 = new Produto(4, null, p3, 3, c3, 3, r1, 1, m1, 1, a2, 2, "M4", null, ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 5, null, fotoBytes);
        Produto pr5 = new Produto(5, null, p3, 3, c3, 3, r1, 1, m1, 1, a2, 2, "M5", null, ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 5, null, fotoBytes);
        Produto pr6 = new Produto(6, null, p3, 3, c3, 3, r1, 1, m1, 1, a2, 2, "M6", null, ProdutoStatus.Indisponivel, 1, 5, DateTime.Now, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 5, null, fotoBytes);

        _context.Almoxarifado.AddRange(a1);

        _context.Repartição.AddRange(r1, r2, r3, r3, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17);

        _context.Maquina.AddRange(m1, m2, m3, m3, m5, m6, m7, m8, m9, m10, m11, m12);

        _context.Corredor.AddRange(c1, c2, c3);

        _context.Prateleira.AddRange(p1, p2, p3);

        _context.Produto.AddRange(pr1, pr2, pr3, pr4, pr5, pr6);

        _context.SaveChanges();
    }
}