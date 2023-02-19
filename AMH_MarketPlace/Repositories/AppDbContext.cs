using AMH_MarketPlace.Entities.Store;
using AMH_MarketPlace.Entities.User;
using AMH_MarketPlace.Entities.User.SubUser;
using AMH_MarketPlace.Entities.User.SubUser.Notifications;
using AMH_MarketPlace.Services.Implement.StoreImplement;
using Microsoft.EntityFrameworkCore;

namespace AMH_MarketPlace.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Role> Role => Set<Role>();
        public DbSet<CategoryNotification> CategoryNotification => Set<CategoryNotification>();
        public DbSet<NotifRead> NotifRead => Set<NotifRead>();
        public DbSet<Notification> Notification => Set<Notification>();
        public DbSet<Address> Address => Set<Address>();
        public DbSet<User> User => Set<User>();
        public DbSet<Credential> Credential => Set<Credential>();
        public DbSet<StoreImage> StoreImage => Set<StoreImage>();
        public DbSet<RateStore> RateStore => Set<RateStore>();
        public DbSet<Store> Store => Set<Store>();

        protected AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credential>().HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
        }
    }
}
