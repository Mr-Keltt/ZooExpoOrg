using Microsoft.EntityFrameworkCore;
using ZooExpoOrg.Context.Entities;
using Microsoft.AspNetCore.Identity;

namespace ZooExpoOrg.Context;

public static class UsersContextConfiguration
{
    public static void ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.ToTable("users");

            entity
                .HasOne(u => u.Client)
                .WithOne(c => c.User)
                .HasForeignKey<ClientEntity>(c => c.UserId);
        });

        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
    }
}