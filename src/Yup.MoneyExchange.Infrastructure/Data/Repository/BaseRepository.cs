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

}
