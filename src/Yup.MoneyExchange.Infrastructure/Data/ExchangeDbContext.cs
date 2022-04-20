using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yup.MoneyExchange.Domain.AggregatesModel;
using Yup.MoneyExchange.Infrastructure.Data.EntityConfigurations;

namespace Yup.MoneyExchange.Infrastructure.Data;

public class ExchangeDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = Schema.Transactional;
    public ExchangeDbContext(DbContextOptions<ExchangeDbContext> options)
        : base(options)
    {

    }
    public DbSet<Currency> Currency => Set<Currency>();
    public DbSet<CurrencyExchangeRate> CurrencyExchangeRate => Set<CurrencyExchangeRate>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CurrencyEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyExchangeRateEntityTypeConfiguration());

        //Disable Default DeleteBehavior
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.NoAction;

        base.OnModelCreating(modelBuilder);
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
public static class Schema
{
    public const string Master = "maestro";
    public const string Transactional = "transaccional";
}

