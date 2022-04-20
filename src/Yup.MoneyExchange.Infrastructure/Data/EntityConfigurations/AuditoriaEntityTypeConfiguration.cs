using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.AggregatesModel.Base;

namespace Yup.MoneyExchange.Infrastructure.Data.EntityConfigurations;

public class AuditoriaEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.CreatedBy);

        builder.Property(x => x.UpdatedDate);
        builder.Property(x => x.UpdatedBy);

        builder.Property(c => c.EsEliminado).IsRequired();
    }
}
