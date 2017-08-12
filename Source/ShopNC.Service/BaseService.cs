using Autofac;
using ShopNC.IRepository;
using ShopNC.IService;
using ShopNC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Service
{
    public class BaseService<T>:IBaseService<T>
        where T:class,new()
    {

        private  IDBSession session = null;

        protected IDBSession DBSession
        {
            get { return session; }
        }
        public BaseService() 
         {
            session=new RepositoryContainner().Container.Resolve<IDBSession>();
         }

        public IBaseRepository<T> baseRepository = new RepositoryContainner().GetBaseRepository<T>();
        public T AddEntity(T entity)
        {
            var model= baseRepository.AddEntity(entity);
            DBSession.SaveChanges();

            return model;
        }

        public bool UpdateEntity(T entity)
        {
          var result= baseRepository.UpdateEntity(entity);
          return DBSession.SaveChanges()>0;
        }

        public bool DeleteEntity(T entity)
        {
           var result= baseRepository.DeleteEntity(entity);
           DBSession.SaveChanges();

           return result;

        }

        public IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
           return baseRepository.LoadEntity(whereLambda);
        }

        public IQueryable<T> LoadPageEntity<S>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, int? pageIndex, int? pageSize, out int total, System.Linq.Expressions.Expression<Func<T, S>> orderLambda, bool isAsc)
        {
            return baseRepository.LoadPageEntity(whereLambda, pageIndex, pageSize, out total, orderLambda, isAsc);
        }


        public async Task<T> AddEntityAsync(T entity)
        {
            var model= baseRepository.AddEntity(entity);
            DBSession.SaveChagesAsync();

            return await Task.FromResult(model);
        }

        public Task<bool> UpdateEntityAsync(T entity)
        {
            var result = baseRepository.UpdateEntity(entity);
            DBSession.SaveChagesAsync();

            return Task.FromResult(result);
        }

        public Task<bool> DeleteEntityAsync(T entity)
        {
            var result = baseRepository.DeleteEntity(entity);
            DBSession.SaveChagesAsync();

            return Task.FromResult(result);
        }

        public Task<IQueryable<T>> LoadEntityAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            var data= baseRepository.LoadEntity(whereLambda);

            return Task.FromResult(data);
        }

        public Task<IQueryable<T>> LoadPageEntityAsync<S>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, int? pageIndex, int? pageSize, out int total, System.Linq.Expressions.Expression<Func<T, S>> orderLambda, bool isAsc)
        {
            var data= baseRepository.LoadPageEntity(whereLambda, pageIndex, pageSize, out total, orderLambda, isAsc);

            return Task.FromResult(data);
        }
    }
}
