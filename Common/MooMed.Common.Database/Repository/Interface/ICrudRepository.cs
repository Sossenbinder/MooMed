using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Repository.Interface
{
    public interface ICrudRepository<TEntity, in TKeyType>
        where TEntity : class, IEntity<TKeyType>
    {
        // Basic
        Task<TEntity> Create(TEntity entity);

        Task<List<TEntity>> Read();

        Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> Read(params Expression<Func<TEntity, bool>>[] filters);

        Task<List<TEntity>> Read(IEnumerable<Expression<Func<TEntity, bool>>> predicate);

        Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] foreignIncludes);

        Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] foreignIncludes);

        Task Update(TEntity entity, [NotNull] Action<TEntity> updateFunc);

        Task Update(TKeyType key, [NotNull] Action<TEntity> updateFunc);

        Task Delete(TEntity model);

        Task Delete(TKeyType key);

        // Extended
        Task CreateOrUpdate(TEntity entity, Action<TEntity> updateFunc);

        Task CreateOrUpdate(List<TEntity> entities, Action<TEntity> updateFunc);
    }
}