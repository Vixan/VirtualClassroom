using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace VirtualClassroom.Core.Shared
{
    public interface IInitializer
    {
        void Initialize(IServiceCollection services);
        void Configure(IApplicationBuilder app);
    }
}
