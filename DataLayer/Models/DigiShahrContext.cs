using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<TblMusic> TblMusics { get; set; }
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
                entity.ToTable("TblAbility");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BannerImageUrl1)
                    .HasMaxLength(500)
                    .HasComment("");

                entity.Property(e => e.BannerImageUrl2)
                    .HasMaxLength(500)
                    .HasComment("");

                entity.Property(e => e.BannerLink1)
                    .HasMaxLength(500)
                    .HasComment("");

                entity.Property(e => e.BannerLink2)
                    .HasMaxLength(500)
                    .HasComment("");

                entity.Property(e => e.Haraj).HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.IsBanner1Enable).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMusicEnable).HasDefaultValueSql("((0))");

                entity.Property(e => e.LotteryDisplayDate).HasColumnType("datetime");

                entity.Property(e => e.LotteryDisplayPrize).HasMaxLength(500);

                entity.Property(e => e.PardakhteOnline).HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.TahvilVaTasvieDarForushgah)
                    .HasDefaultValueSql("((2))")
                    .HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.TahvilVaTasvieDarMahal)
                    .HasDefaultValueSql("((2))")
                    .HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.ValidationTimeSpan).HasDefaultValueSql("((30))");

                entity.HasOne(d => d.Music)
                    .WithMany(p => p.TblAbilities)
                    .HasForeignKey(d => d.MusicId)
                    .HasConstraintName("FK_TblAbility_TblMusics");
            });

            modelBuilder.Entity<TblCatagory>(entity =>
            {
                entity.ToTable("TblCatagory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblDeal>(entity =>
            {
                entity.ToTable("TblDeal");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Banner1).HasComment("");

                entity.Property(e => e.Banner2).HasComment("");

                entity.Property(e => e.Haraj).HasComment("");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PardakhteOnline)
                    .HasDefaultValueSql("((0))")
                    .HasComment("");
            });

            modelBuilder.Entity<TblDealOrder>(entity =>
            {
                entity.ToTable("TblDealOrder");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateSubmited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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
                entity.ToTable("TblDiscount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblDiscounts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblDiscount_TblStore");
            });

            modelBuilder.Entity<TblMusic>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MusicUrl)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblNaighborhood>(entity =>
            {
                entity.ToTable("TblNaighborhood");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.ToTable("TblOrder");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateSubmited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LotteryCode).HasMaxLength(64);

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_TblOrder_TblDiscount");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblOrder_TblStore");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblOrder_TblUser");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.ToTable("TblOrderDetail");

                entity.Property(e => e.Id).HasColumnName("id");

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
                entity.ToTable("TblProduct");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MainImageUrl).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.StoreCatagory)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.StoreCatagoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblProduct_TblStoreCatagoryRel");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.ToTable("TblRole");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblStore>(entity =>
            {
                entity.ToTable("TblStore");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CatagoryLimit).HasDefaultValueSql("((10))");

                entity.Property(e => e.Lat)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LogoUrl).HasMaxLength(500);

                entity.Property(e => e.Lon)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MainBannerUrl).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductLimit).HasDefaultValueSql("((30))");

                entity.Property(e => e.StaticTell)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.SubscribtionTill).HasColumnType("datetime");

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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblStores)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblStore_TblUser");
            });

            modelBuilder.Entity<TblStoreCatagory>(entity =>
            {
                entity.ToTable("TblStoreCatagory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_TblStoreCatagory_TblStoreCatagory");
            });

            modelBuilder.Entity<TblStoreCatagoryRel>(entity =>
            {
                entity.ToTable("TblStoreCatagoryRel");

                entity.Property(e => e.Id).HasColumnName("id");

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
                entity.ToTable("TblStoreNaighborhoodRel");

                entity.Property(e => e.Id).HasColumnName("id");

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
                entity.ToTable("TblUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Auth).HasMaxLength(64);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Lat).HasMaxLength(50);

                entity.Property(e => e.Lon).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.TellNo)
                    .IsRequired()
                    .HasMaxLength(11);

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
