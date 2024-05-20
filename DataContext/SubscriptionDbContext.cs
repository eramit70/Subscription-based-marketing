using Microsoft.EntityFrameworkCore;
using Subscription_based_marketing.Models.Adminstrator;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.ServiceControl;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.Subscription;
using Subscription_based_marketing.Models.User;

namespace Subscription_based_marketing.DataContext
{
    public class SubscriptionDbContext : DbContext
    {
        public SubscriptionDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<SellerAccount> SellerAccounts { get; set; }
        public DbSet<AdminstratorAccount> Adminstrators { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }
        public DbSet<SubscriptionDetails> SubscriptionDetails { get; set; }
        public DbSet<ServiceAccessControl> ServiceAccessControl { get; set; }


        protected static void OnModelUserAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceAccessControl>()
                .HasOne(a => a.user)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected static void OnModelSellerAcount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceAccessControl>()
                .HasOne(a => a.seller)
                .WithMany()
                .HasForeignKey(f => f.SellerID)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected static void OnModelServiceAcount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceAccessControl>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(f => f.ServiceAccessControlID)
                .OnDelete(DeleteBehavior.NoAction);
        }
        protected static void OnModelUserAccountforSubs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubscriptionDetails>()
                .HasOne(a => a.user)
                .WithMany()
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected static void OnModelUserSellerforSubs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubscriptionDetails>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(f => f.serviceID)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected static void OnModelUserSellerforService(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceDetail>()
                .HasOne(a => a.seller)
                .WithMany()
                .HasForeignKey(f => f.SellerID)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }

}
