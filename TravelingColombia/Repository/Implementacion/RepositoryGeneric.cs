using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using TravelingColombia.Models;

namespace Repository.Implementacion
{
    public class RepositoryGeneric<T,Tkey> : IRepositoryGeneric<T,Tkey> where T : class
    {
        private readonly TravelingColombiabdContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryGeneric(TravelingColombiabdContext context){
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> Create(T t)
        {
            try{
                await _dbSet.AddAsync(t);
                return t;
            }catch(Exception ex){Console.WriteLine("Error"+ex.Message); return null;}
        }

        public async Task<bool> DeleteByIdAsync(Tkey tkey)
        {
            try{
                var entidad = await _dbSet.FindAsync(tkey);
                if(entidad == null){
                    return false;
                }
                _dbSet.Remove(entidad);
                return true;
            }catch(Exception ex){Console.WriteLine("Error"+ex.Message); return false;}
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try{
                return await _dbSet.Where(predicate).ToListAsync();
            }catch(Exception ex){Console.WriteLine("Error"+ex.Message); return new List<T>();}
        }

        public async Task<List<T>> GetAllAsync()
        {
            try{
                return await _dbSet.ToListAsync();
            }catch(Exception ex){Console.WriteLine("Error"+ex.Message); return new List<T>();}
        }

        public async Task<T?> GetByIdAsync(Tkey tkey)
        {
            try{
                return await _dbSet.FindAsync(tkey);
            }catch(Exception ex){Console.WriteLine("Error"+ex.Message); return null;}
        }

        public async Task<bool> Update(T t)
        {
            try{
                _dbSet.Update(t);
                return true;
            }catch(Exception ex){Console.WriteLine("Error"+ex.Message); return false;}
        }
    }
}