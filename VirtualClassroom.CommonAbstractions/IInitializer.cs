using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace VirtualClassroom.CommonAbstractions
{
    public interface IInitializer
    {
        void InitializeContext(IServiceCollection services, IConfiguration configuration);
        void InitializeData(IServiceProvider serviceProvider);
    }
}
