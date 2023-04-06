using CB.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Data.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            #region Database Context

            services.AddDbContext<CBDbContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));

            #endregion
        }
    }
}
