using Angular2Blank.Services.Implementation;
using Angular2Blank.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Angular2Blank.Web.Installers
{
    public class ServicesInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
