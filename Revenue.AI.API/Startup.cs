using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Revenue.AI.Application.Services;
using Revenue.AI.Application.Services.Interfaces;
using Revenue.AI.Infrastructure;
using Revenue.AI.Infrastructure.SeedWorks;
using Revenue.AI.Middleware.Common;
using System;

namespace Revenue.AI.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(Startup));
            services.AddScoped<IRedisDestributeCache, DistributeCache>();
            //services.AddScoped<DistributeCacheMiddleware>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _configuration["RedisConn"];
                options.InstanceName = _configuration["RedisAppInstance"];
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Revenue.AI", Version = "v1"});
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(_configuration["ConnectionString"]));
           
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            IHostApplicationLifetime hostApplicationLifetime, AppDbContext appDbContext)
        {
            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                var options = new DistributedCacheEntryOptions();
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(double.Parse(_configuration["ExpirationTimeGap"]));
                options.SlidingExpiration = null;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Revenue.AI v1");
                });
            }
            appDbContext.Database.EnsureCreated();
            //app.UseMiddleware<DistributeCacheMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}