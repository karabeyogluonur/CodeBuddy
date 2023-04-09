using CB.Domain.Entities.Mail;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Data.Contexts
{
    public class CBDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public CBDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmailAccount> EmailAccounts { get; set; }
        public DbSet<EmailTemplate> MailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "AppUsers");
            });

            builder.Entity<AppRole>(entity =>
            {
                entity.ToTable(name: "AppRoles");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("AppUserRoles");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("AppUserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("AppUserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("AppRoleClaims");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("AppUserTokens");
            });
        }
    }
}
