using Euromonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euromonitor.DataAccess.Data.Repository.IRepository
{
    public interface IAppUserOrderRepository : IRepository<AppUserOrder>
    {
        void Update(AppUserOrder appUserBook);//No need to be async as it's not updating DB. Only EF in memory.

        Task<bool> SaveAllAsync();

        Task<AppUserOrder> GetAppUserBookByIdAsync(int id);

        Task<IEnumerable<AppUserOrder>> GetAppUserBooksByAppUserIdAsync(int id);

    }
}
