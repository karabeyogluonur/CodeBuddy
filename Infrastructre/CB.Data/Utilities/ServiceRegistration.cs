using CB.Application.Abstractions.Repositories.UnitOfWork;
using CB.Application.Abstractions.Repositories;
using CB.Data.Contexts;
using CB.Data.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Data;
using CB.Domain.Entities.Membership;

namespace CB.Data.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            #region Database Context

            services.AddDbContext<CBDbContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));

            #endregion

            #region UnitOfWork
            services.AddScoped<IRepositoryFactory, UnitOfWork<CBDbContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<CBDbContext>>();
            services.AddScoped<IUnitOfWork<CBDbContext>, UnitOfWork<CBDbContext>>();
            #endregion

            #region Identity
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CBDbContext>().AddDefaultTokenProviders();
            #endregion
        }
    }
}
