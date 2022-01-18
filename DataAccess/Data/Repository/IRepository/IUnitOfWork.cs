using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        /*
         *What is a unit of work?
         *Represents whatever you do in a single transaction
         *Has access to all of your repos as well as a Save method that
         * will persist all the changes you make to the DB.
         */

        IAppUserRepository AppUser { get; }

        IPizzaRepository Pizza { get; }

        IAppUserOrderRepository AppUserOrder { get; }

        ISP_Call SP_Call { get; }

        void Save();
    }
}
