using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualClassroom.Authentication;
using VirtualClassroom.Authentication.Services;
using VirtualClassroom.CommonAbstractions;
using VirtualClassroom.Core;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Persistence;
using VirtualClassroom.Persistence.EF;
using VirtualClassroom.Persistence.Memory;

namespace VirtualClassroom
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
            // Add authentication
            services.AddScoped<IAuthentication, AuthenticationInitializer>();

            var authenticationService = services.BuildServiceProvider().GetService<IAuthentication>();
            authenticationService.InitializeContext(services, Configuration);        

            // Add persistance
            services.AddScoped<IPersistanceContext, MemoryPersistenceContext>();           
            var dataService = services.BuildServiceProvider().GetService<IPersistanceContext>();
            dataService.InitializeContext(services, Configuration);

            // Add bussines
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IProfessorServices, ProfessorServices>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
