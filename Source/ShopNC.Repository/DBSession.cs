using ShopNC.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Repository
{
    public partial class DBSession:IDBSession
    {
        //private IUserInfoRepository _UserInfoRepository;
        //public  IUserInfoRepository  UserInfoRepository
        //{
        //    get 
        //    {
        //        if (_UserInfoRepository == null)
        //        {
        //            _UserInfoRepository =new UserInfoRepository();
        //        }

        //        return _UserInfoRepository;
        //    }
        //}

        //当前EF上下午文
        public DbContext CurrentEFContext { get; set; }

        private IDBContextFactory dbContextFactory = new EFContextFactory();

        public DBSession()
        {
            this.CurrentEFContext = dbContextFactory.GetCurrentContextInstence();
        }

        public int ExcuteSql(string sql, params System.Data.SqlClient.SqlParameter[] parameters)
        {
            return CurrentEFContext.Database.ExecuteSqlCommand(sql,parameters);

        }

        public List<T> SqlQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] parameters)
        {
            return CurrentEFContext.Database.SqlQuery<T>(sql, parameters).ToList();

        }
        //调用当前  的线程内唯一实例的 EF上下文，保存回数据库
        public int SaveChanges()
        {
            return this.CurrentEFContext.SaveChanges();
        }

        public int SaveChagesAsync() 
        {
            return this.CurrentEFContext.SaveChangesAsync().Result;
        }

        public void DisposeDBContext()
        {
            dbContextFactory.DisposeContext();
        }
    }
}
