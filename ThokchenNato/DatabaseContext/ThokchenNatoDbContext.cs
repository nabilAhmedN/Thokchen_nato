using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ThokchenNato.Models.Data;

namespace ThokchenNato.DatabaseContext
{
    public class ThokchenNatoDbContext : DbContext
    {

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRoles> userRoles { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
    }
}