using App.Core.Interface.Data;
using App.Data;
using StructureMap;

namespace App.Client.Web.Infrastructure
{
	public class StandardRegistry : Registry
	{
		public StandardRegistry()
		{
			Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.WithDefaultConventions();
			});

			// Services
			Scan(
				scan =>
				{
					scan.Assembly("App.Services");
					scan.WithDefaultConventions();
				});

			// Data
			For(typeof(IRepository<>)).Use(typeof(EfRepository<>));
			For(typeof(IUnitOfWork)).Use(typeof(EfUnitOfWork));
		}
	}
}