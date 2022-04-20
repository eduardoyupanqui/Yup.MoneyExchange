﻿using Autofac;
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

        services.AddAplicacion(Configuration);
        services.AddInfrastructure(Configuration, Environment.IsDevelopment());


    }
    public void ConfigureContainer(ContainerBuilder builder)
    {

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

        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();

        });
    }
}