using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VirtualClassroom.CommonAbstractions;
using VirtualClassroom.Persistence;

namespace VirtualClassroom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);


            

            using (var scope = host.Services.CreateScope())
            {
                var dataService = scope.ServiceProvider.GetService<IPersistanceContext>();
                if (dataService != null)
                    dataService.InitializeData(scope.ServiceProvider);

                var authenticationService = scope.ServiceProvider.GetService<IAuthentication>();
                authenticationService.InitializeData(scope.ServiceProvider);

            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
