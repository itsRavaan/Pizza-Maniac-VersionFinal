using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Data.Repository.IRepository
{
    public interface IPizzaRepository : IRepository<Pizza>
    {
        void Update(Pizza book);//No need to be async as it's not updating DB. Only EF in memory.

        Task<bool> SaveAllAsync();

        Task<IEnumerable<Pizza>> GetPizzaAsync();

        Task<Pizza> GetBookByIdAsync(int id);

        Task<Pizza> GetBookByBookNameAsync(string bookname);
    }
}
