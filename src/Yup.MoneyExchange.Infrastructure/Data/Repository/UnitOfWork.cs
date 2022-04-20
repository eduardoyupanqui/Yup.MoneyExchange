using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.Repositories;

namespace Yup.MoneyExchange.Infrastructure.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
