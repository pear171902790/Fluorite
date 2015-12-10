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
        public DbSet<Data> Datas { get; set; }

        public static DB Instance
        {
            get { return _driverDbContext ?? (_driverDbContext = new DB()); }
        }
    }

    public class Data
    {
        [Key]
        public Guid Key { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Valid { get; set; }
    }

    public enum DataType
    {
        User = 1,
        Seller = 2
    }
}