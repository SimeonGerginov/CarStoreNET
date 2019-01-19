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
    }
}
