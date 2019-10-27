using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_API.Interfaces
{
    /// <summary>
    /// BaseEntity (interface)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseEntity<T>
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Get();

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<T> Get(string value);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Updates the specified key.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> Update(T entity);

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<bool> Remove(string value);
    }
}
