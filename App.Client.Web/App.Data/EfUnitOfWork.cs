using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using App.Core.Interface.Data;

namespace App.Data
{
	public class EfUnitOfWork : IUnitOfWork
	{
		private AppDbContext _context;
		private Hashtable _repositories;

		public IRepository<T> Repository<T>() where T : class
		{
			if (_repositories == null)
				_repositories = new Hashtable();

			var type = typeof(T).Name;

			if (!_repositories.ContainsKey(type))
			{
				var repositoryType = typeof(EfRepository<>);

				var repositoryInstance =
					Activator.CreateInstance(repositoryType
						.MakeGenericType(typeof(T)), _context);

				_repositories.Add(type, repositoryInstance);
			}

			return (IRepository<T>) _repositories[type];
		}

		#region Methods

		public EfUnitOfWork()
		{
			_context = new AppDbContext();
		}

		public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
		{
			return _context.Database.SqlQuery<T>(sql, parameters);
		}

		public IEnumerable<T> SQLQuery2<T>(string sql, params object[] parameters)
		{
			return _context.Database.SqlQuery<T>(sql, parameters);
		}

		public int SQLCommand(string sql, params object[] parameters)
		{
			return _context.Database.ExecuteSqlCommand(sql,parameters);
		}

		public System.Data.Common.DbTransaction BeginTransaction()
		{
			System.Data.Entity.DbContextTransaction transaction = _context.Database.BeginTransaction();
			return transaction.UnderlyingTransaction;
		}

		public int SaveChanges()
		{
			try
			{
				return _context.SaveChanges();
			}
			catch (DbEntityValidationException dbEx)
			{
				throw new Exception(GetFullErrorText(dbEx), dbEx);
			}
		}

		public bool HasChanges
		{
			get
			{
				return _context.ChangeTracker.HasChanges();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				if (_context != null)
				{
					_context.Dispose();
					_context = null;
				}
		}

		protected string GetFullErrorText(DbEntityValidationException exc)
		{
			var msg = string.Empty;
			foreach (var validationErrors in exc.EntityValidationErrors)
			foreach (var error in validationErrors.ValidationErrors)
				msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
			return msg;
		}

		#endregion
	}
}