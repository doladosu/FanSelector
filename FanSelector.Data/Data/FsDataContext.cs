using System.Data.Entity;
using FanSelector.Data.Auth;
using FanSelector.Models.Db;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FanSelector.Data.Data
{
    public partial class FsDataContext : IdentityDbContext<ApplicationUser>
    {
        public FsDataContext()
            : base("name=DefaultConnection")
        {
        }

        public static FsDataContext Create()
        {
            return new FsDataContext();
        }

        public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Contest> Contests { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Secret)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.AllowedOrigin)
                .IsUnicode(false);

            modelBuilder.Entity<Contest>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RefreshToken>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<RefreshToken>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<RefreshToken>()
                .Property(e => e.ClientId)
                .IsUnicode(false);

            modelBuilder.Entity<RefreshToken>()
                .Property(e => e.ProtectedTicket)
                .IsUnicode(false);
        }
    }
}
