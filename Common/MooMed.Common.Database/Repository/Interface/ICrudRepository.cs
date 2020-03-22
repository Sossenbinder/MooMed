using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Repository.Interface
{
	public interface ICrudRepository<TEntity>
		where TEntity: IEntity
	{
		Task<bool> Create([NotNull] TEntity entity);

		Task<List<TEntity>> Read([NotNull] Expression<Func<TEntity, bool>> predicate);

		Task<List<TEntity>> Read([NotNull] Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] foreignIncludes);

		Task Update(TEntity newItem, Action<TEntity> updateFunc);

		Task Delete([NotNull] TEntity model);
	}
}
