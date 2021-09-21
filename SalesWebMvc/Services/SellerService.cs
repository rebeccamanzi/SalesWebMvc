using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            // vai acessar a base de dados e trazer os resultados em lista
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            // Include -> expressão do Entity para fazer o join entre as tabelas
            // e conseguir pegar o valor do department (eager loading)
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Seller.Find(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException e)
            {
                throw new IntegrityException("Cant delete seller because he/she has sales");
            }           
        }

        public async Task UpdateAsync(Seller obj)
        {
            //verifica se o id existe
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new DllNotFoundException("Id not found");
            }
            try
            {
                //atualiza banco de dados
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
