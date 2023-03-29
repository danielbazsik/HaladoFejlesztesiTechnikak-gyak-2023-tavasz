using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Database_Application
{
    public class CarDbContext : DbContext
    {

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Car> Car { get; set; }

        public CarDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CarDB.mdf;Integrated Security=True";
                builder
                .UseLazyLoadingProxies()
                .UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity => entity
            .HasOne<Brand>()
            .WithMany(brand => brand.Cars)
            .HasForeignKey(car => car.BrandId)
            .OnDelete(DeleteBehavior.ClientSetNull));

            //modelBuilder.Entity<Brand>().HasData(new Brand[]
            //{
            //    new Brand() { Id = 1, Name = "BMW" },
            //    new Brand() { Id = 2, Name = "Citroen" },
            //    new Brand() { Id = 3, Name = "Audi" }
            //});
            //modelBuilder.Entity<Car>().HasData(new Car[]
            //{
            //    new Car() { Id = 1, BrandId = 1, BasePrice = 20000, Model = "BMW 116d" },
            //    new Car() { Id = 2, BrandId = 1, BasePrice = 30000, Model = "BMW 510" },
            //    new Car() { Id = 3, BrandId = 2, BasePrice = 10000, Model = "Citroen C1" },
            //    new Car() { Id = 4, BrandId = 2, BasePrice = 15000, Model = "Citroen C3" },
            //    new Car() { Id = 5, BrandId = 3, BasePrice = 20000, Model = "Audi A3" },
            //    new Car() { Id = 6, BrandId = 3, BasePrice = 25000, Model = "Audi A4" }
            //});

            Brand bmw = new Brand() { Id = 1, Name = "BMW" };
            Brand citroen = new Brand() { Id = 2, Name = "Citroen" };
            Brand audi = new Brand() { Id = 3, Name = "Audi" };

            Car bmw1 = new Car() { Id = 1, BrandId = 1, BasePrice = 20000, Model = "BMW 116d" };
            Car bmw2 = new Car() { Id = 2, BrandId = 1, BasePrice = 30000, Model = "BMW 510" };
            Car citroen1 = new Car() { Id = 3, BrandId = 2, BasePrice = 10000, Model = "Citroen C1" };
            Car citroen2 = new Car() { Id = 4, BrandId = 2, BasePrice = 15000, Model = "Citroen C3" };
            Car audi1 = new Car() { Id = 5, BrandId = 3, BasePrice = 20000, Model = "Audi A3" };
            Car audi2 = new Car() { Id = 6, BrandId = 3, BasePrice = 25000, Model = "Audi A4" };

            modelBuilder.Entity<Brand>().HasData(bmw, citroen, audi);
            modelBuilder.Entity<Car>().HasData(bmw1, bmw2, citroen1, citroen2, audi1, audi2);

        }
    }
}
