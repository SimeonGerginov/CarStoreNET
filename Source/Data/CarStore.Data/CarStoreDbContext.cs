using CarStore.Data.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Data
{
    public class CarStoreDbContext : IdentityDbContext<Customer>
    {
        public CarStoreDbContext(DbContextOptions<CarStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Model> Models { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<StoreCategory> StoreCategories { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarCategory>(entity =>
            {
                entity.HasKey(cc => new {cc.CarId, cc.CategoryId});
                entity.HasOne(cc => cc.Car).WithMany(c => c.CarCategories).HasForeignKey(cc => cc.CarId);
                entity.HasOne(cc => cc.Category).WithMany(c => c.CarCategories).HasForeignKey(cc => cc.CategoryId);
            });

            builder.Entity<CarStoreCategory>(entity =>
            {
                entity.HasKey(csc => new { csc.CarId, csc.StoreCategoryId });
                entity.HasOne(csc => csc.Car).WithMany(sc => sc.CarStoreCategories).HasForeignKey(csc => csc.CarId);
                entity.HasOne(csc => csc.StoreCategory).WithMany(c => c.CarStoreCategories).HasForeignKey(csc => csc.StoreCategoryId);
            });
        }
    }
}
