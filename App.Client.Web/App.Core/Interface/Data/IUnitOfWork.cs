using System;
using System.Collections.Generic;
using System.Data.Common;

namespace App.Core.Interface.Data
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> Repository<T>() where T : class;
		DbTransaction BeginTransaction();
		int SaveChanges();
		bool HasChanges { get; }
		IEnumerable<T> SQLQuery2<T>(string sql, params object[] parameters);
		int SQLCommand(string sql, params object[] parameters);
	}
}