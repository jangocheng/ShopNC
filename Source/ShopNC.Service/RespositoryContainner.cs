using Autofac;
using ShopNC.IRepository;
using ShopNC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Service
{
   public class RepositoryContainner
    {
         static object o=new object();
         static ContainerBuilder containerBuilder = new ContainerBuilder();
         static IContainer container = null;

        static RepositoryContainner() 
        {
            containerBuilder.RegisterType<DBSession>().As<IDBSession>();//IDBSession 不能为.SingleInstance()？
            container= containerBuilder.Build();
         }

        public IContainer Container { get { return container; } }
        
        public static void DisposeContainer()
        {
            container.Dispose();
        }

        public IBaseRepository<T> GetBaseRepository<T>()
        where T:class,new()
        {
             IBaseRepository<T> baseRepository = null;
             if (baseRepository == null)
             {
                 lock (o)
                 {
                     baseRepository = new BaseRepository<T>();
                 }
             }

            return baseRepository;
        }
       
    }
}
