using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Core.Code.Database.Repository.Interface
{
	public interface ICrudRepository<TEntity>
		where TEntity : IDatabaseEntity
	{
		Task Create([NotNull] TEntity entity);

		Task<List<TEntity>> Read([NotNull] Expression<Func<TEntity, bool>> predicate);

		Task Update(TEntity newEntity, Action<TEntity> updateFunc);

		Task Delete([NotNull] TEntity entity);
	}
}
