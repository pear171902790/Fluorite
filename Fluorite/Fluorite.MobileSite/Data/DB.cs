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
        public DB()
            : base("name=Fluorite")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DB, Migrations.Configuration>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Article> Articles { get; set; }
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
        public string Remarks { get; set; }
        public string Contacts { get; set; }
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
        public string Remarks { get; set; }
        public bool Valid { get; set; }
        public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; }
        public string CoverUrl { get; set; }
        
        public ArticleType Type { get; set; }
    }

    public enum ArticleType
    {
        Carousel,
        Menu,
        Common,
        SmallCover
    }

}