using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleUserRepo.Interfaces
{
    /// <summary>
    /// The DbRepository interface
    /// </summary>
    public interface IDbRepository
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        /// <returns>Gets all</returns>
        IQueryable<TEntity> GetItems<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="T">{Generic Type T}</typeparam>
        /// <param name="queryText">The queryText.</param>
        /// <param name="sqlParameters">The sqlParameters.</param>
        /// <returns>Gets by conditions</returns>
        Task<IEnumerable<T>> GetItemsAsync<T>(string queryText, Dictionary<string, object> sqlParameters);

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="T">{Generic Type T}</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Gets by conditions</returns>
        Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Insert the items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        void InsertItems<TEntity, CEntity>(TEntity first, CEntity second)
            where TEntity : class
            where CEntity : class;

        /// <summary>
        /// Insert the items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        void InsertItems<TEntity>(List<TEntity> items)
            where TEntity : class;

        /// <summary>
        /// Insert the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        void InsertItem<TEntity>(TEntity item)
            where TEntity : class;

        /// <summary>
        /// Update the item.
        /// </summary>
        /// <param name="items">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        void UpdateItem<TEntity>(TEntity item)
            where TEntity : class;

        /// <summary>
        /// Delete the item.
        /// </summary>
        /// <param name="items">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        void DeleteItem<TEntity>(TEntity item)
            where TEntity : class;

        /// <summary>
        /// Delete the item.
        /// </summary>
        /// <param name="items">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        void DeleteRange<TEntity>(List<TEntity> item)
            where TEntity : class;

        /// <summary>
        /// Check the item exists.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        bool CheckExists<T>(Expression<Func<T, bool>> predicate) where T : class;

    }
}
