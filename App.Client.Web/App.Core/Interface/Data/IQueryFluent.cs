using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Core.Interface.Data
{
	public interface IQueryFluent<TEntity> where TEntity : class
	{
		IQueryFluent<TEntity> Filter(Expression<Func<TEntity, bool>> expression);
		IQueryFluent<TEntity> Sort(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> expression);
		IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> includeProperties);
		IPagedList<TEntity> GetPagedList(int pageIndex, int pageSize);
		IEnumerable<TEntity> Get(bool enableTracking = false);
		TEntity GetSingle(bool enableTracking = false);
	}
}