using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Common.Definitions.Interface;
using MooMed.DotNet.Extensions;

namespace MooMed.Common.Database.Repository
{
	public abstract class AbstractCrudRepository<TDbContext, TEntity, TKeyType> : ICrudRepository<TEntity, TKeyType> 
		where TEntity : class, IEntity<TKeyType>
		where TDbContext : AbstractDbContext
	{
		[NotNull]
		private readonly AbstractDbContextFactory<TDbContext> _contextFactory;

		protected AbstractCrudRepository(
			[NotNull] AbstractDbContextFactory<TDbContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		protected TDbContext CreateContext()
		{
			return _contextFactory.CreateContext();
		}

		[NotNull]
		public async Task<TEntity> Create(TEntity entity)
		{
			var creationResult = await RunInContextAndCommitWithResult(async set =>
			{
				var thing = await set.AddAsync(entity);

				return thing.Entity;
			});

			return creationResult;
		}

		[NotNull]
		public Task<List<TEntity>> Read()
			=> RunInContextWithResult(dbSet => dbSet.ToListAsync());

		[NotNull]
		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate)
			=> RunInContextWithResult(dbSet => dbSet.Where(predicate).ToListAsync());

		[NotNull]
		public Task<List<TEntity>> Read(params Expression<Func<TEntity, bool>>[] filters)
			=> Read(filters.AsEnumerable());

		[NotNull]
		public async Task<List<TEntity>> Read([CanBeNull] IEnumerable<Expression<Func<TEntity, bool>>> filters)
		{
			var predicateList = filters?.ToList();

			if (predicateList.IsNullOrEmpty())
			{
				return await RunInContextWithResult(dbSet => dbSet.ToListAsync());
			}

			var predicate = PredicateBuilder.New<TEntity>(x => true);

			predicate = predicateList.Aggregate(predicate, (current, filter) => current.And(filter));

			return await RunInContextWithResult(dbSet => dbSet.Where(predicate).ToListAsync());
		}

		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] foreignIncludes)
			=> Read(predicate, queryable => foreignIncludes.Aggregate(queryable, (current, include) => current.Include(include)));

		[NotNull]
		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] foreignIncludes)
		{
			return RunInContextWithResult(dbSet =>
			{
				var query = dbSet
					.Where(predicate);

				query = foreignIncludes.Aggregate(query, (current, include) => include(current));

				return query.ToListAsync();
			});
		}

		[NotNull]
		public Task Update(TEntity entity, Action<TEntity> updateFunc)
			=> Update(entity.Id, updateFunc);

		public Task Update(TKeyType key, Action<TEntity> updateFunc)
			=> RunInContextAndCommit(async set =>
			{
				var existingEntity = (await Read(x => x.Id.Equals(key))).SingleOrDefault();

				if (existingEntity == null)
				{
					return;
				}

				updateFunc(existingEntity);

				set.Update(existingEntity);
			});

		[NotNull]
		public Task Delete(TEntity entityToDelete)
			=> Delete(entityToDelete.Id);

		public Task Delete(TKeyType key)
			=> RunInContextAndCommit(async set =>
			{
				var existingEntity = (await Read(x => x.Id.Equals(key))).SingleOrDefault();

				if (existingEntity == null)
				{
					return;
				}

				set.Remove(existingEntity);
			});

		public async Task CreateOrUpdate(TEntity entity,  Action<TEntity> updateFunc)
		{
			var existingItem = (await Read(x => x.Id.Equals(entity.Id))).SingleOrDefault();

			if (existingItem == null)
			{
				await Create(entity);
			}
			else
			{
				await Update(entity, updateFunc);
			}
		}

		private async Task<TEntity> RunInContextWithResult([NotNull] Func<DbSet<TEntity>, Task<TEntity>> dbFunc)
		{
			await using var ctx = GetContext();

			return await dbFunc(ctx.Set<TEntity>());
		}

		private async Task<List<TEntity>> RunInContextWithResult([NotNull] Func<DbSet<TEntity>, Task<List<TEntity>>> dbFunc)
		{
			await using var ctx = GetContext();

			return await dbFunc(ctx.Set<TEntity>());
		}

		private async Task<bool> RunInContextAndCommit([NotNull] Func<DbSet<TEntity>, ValueTask> contextFunc)
		{
			await using var ctx = GetContext();

			await contextFunc(ctx.Set<TEntity>());
			var affectedRows = await ctx.SaveChangesAsync();

			return affectedRows != 0;
		}

		private async Task<TEntity> RunInContextAndCommitWithResult([NotNull] Func<DbSet<TEntity>, Task<TEntity>> contextFunc)
		{
			await using var ctx = GetContext();

			var entity = await contextFunc(ctx.Set<TEntity>());
			await ctx.SaveChangesAsync();

			return entity;
		}

		private TDbContext GetContext() => _contextFactory.CreateContext();
	}
}
