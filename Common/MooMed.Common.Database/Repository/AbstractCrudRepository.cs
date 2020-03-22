using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Database.Repository
{
	public abstract class AbstractCrudRepository<TContextFactory, TDbContext, TEntity> : ICrudRepository<TEntity> 
		where TEntity : class, IEntity 
		where TContextFactory : AbstractDbContextFactory<TDbContext>
		where TDbContext : AbstractDbContext
	{
		[NotNull]
		private readonly TContextFactory m_contextFactory;

		protected AbstractCrudRepository(
			[NotNull] TContextFactory contextFactory)
		{
			m_contextFactory = contextFactory;
		}

		protected TDbContext CreateContext()
		{
			return m_contextFactory.CreateContext();
		}

		[NotNull]
		public Task<bool> Create(TEntity entity)
			=> RunInContextAndCommit(async set => await set.AddAsync(entity));

		[NotNull]
		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate)
			=> RunInContextWithResult(dbSet => dbSet.Where(predicate).ToListAsync());

		[NotNull]
		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] foreignIncludes)
		{
			return RunInContextWithResult(async dbSet =>
			{
				var query = dbSet
					.Where(predicate);

				query = foreignIncludes.Aggregate(
					query, 
					(current, foreignInclude) => current.Include(foreignInclude));

				return await query.ToListAsync();
			});
		}

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

		private async Task<List<TEntity>> RunInContextWithResult([NotNull] Func<DbSet<TEntity>, Task<List<TEntity>>> dbFunc)
		{
			await using var ctx = m_contextFactory.CreateContext();
			return await dbFunc(ctx.Set<TEntity>());
		}

		private async Task<bool> RunInContextAndCommit([NotNull] Func<DbSet<TEntity>, ValueTask> contextFunc)
		{
			await using var ctx = m_contextFactory.CreateContext();
			await contextFunc(ctx.Set<TEntity>());
			var affectedRows = await ctx.SaveChangesAsync();

			return affectedRows != 0;
		}

		private async Task RunInContextAndCommitWithResult([NotNull] Func<DbSet<TEntity>, Task> contextFunc)
		{
			await using var ctx = m_contextFactory.CreateContext();
			await contextFunc(ctx.Set<TEntity>());
			await ctx.SaveChangesAsync();
		}
	}
}
