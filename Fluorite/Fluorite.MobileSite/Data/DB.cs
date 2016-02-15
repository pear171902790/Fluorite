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
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Self> Selves { get; set; }
    }

    public class Self
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
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
        public virtual List<Article> Articles { get; set; }
        public string LogoUrl { get; set; }
        public string LogoText { get; set; }
        public string LogoTextSize { get; set; }
        public string LogoUri { get; set; }
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
        public string ExternalUrl { get; set; }
        public ArticleType Type { get; set; }
        public bool IsExternal { get; set; }
        public string Order { get; set; }
        public string OrderPicUrl { get; set; }
        public string OrderTitle { get; set; }
    }

    public enum ArticleType
    {
        Carousel,
        Menu,
        Common,
        SmallCover
    }

}