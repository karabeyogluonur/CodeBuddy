using CB.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Data.Seeds
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<CBDbContext>();
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.Database.Migrate();

            //seeds
        }
    }
}
