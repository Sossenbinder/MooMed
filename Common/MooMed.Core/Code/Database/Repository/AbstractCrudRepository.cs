using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Definitions.Interface;
using MooMed.Core.Code.Database.Repository.Interface;

namespace MooMed.Core.Code.Database.Repository
{
	public abstract class AbstractCrudRepository<TContext, TEntity> : ICrudRepository<TEntity> 
		where TEntity : class, IDatabaseEntity where TContext : BaseDbContext, new()
	{
		[NotNull]
		public Task Create(TEntity entity)
			=> RunInContextAndCommit(set => set.AddAsync(entity).AsTask());

		[NotNull]
		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate)
			=> RunInContextWithResult(dbSet => dbSet.Where(predicate).ToListAsync());

		[NotNull]
		public Task Update(TEntity newEntity, Action<TEntity> updateFunc)
			=> RunInContextAndCommit(async set =>
			{
				var existingEntity = await set.FirstOrDefaultAsync(dbEntity => dbEntity.GetKey().Equals(newEntity.GetKey()));

				if (existingEntity == null)
				{
					return;
				}

				updateFunc(existingEntity);

				set.Update(existingEntity);
			});

		[NotNull]
		public Task Delete(TEntity entityToDelete)
			=> RunInContextAndCommit(async set =>
			{
				var existingEntity = await set.FirstOrDefaultAsync(dbEntity => dbEntity.GetKey().Equals(entityToDelete.GetKey()));

				if (existingEntity == null)
				{
					return;
				}

				set.Remove(entityToDelete);
			});

		private static async Task<List<TEntity>> RunInContextWithResult([NotNull] Func<DbSet<TEntity>, Task<List<TEntity>>> dbFunc)
		{
			using (var ctx = new TContext())
			{
				return await dbFunc(ctx.Set<TEntity>());
			}
		}

		private static async Task RunInContextAndCommit([NotNull] Func<DbSet<TEntity>, Task> contextFunc)
		{
			using (var ctx = new TContext())
			{
				await contextFunc(ctx.Set<TEntity>());
				await ctx.SaveChangesAsync();
			}
		}
	}
}
