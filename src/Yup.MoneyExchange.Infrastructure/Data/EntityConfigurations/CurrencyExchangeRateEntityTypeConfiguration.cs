using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.AggregatesModel;

namespace Yup.MoneyExchange.Infrastructure.Data.EntityConfigurations;

public class CurrencyExchangeRateEntityTypeConfiguration : AuditoriaEntityTypeConfiguration<CurrencyExchangeRate>, IEntityTypeConfiguration<CurrencyExchangeRate>
{
    public override void Configure(EntityTypeBuilder<CurrencyExchangeRate> builder)
    {

        builder.ToTable("CurrencyExchangeRate", Schema.Transactional);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.CurrencyExchangeRateGuid).IsRequired();

        // Mapping
        builder.Property(c => c.Exchange).HasPrecision(12,2).IsRequired();
        builder.Property(c => c.EsActive).IsRequired();
        builder.Property(e => e.CurrencyFromId);
        builder.HasOne<Currency>().WithMany().HasForeignKey(e => e.CurrencyFromId);
        builder.Property(e => e.CurrencyToId);
        builder.HasOne<Currency>().WithMany().HasForeignKey(e => e.CurrencyToId);

        base.Configure(builder);
    }
}
