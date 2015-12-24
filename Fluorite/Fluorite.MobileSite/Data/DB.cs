using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fluorite.MobileSite.Data
{
    public class DB : DbContext
    {
        private static DB _driverDbContext;
        public DB()
            : base("name=Fluorite")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DB, Migrations.Configuration>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Article> Articles { get; set; }

        public static DB Instance
        {
            get { return _driverDbContext ?? (_driverDbContext = new DB()); }
        }
    }

    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Valid { get; set; }
    }

    public class Seller
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Valid { get; set; }
        public virtual List<Article> Article { get; set; }
    }
    public class Article
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Valid { get; set; }
        public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; }
    }

}