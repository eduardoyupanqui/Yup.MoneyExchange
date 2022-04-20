using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yup.MoneyExchange.Domain.Repositories;
using Yup.MoneyExchange.Infrastructure.Data;
using Yup.MoneyExchange.Infrastructure.Data.Repository;

namespace Yup.MoneyExchange.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration, bool IsDevelopment)
    {
        services.AddDbContext<DbContext, ExchangeDbContext>(options =>
            options
                //.UseSqlServer(Configuration.GetConnectionString(nameof(ExchangeDbContext))
                // //, options => options.EnableRetryOnFailure()
                // )
                .UseInMemoryDatabase(databaseName: "Exchange")
                //.UseLoggerFactory(loggerFactory)
                //.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                .EnableSensitiveDataLogging(IsDevelopment)

            );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        //services.AddTransient<ICurrencyRepository, CurrencyRepository>();

        return services;
    }
}
