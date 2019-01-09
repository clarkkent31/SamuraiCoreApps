using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SamuraiCore.Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        T GetById(int id);
    }
}
