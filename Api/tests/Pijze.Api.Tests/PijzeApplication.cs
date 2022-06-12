using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pijze.Api.Tests.Security;
using Pijze.Domain.Beers;
using Pijze.Domain.Services;
using Pijze.Infrastructure.Data;

namespace Pijze.Api.Tests;

internal class PijzeApplication : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // var descriptor = services.SingleOrDefault(
            //     d => d.ServiceType ==
            //          typeof(DbContextOptions<PijzeDbContext>));
            //
            // if (descriptor != null)
            //     services.Remove(descriptor);
            
            // services.AddDbContext<PijzeDbContext>(options =>
            // {
            //     options.UseInMemoryDatabase("PijzeInMemory");
            // });
            //
            // var sp = services.BuildServiceProvider();
            // using (var scope = sp.CreateScope())
            // using (var context = scope.ServiceProvider.GetRequiredService<PijzeDbContext>())
            // {
            //     context.Database.EnsureDeleted();
            //     context.Database.EnsureCreated();
            //  
            // }

            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, MockAuthenticationHandler>("Test", null);
        });
    }
}