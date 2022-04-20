﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll(bool asNoTracking = true);
    TEntity Add(TEntity entity);
}