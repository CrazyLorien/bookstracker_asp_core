using Angular2Blank.Data.Context;
using Angular2Blank.Data.Repository;
using Angular2Blank.Services.Implementation;
using Angular2Blank.Services.Interfaces;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Angular2Blank.Installers
{
    public class DataInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFramework().AddSqlServer().AddDbContext<Angular2BlankContext>(options =>
            {
                options.UseSqlServer(configuration["Data:ConnectionString"]);
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
