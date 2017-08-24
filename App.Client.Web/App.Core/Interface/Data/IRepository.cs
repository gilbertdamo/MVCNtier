using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Core.Interface.Data
{
	public partial interface IRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> Table { get; }
		TEntity GetById(object id);
		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);
		void Update(TEntity entityToUpdate);

		/// <summary>
		///   Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
		///   operations
		/// </summary>
		IQueryable<TEntity> TableNoTracking { get; }
		IQueryFluent<TEntity> Query();
		IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
	}
}
