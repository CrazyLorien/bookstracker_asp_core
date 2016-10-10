using Angular2Blank.Services.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Angular2Blank.Web.Installers
{
    public class ProvidersInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<IPasswordProvider, PasswordProvider>();
        }
    }
}
