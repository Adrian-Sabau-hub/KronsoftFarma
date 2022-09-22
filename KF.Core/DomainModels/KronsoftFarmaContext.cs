using KF.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace KF.Core.DomainModels
{
    public partial class KronsoftFarmaContext : DbContext, IDbContext
    {
        #region ctor

        public KronsoftFarmaContext()
        {
        }

        public KronsoftFarmaContext(DbContextOptions<KronsoftFarmaContext> options)
            : base(options)
        {
        }

        #endregion

        #region Properties

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ADISABAULAPDELL;Database=KronsoftFarma;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Producer).HasMaxLength(50);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_Stocks")
                    .IsUnique();

                entity.Property(e => e.StockId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Stock)
                    .HasForeignKey<Stock>(d => d.ProductId)
                    //.HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocks_Products");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                //entity.Property(e => e.UserId).HasColumnName("isAdmin");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        #endregion
    }
}
