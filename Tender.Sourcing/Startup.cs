using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Tender.Sourcing.Data.Concrete;
using Tender.Sourcing.Data.Interfacess;
using Tender.Sourcing.Repositories;
using Tender.Sourcing.Repositories.Interfaces;
using Tender.Sourcing.Settings;

namespace Tender.Sourcing
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();

      services.Configure<TenderDatabaseSettings>(Configuration.GetSection("TenderDatabaseSettings"));

      services.AddSingleton<ITenderDatabaseSettings>(x => x.GetRequiredService<IOptions<TenderDatabaseSettings>>().Value);

      services.AddTransient<ITenderContext, TenderContext>();
      services.AddTransient<ITenderRepository, TenderRepository>();
      services.AddTransient<IBidRepository, BidRepository>();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tender.Sourcing", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tender.Sourcing v1"));

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
