using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class BuscasService
    {

        private readonly Api_Almoxarifado_MirviContext _context;

        public BuscasService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }
    }
}

