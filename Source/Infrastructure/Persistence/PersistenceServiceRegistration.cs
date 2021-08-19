using Application.Contracts.Persisitance;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.Persistence.Repository;

namespace VTS.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersitenceCollection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
