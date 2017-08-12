using ShopNC.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Repository
{
    //数据访问层基类
    public class BaseRepository<T>:IBaseRepository<T>
        where T : class,new()
    {
        //解耦了 当前UserInfoRepository跟上下文的直接依赖。
        //保证了线程内上下文的实例唯一

        private  IDBContextFactory dbContextFactory = new EFContextFactory();

        protected DbContext db;
        public BaseRepository()
        {
            db = dbContextFactory.GetCurrentContextInstence();

        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            if (entity != null)
            {
                db.Set<T>().Add(entity);
               // db.SaveChanges();

            }
            return entity;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateEntity(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Modified;

           // db.SaveChanges();
            return true;
        }

        /// <summary>
        /// 删除实体 删除的时候只需要给主键赋值就可以
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {

            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;

           // db.SaveChanges();
            return  true;

        }

        /// <summary>
        /// 根据条件获取实体列表(不分页)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntity(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda);
        }

        /// <summary>
        /// 根据条件获取实体列表(分页)
        /// </summary>
        /// <typeparam name="S">排序类型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="total">总记录数</param>
        /// <param name="orderLambda">排序Lambda</param>
        /// <param name="ifAsc">是否自增</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntity<S>(Expression<Func<T, bool>> whereLambda, int? pageIndex, int? pageSize, out int total, Expression<Func<T, S>> orderLambda, bool isAsc)
        {
            int index = pageIndex ?? 1;
            int size = pageSize ?? 10;

            var data = db.Set<T>().Where<T>(whereLambda);

            total = data.Count();

            data = isAsc ? data.OrderBy(orderLambda) : data.OrderByDescending(orderLambda);
            data = data.Skip<T>(size * (index - 1)).Take<T>(size);

            return data;
        }
    }
}
