using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.Repositories;

namespace Yup.MoneyExchange.Infrastructure.Data.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    public BaseRepository(DbContext context)
    {
        _context = context;
    }
    public IEnumerable<TEntity> GetAll(bool asNoTracking = true)
    {
        return Query(asNoTracking).AsEnumerable<TEntity>();
    }

    public IQueryable<TEntity> Query(bool asNoTracking = true)
    {
        return asNoTracking ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>();
    }
    public virtual TEntity Add(TEntity entity)
    {
        return _context.Set<TEntity>().Add(entity).Entity;

        //Evaluar
        //Agregado entidad a la unidad de trabajo
        //DbEntityEntry dbEntrada = _unidadTrabajo.Entry<TEntidad>(entidad);
        //if (dbEntrada.State != EntityState.Detached)
        //    dbEntrada.State = EntityState.Added;
        //else
        //    _dbEntidad.Add(entidad);
    }

}
