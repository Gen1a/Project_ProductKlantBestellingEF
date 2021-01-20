using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityFrameworkRepository.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// READ operation.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> List();

        /// <summary>
        /// READ operation.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// READ operation.
        /// </summary>
        /// <returns></returns>
        T GetById(long id);

        /// <summary>
        /// CREATE operation.
        /// </summary>
        /// <returns></returns>
        long Create(T entity);

        /// <summary>
        /// DELETE operation.
        /// </summary>
        /// <returns></returns>
        void Delete(T entity);

        /// <summary>
        /// UPDATE operation.
        /// </summary>
        /// <returns></returns>
        void Update(T entity);
    }
}