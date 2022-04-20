using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query(bool asNoTracking = true);
    IEnumerable<TEntity> GetAll(bool asNoTracking = true);
    IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, bool asNoTracking = true);
    TEntity Add(TEntity entity);
}
