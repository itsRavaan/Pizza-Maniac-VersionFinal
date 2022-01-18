using Api.DataAccess.Data.Repository.IRepository;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Data.Repository
{
    public class PizzaRepository : Repository<Pizza>, IPizzaRepository
    {
        private readonly ApplicationDbContext _context;

        //Inject our datacontext in here
        public PizzaRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<Pizza> GetBookByBookNameAsync(string bookname)
        {
            return await _context.Pizza
                .SingleOrDefaultAsync(x => x.PizzaName == bookname);
        }

        public async Task<Pizza> GetBookByIdAsync(int id)
        {
            return await _context.Pizza.FindAsync(id);
        }

        public async Task<IEnumerable<Pizza>> GetPizzaAsync()
        {
            return await _context.Pizza
                .ToListAsync();
        }

        /// <summary>
        /// Ensure that > 0 changes have been saved in order to return a boolean.
        /// </summary>
        /// <returns>boolean if changes saved successfully</returns>
        public async Task<bool> SaveAllAsync()
        {
            //ensure that > 0 changes have been saved in order to return a boolean.
            //Save changes async returns an int with number of changes saved in DB
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Pizza book)
        {
            //Set the state to modified in EF. Meaning dont save to DB yet.
            _context.Entry(book).State = EntityState.Modified;
        }
    }
}
