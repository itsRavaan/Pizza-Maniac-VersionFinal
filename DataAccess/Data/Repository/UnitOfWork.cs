using Api.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AppUser = new AppUserRepository(_db);
            Pizza = new PizzaRepository(_db);
            AppUserOrder = new AppUserOrderRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public IAppUserRepository AppUser { get; private set; }

        public IPizzaRepository Pizza { get; private set; }

        public IAppUserOrderRepository AppUserOrder { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        /// <summary>
        /// Dispose of unused resources
        /// </summary>
        public void Dispose()
        {
            _db.Dispose();
        }

        /// <summary>
        /// Persist changes to the DB
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
