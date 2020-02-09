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
		where TEntity : class, IDatabaseEntity 
		where TContextFactory : AbstractDbContextFactory<TDbContext>
		where TDbContext : AbstractDbContext
	{
		[NotNull]
		private readonly TContextFactory m_contextFactory;

		protected AbstractCrudRepository([NotNull] TContextFactory contextFactory)
		{
			m_contextFactory = contextFactory;
		}

		protected TDbContext CreateContext()
		{
			return m_contextFactory.CreateContext();
		}

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

		private async Task<List<TEntity>> RunInContextWithResult([NotNull] Func<DbSet<TEntity>, Task<List<TEntity>>> dbFunc)
		{
			await using var ctx = m_contextFactory.CreateContext();
			return await dbFunc(ctx.Set<TEntity>());
		}

		private async Task RunInContextAndCommit([NotNull] Func<DbSet<TEntity>, Task> contextFunc)
		{
			await using var ctx = m_contextFactory.CreateContext();
			await contextFunc(ctx.Set<TEntity>());
			await ctx.SaveChangesAsync();
		}
	}
}
