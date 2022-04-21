using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.AggregatesModel;

namespace Yup.MoneyExchange.Infrastructure.Data.EntityConfigurations;

public class CurrencyEntityTypeConfiguration : AuditoriaEntityTypeConfiguration<Currency>, IEntityTypeConfiguration<Currency>
{
    public override void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currency", Schema.Master);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        // Mapping
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Abreviature).IsRequired();

        builder.Property(c => c.EsActive).IsRequired();


        base.Configure(builder);
    }
}
