using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualClassroom.Authentication.Data;
using VirtualClassroom.CommonAbstractions;

namespace VirtualClassroom.Authentication
{
    public class AuthenticationInitializer : IAuthentication
    {
        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AuthConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            CreateRoles(serviceProvider);
        }
        
        public string GetUserEmail()
        {
            throw new NotImplementedException();
        }

        public int GetUserId()
        {
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }

        public bool IsProfessor()
        {
            throw new NotImplementedException();
        }

        public bool IsStudent()
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void Register()
        {
            throw new NotImplementedException();
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            Task<IdentityResult> roleResult;

            Task<bool> hasProfessorRole = roleManager.RoleExistsAsync("Professor");
            hasProfessorRole.Wait();

            if (!hasProfessorRole.Result)
            {
                IdentityRole professorRole = new IdentityRole()
                {
                    Name = "Professor"
                };

                roleResult = roleManager.CreateAsync(professorRole);
                roleResult.Wait();
            }

            Task<bool> hasStudentsRole = roleManager.RoleExistsAsync("Student");
            hasStudentsRole.Wait();

            if (!hasStudentsRole.Result)
            {
                IdentityRole studentRole = new IdentityRole()
                {
                    Name = "Student"
                };

                roleResult = roleManager.CreateAsync(studentRole);
                roleResult.Wait();
            }
        }
    }
}
