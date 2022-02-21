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
        public virtual DbSet<TblBookMark> TblBookMarks { get; set; }
        public virtual DbSet<TblCatagory> TblCatagories { get; set; }
        public virtual DbSet<TblCity> TblCities { get; set; }
        public virtual DbSet<TblDeal> TblDeals { get; set; }
        public virtual DbSet<TblDealOrder> TblDealOrders { get; set; }
        public virtual DbSet<TblDiscount> TblDiscounts { get; set; }
        public virtual DbSet<TblMusic> TblMusics { get; set; }
        public virtual DbSet<TblNaighborhood> TblNaighborhoods { get; set; }
        public virtual DbSet<TblOrder> TblOrders { get; set; }
        public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblQueue> TblQueues { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblStore> TblStores { get; set; }
        public virtual DbSet<TblStoreCatagory> TblStoreCatagories { get; set; }
        public virtual DbSet<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder
      .UseLazyLoadingProxies()
      .UseSqlServer("Data Source=185.55.224.199;Initial Catalog=ecovilli_ecovill;User ID=ecovilli_ecovill;Password=ap50%5cV");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ecovilli_ecovill");

            modelBuilder.Entity<TblAbility>(entity =>
            {
                entity.Property(e => e.BannerImageUrl1).HasComment("");

                entity.Property(e => e.BannerImageUrl2).HasComment("");

                entity.Property(e => e.BannerLink1).HasComment("");

                entity.Property(e => e.BannerLink2).HasComment("");

                entity.Property(e => e.Haraj).HasComment("0 is Not Bought; 1 is True; 2 is false;");

                entity.Property(e => e.IsMusicEnable).HasDefaultValueSql("((0))");

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

            modelBuilder.Entity<TblBookMark>(entity =>
            {
                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblBookMarks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_TblBookMark_TblStore");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblBookMarks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TblBookMark_TblUser");
            });

            modelBuilder.Entity<TblCatagory>(entity =>
            {
                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblCatagories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblCatagory_TblStore");
            });

            modelBuilder.Entity<TblDeal>(entity =>
            {
                entity.Property(e => e.Banner1).HasComment("");

                entity.Property(e => e.Banner2).HasComment("");

                entity.Property(e => e.Haraj).HasComment("");

                entity.Property(e => e.PardakhteOnline)
                    .HasDefaultValueSql("((0))")
                    .HasComment("");
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

            modelBuilder.Entity<TblNaighborhood>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.TblNaighborhoods)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_TblNaighborhood_TblCity");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasComment("0 is tahvil dar khaneye moshtari; 1 is tahvil dar forushgah;");

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
                    .HasConstraintName("FK_TblProduct_TblCatagory");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblProduct_TblStore");
            });

            modelBuilder.Entity<TblQueue>(entity =>
            {
                entity.Property(e => e.DateSubminted).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TblQueues)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblQueue_TblStore");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblQueues)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblQueue_TblUser");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsFixedLength(true);
            });

            modelBuilder.Entity<TblStore>(entity =>
            {
                entity.Property(e => e.CatagoryLimit).HasDefaultValueSql("((10))");

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

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TblStores)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_TblStore_TblCity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblStores)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblStore_TblUser");
            });

            modelBuilder.Entity<TblStoreCatagory>(entity =>
            {
                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_TblStoreCatagory_TblStoreCatagory");
            });

            modelBuilder.Entity<TblStoreNaighborhoodRel>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.TblStoreNaighborhoodRels)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_TblStoreNaighborhoodRel_TblCity");

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

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_TblUser_TblUser");

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
