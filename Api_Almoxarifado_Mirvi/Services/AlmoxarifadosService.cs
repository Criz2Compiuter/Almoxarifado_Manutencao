﻿using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class AlmoxarifadoService
    {
        private readonly Api_Almoxarifado_MirviContext _context;

        public AlmoxarifadoService(Api_Almoxarifado_MirviContext context)
        {
            _context = context;
        }

        public async Task<List<Almoxarifado>> FindAllAsync()
        {
            return await _context.Almoxarifado.ToListAsync();
        }

        public async Task InsertAsync(Almoxarifado obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Almoxarifado> FindByIdAsync(int id)
        {
            return await _context.Almoxarifado.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Almoxarifado.FindAsync(id);
                _context.Almoxarifado.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegreityException(e.Message);
            }
        }

        public async Task UpdateAsync(Almoxarifado obj)
        {
            bool hasAny = _context.Almoxarifado.Any(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id nao encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (IntegreityException e)
            {
                throw new IntegreityException(e.Message);
            }
        }
    }
}
