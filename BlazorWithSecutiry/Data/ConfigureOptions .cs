using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;

namespace BlazorWithSecutiry
{
    public static class CollectionExtensions
    {
        public static void AddEditor(this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(Startup));
        }
    }
}
