using AMH_MarketPlace.Entities.Bank.Wallet;
using AMH_MarketPlace.Entities.Store;
using AMH_MarketPlace.Entities.Store.Product;
using AMH_MarketPlace.Entities.Transaction;
using AMH_MarketPlace.Entities.Transaction.TransacPurchase;
using AMH_MarketPlace.Entities.Transaction.TransacWallet;
using AMH_MarketPlace.Entities.User;
using AMH_MarketPlace.Entities.User.SubUser;
using AMH_MarketPlace.Entities.User.SubUser.Notifications;
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
        public DbSet<Wallet> Wallet => Set<Wallet>();
        public DbSet<UserWallet> UserWallet => Set<UserWallet>();
        public DbSet<CategoryProduct> CategoryProduct => Set<CategoryProduct>();
        public DbSet<ProductImage> ProductImage => Set<ProductImage>();
        public DbSet<Product> Product => Set<Product>();
        public DbSet<PurchaseProduct> PurchaseProduct => Set<PurchaseProduct>();
        public DbSet<Transaction> Transaction => Set<Transaction>();
        public DbSet<TopUpWallet> TopUpWallet => Set<TopUpWallet>();
        public DbSet<TransferWallet> TransferWallet => Set<TransferWallet>();
        public DbSet<WithdrawalWallet> WithdrawalWallet => Set<WithdrawalWallet>();

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
            modelBuilder.Entity<UserWallet>().HasIndex(w => w.NIK).IsUnique();
        }
    }
}
