using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TournamentPlatform.DAL.Repository.Implementation;
using TournamentPlatform.DAL.Repository.Interface;

namespace TournamentPlatform.WebUI.Configuration
{
    public static class DIConfigurator
    {
        public static void ConfigureApp(this IServiceCollection services)
        {
            var businessAssembly = Assembly.Load("TournamentPlatform.BLL");

            services.Scan(scan => scan.FromAssemblies(businessAssembly)
                                      .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                                      .AsImplementedInterfaces())
                    .AddAutoMapper(typeof(MappingProfile))
                    .AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}