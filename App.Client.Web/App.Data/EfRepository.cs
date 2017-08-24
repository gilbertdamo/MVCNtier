using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using App.Core;
using App.Core.Interface.Data;

namespace App.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		internal AppDbContext Context;
		internal DbSet<TEntity> DbSet;

		public EfRepository(AppDbContext context)
		{
			this.Context = context;
			this.DbSet = context.Set<TEntity>();
		}

		public TEntity GetById(object id)
		{
			return DbSet.Find(id);
		}

		public void Add(TEntity entity)
		{
			DbSet.Add(entity);
		}

		public void AddRange(IEnumerable<TEntity> entities)
		{
			DbSet.AddRange(entities);
		}

		public void Remove(TEntity entity)
		{
			DbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			DbSet.RemoveRange(entities);
		}

		public void Update(TEntity entityToUpdate)
		{
			DbSet.Attach(entityToUpdate);
			Context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public IQueryable<TEntity> TableNoTracking
		{
			get { return DbSet.AsNoTracking(); }
		}

		public IQueryFluent<TEntity> Query()
		{
			return new QueryFluent<TEntity>(this);
		}

		public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
		{
			return new QueryFluent<TEntity>(this, query);
		}

		internal IEnumerable<TEntity> Get(List<Expression<Func<TEntity, bool>>> filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, bool enableTracking = false)
		{
			IQueryable<TEntity> query = DbSet;

			if (filters != null)
			{
				foreach (Expression<Func<TEntity, bool>> filter in filters)
					query = query.Where(filter);
			}
			if (includeProperties.Count > 0)
				query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

			if (orderBy != null)
				query = orderBy(query);

			return enableTracking ? query.ToList() : query.AsNoTracking().ToList();
		}

		internal IPagedList<TEntity> GetPagedList(List<Expression<Func<TEntity, bool>>> filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int pageNumber = 0, int pageSize = int.MaxValue, List<Expression<Func<TEntity, object>>> includeProperties = null)
		{
			IQueryable<TEntity> query = DbSet;

			if (filters != null)
			{
				foreach (Expression<Func<TEntity, bool>> filter in filters)
					query = query.Where(filter);
			}

			if (includeProperties.Count > 0)
				query = includeProperties.Aggregate(query, (current, include) => current.Include(include));

			if (orderBy != null)
				query = orderBy(query);

			IEnumerable<TEntity> result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToList();
			int totalCount = query.Count();
			return new PagedList<TEntity>(result, pageNumber, pageSize, totalCount);
		}

		public IQueryable<TEntity> Table
		{
			get { return DbSet; }
		}

	}
}