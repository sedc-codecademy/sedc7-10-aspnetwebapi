using DataAccess;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers
{
    public static class DiModule
    {
        public static IServiceCollection RegisterModule(
            IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<UserDto>, UserRepository>();
            services.AddTransient<IRepository<NoteDto>, NoteRepository>();
            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer(connectionString));

            return services;
        }
    }
}
