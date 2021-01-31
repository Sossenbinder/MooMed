using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Repository.Interface;
using MooMed.Common.Definitions.Interface;
using MooMed.DotNet.Extensions;

namespace MooMed.Common.Database.Repository
{
	public abstract class AbstractCrudRepository<TDbContext, TEntity, TKeyType> : ICrudRepository<TEntity, TKeyType>
		where TEntity : class, IEntity<TKeyType>
		where TDbContext : DbContext
	{
		private readonly IDbContextFactory<TDbContext> _contextFactory;

		protected AbstractCrudRepository(IDbContextFactory<TDbContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		protected TDbContext CreateContext()
		{
			return _contextFactory.CreateDbContext();
		}

		public async Task<TEntity> Create(TEntity entity)
		{
			var creationResult = await RunInContextAndCommitWithResult(async set =>
			{
				var entityEntry = await set.AddAsync(entity);

				return entityEntry.Entity;
			});

			return creationResult;
		}

		public Task<List<TEntity>> Read()
			=> RunInContextWithResult(dbSet => dbSet.AsNoTracking().ToListAsync());

		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate)
			=> RunInContextWithResult(dbSet => dbSet.AsNoTracking().Where(predicate).ToListAsync());

		public Task<List<TEntity>> Read(params Expression<Func<TEntity, bool>>[] filters)
			=> Read(filters.AsEnumerable());

		public async Task<List<TEntity>> Read(IEnumerable<Expression<Func<TEntity, bool>>> filters)
		{
			var predicateList = filters.ToList();

			if (predicateList.IsNullOrEmpty())
			{
				return await RunInContextWithResult(dbSet => dbSet.ToListAsync());
			}

			var predicate = predicateList.Aggregate((current, filter) => current.And(filter));

			return await RunInContextWithResult(dbSet => dbSet
				.AsNoTracking()
				.Where(predicate)
				.ToListAsync());
		}

		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] foreignIncludes)
		{
			// Assemble all includes. With queryable as seed, add .Include for all the includes passed in here
			var foreignIncludesAsFunc = new Func<IQueryable<TEntity>, IQueryable<TEntity>>(queryable => foreignIncludes.Aggregate(queryable, (current, include) => current.Include(include)));

			return Read(predicate, foreignIncludesAsFunc);
		}

		public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> predicate, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] foreignIncludes)
		{
			return RunInContextWithResult(dbSet =>
			{
				var query = dbSet
					.AsNoTracking()
					.Where(predicate);

				query = foreignIncludes.Aggregate(query, (current, include) => include(current));

				return query.ToListAsync();
			});
		}

		public Task<bool> Update(TEntity entity)
		{
			return RunInContextAndCommit(set =>
			{
				set.Update(entity);
				return default;
			});
		}

		public Task Update(Expression<Func<TEntity, bool>> selector, Action<TEntity> updateFunc)
		{
			return RunInContextAndCommit(async set =>
			{
				var existingEntity = await set.Where(selector).SingleOrDefaultAsync();

				if (existingEntity == null)
				{
					return;
				}

				updateFunc(existingEntity);
			});
		}

		public Task Delete(TEntity entity)
		{
			return RunInContextAndCommit(async set =>
			{
				var existingEntity = await set.SingleOrDefaultAsync(x => x.Equals(entity));

				if (existingEntity == null)
				{
					return;
				}

				set.Remove(existingEntity);
			});
		}

		public Task Delete(Expression<Func<TEntity, bool>> selector)
		{
			return RunInContextAndCommit(async set =>
			{
				var existingEntity = await set.Where(selector).SingleOrDefaultAsync();

				if (existingEntity == null)
				{
					return;
				}

				set.Remove(existingEntity);
			});
		}

		public async Task CreateOrUpdate(TEntity entity, Action<TEntity> updateFunc)
		{
			await using var ctx = CreateContext();

			var set = ctx.Set<TEntity>();

			var existingItem = await set.SingleOrDefaultAsync(x => x.Equals(entity));

			if (existingItem != null)
			{
				updateFunc(existingItem);
			}
			else
			{
				// ReSharper disable once MethodHasAsyncOverload
				set.Add(entity);
			}

			await ctx.SaveChangesAsync();
		}

		public async Task CreateOrUpdate(List<TEntity> entities, Action<TEntity> updateFunc)
		{
			await using var ctx = CreateContext();

			var set = ctx.Set<TEntity>();

			var existingEntities = await set
				.Where(x => entities.Contains(x))
				.ToListAsync();

			foreach (var entity in existingEntities.Where(entity => entity != null))
			{
				updateFunc(entity);
			}

			foreach (var entity in entities.Except(existingEntities))
			{
				// ReSharper disable once MethodHasAsyncOverload
				set.Add(entity);
			}

			await ctx.SaveChangesAsync();
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

		private TDbContext GetContext() => _contextFactory.CreateDbContext();
	}
}