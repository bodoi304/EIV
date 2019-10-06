using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.Interface
{
   //Các method cần có cho 1 DB context
   public interface IDBContextHelper
    {
        IQueryable<T> GetTable<T>() where T : class;

        /// <summary>
        /// Get all data and return a list of objects of a particular type, where the type is defined by the T parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetAll<T>() where T : class;

        /// <summary>
        /// Get limited data by paging.
        /// Return a list of objects of a particular type, where the type is defined by the T parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> GetAll<T>(int pageNumber, int pageSize) where T : class;

        /// <summary>
        /// Return a list of entities by filter (T parameter is a type of the entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Return a list of entities by filter and paging (T parameter is a type of the entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> Filter<T>(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, String namePropertyOrder)
       where T : class;

        /// <summary>
        /// Return an entity if exist, or not return Null. (T parameter is a type of the entity)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetOne<T>(Expression<Func<T, bool>> predicate) where T : class;
        /// <summary>
        /// Check an entity is exist in database. (T parameter is a type of the entity).
        /// Return True if the entity is exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool EntityExists<T>(Expression<Func<T, bool>> predicate) where T : class;
        /// <summary>
        /// Insert an entity into database (T parameter is a type of the entity).
        /// Notes: After inserting, this entity is mapping with database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Insert<T>(T entity) where T : class;
        /// <summary>
        /// Insert a list of entities (T parameter is a type of the entity).
        /// Notes: After inserting, this list is mapping with database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        void Insert<T>(List<T> entityList) where T : class;

        /// <summary>
        /// Update an entity by expression (T parameter is a type of the entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        void Update<T>(T entity, Expression<Func<T, bool>> predicate) where T : class;

        void Update<T>(T entity) where T : class;

        void AddOrUpdate<T>(T entity, Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Delete data of one table has name is T parameter by expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Delete all data of one table has name is T parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void DeleteAll<T>() where T : class;

        #region Transaction Controller
        void BeginTransaction();
        void RollbackTransaction();

        void CommitTransaction();
        #endregion

        void SaveChange();

    }
}
