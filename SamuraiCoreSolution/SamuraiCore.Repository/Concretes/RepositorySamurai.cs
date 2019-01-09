using SamuraiCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SamuraiCore.Data;
using SamuraiCore.Repository.Interfaces;

namespace SamuraiCore.Repository.Concretes
{
    public class RepositorySamurai : RepositoryBase<Samurai>, IRepositorySamurai
    {
        public RepositorySamurai(SamuraiContext dbContext) : base(dbContext)
        {
        }

        public override Samurai GetById(int id)
        {
            return DbContext.Samurai.Find(id);
        }
    }
}
