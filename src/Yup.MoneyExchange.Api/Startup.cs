using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yup.MoneyExchange.Api.Configs;
using Yup.MoneyExchange.Api.Helpers;
using Yup.MoneyExchange.Application;
using Yup.MoneyExchange.Infrastructure;

namespace Yup.MoneyExchange.Api;
public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AuthConfig>(Configuration.GetSection("Auth"));
        services.AddTransient<TokenGenerator>();

        services.AddAplicacion(Configuration);
        services.AddInfrastructure(Configuration, Environment.IsDevelopment());

        var authConfig = Configuration.GetSection("Auth").Get<AuthConfig>();
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfig.Secret));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
           {
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = signingKey,
                   ValidateIssuer = true,
                   ValidIssuer = authConfig.Issuer,
                   ValidateAudience = true,
                   ValidAudience = authConfig.Audience,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
                   RequireExpirationTime = true,
               };
               options.Events = new JwtBearerEvents()
               {
                       //Evento para saber el detalle de un 401 (No Authorize), token invalido? token expirado?
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception is SecurityTokenExpiredException expiredException)
                           {
                               context.Response.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.WWWAuthenticate,
                                   new Microsoft.Extensions.Primitives.StringValues(new[] {
                                           JwtBearerDefaults.AuthenticationScheme,
                                           "error=\"invalid_token\"",
                                           "error_description=\"Token de acceso ha expirado\""
                                   }));
                           }
                           return System.Threading.Tasks.Task.FromResult(0);
                       }
               };
           });

    }
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();

        });
    }
}
