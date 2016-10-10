using Angular2Blank.Data.Context;
using Angular2Blank.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Angular2Blank.Web.Installers
{
    public class DataInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Angular2BlankContext>(options =>
            {
                options.UseSqlServer(configuration["Data:ConnectionString"]);
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
