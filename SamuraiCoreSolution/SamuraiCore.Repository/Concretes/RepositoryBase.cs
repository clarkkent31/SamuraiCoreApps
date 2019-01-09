using Microsoft.EntityFrameworkCore;
using SamuraiCore.Data;
using SamuraiCore.Repository.Interfaces;

namespace SamuraiCore.Repository.Concretes
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SamuraiContext DbContext { get; set; }

        protected RepositoryBase(SamuraiContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract T GetById(int id);
    }
}
