using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    public partial class DigiShahrContext : DbContext
    {
        public DigiShahrContext()
        {
        }

        public DigiShahrContext(DbContextOptions<DigiShahrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAbility> TblAbilities { get; set; }
        public virtual DbSet<TblCatagory> TblCatagories { get; set; }
        public virtual DbSet<TblDeal> TblDeals { get; set; }
        public virtual DbSet<TblDealOrder> TblDealOrders { get; set; }
        public virtual DbSet<TblDiscount> TblDiscounts { get; set; }
        public virtual DbSet<TblNaighborhood> TblNaighborhoods { get; set; }
        public virtual DbSet<TblOrder> TblOrders { get; set; }
        public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblStore> TblStores { get; set; }
        public virtual DbSet<TblStoreCatagory> TblStoreCatagories { get; set; }
        public virtual DbSet<TblStoreCatagoryRel> TblStoreCatagoryRels { get; set; }
        public virtual DbSet<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=103.216.62.27;Initial Catalog=DigiShahr;User ID=Yanal;Password=1710ahmad.fard");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAbility>(entity =>
            {
                entity.Property(e => e.BannerImageUrl1).HasComment("");

                entity.Property(e => e.BannerImageUrl2).HasComment("");

                entity.Property(e => e.BannerLink1).HasComment("");

                entity.Property(e => e.BannerLink2).HasComment("");

                entity.Property(e => e.Haraj).HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.PardakhteOnline).HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.TahvilVaTasvieDarForushgah)
                    .HasDefaultValueSql("((2))")
                    .HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.TahvilVaTasvieDarMahal)
                    .HasDefaultValueSql("((2))")
                    .HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.ValidationTimeSpan).HasDefaultValueSql("((30))");
            });

            modelBuilder.Entity<TblDeal>(entity =>
            {
                entity.Property(e => e.Banner1).HasComment("0 is Not Bought; Else Bought;");

                entity.Property(e => e.Banner2).HasComment("0 is Not Bought; Else Bought;");

                entity.Property(e => e.Haraj).HasComment("");

                entity.Property(e => e.PardakhteOnline).HasComment("");
            });

            modelBuilder.Entity<TblDealOrder>(entity =>
            {
                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Deal)
                    .WithMany(p => p.TblDealOrders)
                    .HasForeignKey(d => d.DealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblDealOrder_TblDeal");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblDealOrders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblDealOrder_TblStore");
            });

            modelBuilder.Entity<TblDiscount>(entity =>
            {
                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblDiscounts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblDiscount_TblStore");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_TblOrder_TblDiscount");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblOrder_TblUser");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.Property(e => e.Count).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblOrderDetail_TblOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblOrderDetail_TblProduct");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasOne(d => d.StoreCatagory)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.StoreCatagoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblProduct_TblStoreCatagoryRel");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsFixedLength(true);
            });

            modelBuilder.Entity<TblStore>(entity =>
            {
                entity.Property(e => e.CatagoryLimit).HasDefaultValueSql("((10))");

                entity.Property(e => e.DeliveryPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductLimit).HasDefaultValueSql("((30))");

                entity.HasOne(d => d.Ability)
                    .WithMany(p => p.TblStores)
                    .HasForeignKey(d => d.AbilityId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TblStore_TblAbility");

                entity.HasOne(d => d.Catagory)
                    .WithMany(p => p.TblStores)
                    .HasForeignKey(d => d.CatagoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblStore_TblStoreCatagory");
            });

            modelBuilder.Entity<TblStoreCatagory>(entity =>
            {
                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_TblStoreCatagory_TblStoreCatagory");
            });

            modelBuilder.Entity<TblStoreCatagoryRel>(entity =>
            {
                entity.HasOne(d => d.Catagory)
                    .WithMany(p => p.TblStoreCatagoryRels)
                    .HasForeignKey(d => d.CatagoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblStoreCatagoryRel_TblCatagory");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblStoreCatagoryRels)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_TblStoreCatagoryRel_TblStore");
            });

            modelBuilder.Entity<TblStoreNaighborhoodRel>(entity =>
            {
                entity.HasOne(d => d.Naighborhood)
                    .WithMany(p => p.TblStoreNaighborhoodRels)
                    .HasForeignKey(d => d.NaighborhoodId)
                    .HasConstraintName("FK_TblStoreNaighborhoodRel_TblNaighborhood");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblStoreNaighborhoodRels)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_TblStoreNaighborhoodRel_TblStore");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Naighborhood)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.NaighborhoodId)
                    .HasConstraintName("FK_TblUser_TblNaighborhood");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblUser_TblRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
