using ShopNC.Entity.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Entity.Mapping
{
    public class ShopNCContext : DbContext
    {
        private const string CONNSTR = "ShopConnStr";
        //必须继承base 并把连接字符串传入
        public ShopNCContext():base(CONNSTR)
        {
            //自动更新数据库 不用运行 update-database 命令。
            Database.SetInitializer<ShopNCContext>(new MigrateDatabaseToLatestVersion<ShopNCContext, Configuration>(CONNSTR));

           // this.Configuration.ProxyCreationEnabled = true;
           // this.Configuration.LazyLoadingEnabled = true; 默认为true
        }
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<PermissionGroup> PermissionGroup { get; set; }


       // public DbSet<UserRoleR> UserRoleR { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 禁用多对多关系表的级联删除
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

        }

    }

}
