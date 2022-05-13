using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using worksheet2.Authentication;
using worksheet2.Data;
using worksheet2.Data.Repository;
using worksheet2.Data.Repository.Impl;
using worksheet2.Model.Settings;
using worksheet2.Services;
using worksheet2.Services.Impl;

namespace worksheet2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            var config = Configuration
                .GetSection("AppSettings");

            serviceCollection.AddMemoryCache();
            serviceCollection.Configure<AppSettings>(config);
            RegisterServices(serviceCollection);
            RegisterRepositories(serviceCollection);
            serviceCollection.AddDbContext<BankContext>(
                options => options.UseMySQL(
                    Configuration.GetConnectionString("DefaultConnection")));
            serviceCollection.AddControllers()
                .AddJsonOptions(
                    options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

            serviceCollection.AddAuthentication();

            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "worksheet2", Version = "v1"});
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountDetailsService, AccountDetailService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPayService, PayService>();
            services.AddScoped<ICurrencyRateService, CurrencyRateService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IAccountDetailRepository, AccountDetailRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "worksheet2 v1"));
            }

            applicationBuilder.UseHttpsRedirection();

            applicationBuilder.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            applicationBuilder.UseRouting();
            
            applicationBuilder.UseAuthorization();

            applicationBuilder.UseMiddleware<TokenCheckMiddleware>();

            applicationBuilder.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}