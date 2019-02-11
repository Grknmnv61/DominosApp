using Dominos.DataLayer;
using System;
using System.Linq;
using System.Data.Entity;


namespace Dominos.Repository.Repository
{
    public class DBRepository : IRepository<DominosDBEntities>
    {
        public DominosDBEntities Context { get; set; }
        private DbContextTransaction transaction;

        public DBRepository(DominosDBEntities context)
        {
            Context = context;
        }
        public int Commit()
        {
            var result = 0;

            try
            {
                result = Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Context.Set<T>().AsNoTracking();
        }

        public void RollBack()
        {
            transaction.Rollback();
        }
    }
}
