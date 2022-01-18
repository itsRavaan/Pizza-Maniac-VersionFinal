using Euromonitor.DataAccess.Data.Repository.IRepository;
using Euromonitor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euromonitor.DataAccess.Data.Repository
{
    public class AppUserOrderRepository : Repository<AppUserOrder>, IAppUserOrderRepository
    {
        private readonly ApplicationDbContext _context;

        //Inject our DataContext into the DI container
        public AppUserOrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUserOrder> GetAppUserBookByIdAsync(int id)
        {
            return await _context.AppUserOrder.FindAsync(id);
        }

        public async Task<IEnumerable<AppUserOrder>> GetAppUserBooksByAppUserIdAsync(int id)
        {
            return await _context.AppUserOrder
                .Where(c => c.AppUserId == id)
               .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            //ensure that > 0 changes have been saved in order to return a boolean.
            //Save changes async returns an int with number of changes saved in DB
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUserOrder appUserBook)
        {
            //Set the state to modified. Meaning dont sav to DB yet.
            _context.Entry(appUserBook).State = EntityState.Modified;
        }
    }
}
