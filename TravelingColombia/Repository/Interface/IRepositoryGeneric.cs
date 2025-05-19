using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepositoryGeneric<T,Tkey> where T : class
    {
        Task<T> GetByIdAsync(Tkey tkey);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> Create(T t);
        Task<bool> Update(T t);
        Task<bool> DeleteByIdAsync(Tkey tkey);
    }
}