using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Repository.Interface
{
	public interface ICrudRepository<TEntity, in TKeyType> where TEntity: IEntity<TKeyType>
	{
		// Basic
		Task<bool> Create([NotNull] TEntity entity);

		Task<List<TEntity>> Read([NotNull] Expression<Func<TEntity, bool>> predicate);

		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] foreignIncludes);

		Task<List<TEntity>> Read([NotNull] Expression<Func<TEntity, bool>> predicate, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] foreignIncludes);

		Task Update([NotNull] TEntity entity, [NotNull] Action<TEntity> updateFunc);

		Task Update([NotNull] TKeyType key, [NotNull] Action<TEntity> updateFunc);

		Task Delete([NotNull] TEntity model);

		Task Delete([NotNull] TKeyType key);

		// Extended
		Task CreateOrUpdate([NotNull] TEntity entity, [NotNull] Action<TEntity> updateFunc);
	}
}
