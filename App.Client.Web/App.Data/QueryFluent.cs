using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using App.Core;
using App.Core.Interface.Data;

namespace App.Data
{
	public sealed class QueryFluent<TEntity> : IQueryFluent<TEntity> where TEntity : class
	{
		public QueryFluent(EfRepository<TEntity> repository)
		{
			_repository = repository;
			_includeProperties = new List<Expression<Func<TEntity, object>>>();
			_filters = new List<Expression<Func<TEntity, bool>>>();
		}

		public QueryFluent(EfRepository<TEntity> repository, Expression<Func<TEntity, bool>> expression) : this(repository)
		{
			_filters.Add(expression);
		}

		public IQueryFluent<TEntity> Filter(Expression<Func<TEntity, bool>> expression)
		{
			_filters.Add(expression);
			return this;
		}

		public IQueryFluent<TEntity> Sort(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> expression)
		{
			_sortExpression = expression;
			return this;
		}

		public IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> includeProperties)
		{
			_includeProperties.Add(includeProperties);
			return this;
		}

		public IPagedList<TEntity> GetPagedList(int pageIndex, int pageSize)
		{
			return _repository.GetPagedList(_filters, _sortExpression, pageIndex, pageSize, _includeProperties);
		}

		public IEnumerable<TEntity> Get(bool enableTracking = false)
		{
			return _repository.Get(_filters, _sortExpression, _includeProperties, enableTracking: enableTracking);
		}

		public TEntity GetSingle(bool enableTracking = false)
		{
			return
				_repository.Get(filters: _filters, includeProperties: _includeProperties, enableTracking: enableTracking)
					.SingleOrDefault();
		}

		#region Private Fields

		private readonly EfRepository<TEntity> _repository;
		private readonly List<Expression<Func<TEntity, bool>>> _filters;
		private readonly List<Expression<Func<TEntity, object>>> _includeProperties;
		private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _sortExpression;

		#endregion Private Fields
	}
}