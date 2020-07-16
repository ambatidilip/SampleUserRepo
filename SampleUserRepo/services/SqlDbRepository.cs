using Microsoft.EntityFrameworkCore;
using SampleUserRepo.Context;
using SampleUserRepo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleUserRepo.services
{
    public class SqlDbRepository : IDbRepository
    {
        /// <summary>
        /// The disposed value.
        /// </summary>
        private bool disposedValue = false;

        private crsuserauthdeContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDbRepository"/> class.
        /// </summary>
        /// <param name="cRSContext">The context.</param>
        public SqlDbRepository(crsuserauthdeContext cRSContext)
        {
            this.dbContext = cRSContext ?? throw new ArgumentNullException(nameof(cRSContext));
            this.dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.dbContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        /// <returns>Gets all.</returns>
        public IQueryable<TEntity> GetItems<TEntity>()
            where TEntity : class
        {
            try
            {
                this.dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                return this.dbContext.Set<TEntity>().Select(o => o);
            }
            finally
            {
                this.dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="T">{Generic Type T}.</typeparam>
        /// <param name="queryText">The queryText.</param>
        /// <param name="sqlParameters">The sqlParameters.</param>
        /// <returns>Gets by conditions</returns>
        public Task<IEnumerable<T>> GetItemsAsync<T>(string queryText, Dictionary<string, object> sqlParameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="T">{Generic Type T}.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Gets by conditions</returns>
        public Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert the items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        public void InsertItems<TEntity>(List<TEntity> items)
            where TEntity : class
        {
            try
            {
                this.dbContext.BeginTransaction();
                foreach (var item in items)
                {
                    this.dbContext.Set<TEntity>().Add(item);
                }

                this.dbContext.SaveChanges();
                this.dbContext.Commit();
            }
            catch (Exception ex)
            {
                this.dbContext.Rollback();
                throw;
            }

        }

        /// <summary>
        /// Insert the item.
        /// </summary>
        /// <param name="items">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        public void InsertItem<TEntity>(TEntity item)
            where TEntity : class
        {
            try
            {
                this.dbContext.BeginTransaction();
                this.dbContext.Set<TEntity>().Add(item);
                this.dbContext.SaveChanges();
                this.dbContext.Commit();
            }
            catch (Exception ex)
            {
                this.dbContext.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Update the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        public void UpdateItem<TEntity>(TEntity item)
            where TEntity : class
        {
            try
            {
                this.dbContext.BeginTransaction();
                this.dbContext.Update<TEntity>(item);
                this.dbContext.SaveChanges();
                this.dbContext.Commit();
            }
            catch (Exception ex)
            {
                this.dbContext.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Delete the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        public void DeleteItem<TEntity>(TEntity item)
            where TEntity : class
        {
            try
            {
                this.dbContext.BeginTransaction();
                this.dbContext.Remove<TEntity>(item);
                this.dbContext.SaveChanges();
                this.dbContext.Commit();
            }
            catch (Exception ex)
            {
                this.dbContext.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Delete the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        public void DeleteRange<TEntity>(List<TEntity> item)
            where TEntity : class
        {
            try
            {
                this.dbContext.BeginTransaction();
                this.dbContext.RemoveRange(item);
                this.dbContext.SaveChanges();
                this.dbContext.Commit();
            }
            catch (Exception ex)
            {
                this.dbContext.Rollback();
                throw;
            }
        }

        public void InsertItems<TEntity, CEntity>(TEntity first, CEntity second)
            where TEntity : class
            where CEntity : class
        {
            try
            {
                this.dbContext.BeginTransaction();
                this.dbContext.Set<TEntity>().Add(first);
                this.dbContext.SaveChanges();
                this.dbContext.Set<CEntity>().Add(second);
                this.dbContext.SaveChanges();
                this.dbContext.Commit();
            }
            catch (Exception ex)
            {
                this.dbContext.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Check the item primary key.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        public bool CheckExistsPrimaryKey<TEntity>(TEntity item) where TEntity : class
        {
            var set = dbContext.Set<TEntity>();

            var entityType = dbContext.Model.FindEntityType(typeof(TEntity));
            var primaryKey = entityType.FindPrimaryKey();
            var keyValues = new object[primaryKey.Properties.Count];


            for (int i = 0; i < keyValues.Length; i++)
                keyValues[i] = primaryKey.Properties[i].GetGetter().GetClrValue(item);

            var obj = set.Find(keyValues);

            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Check the item exists.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity">{Generic Type TEntity}.</typeparam>
        public bool CheckExists<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var set = dbContext.Set<T>();
            var exists = set.Select(o => o).Any(predicate);
            return exists;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    var disposableClient = this.dbContext as IDisposable;
                    disposableClient?.Dispose();
                }

                this.disposedValue = true;
            }
        }


    }
}
