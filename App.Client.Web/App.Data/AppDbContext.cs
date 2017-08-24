using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using App.Core.Domain;
using App.Data.Mapping;

namespace App.Data
{
	public class AppDbContext : DbContext
	{
		private const string ConnectionStringName = "Name=DbConnection";

		static AppDbContext()
		{
			Database.SetInitializer<AppDbContext>(null);
		}

		public AppDbContext()
			: base(ConnectionStringName)
		{
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//dynamically load all configuration
			//System.Type configType = typeof(AddressMap);   //any of your configuration classes here
			//var typesToRegister = Assembly.GetAssembly(configType).GetTypes()

			IEnumerable<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
				.Where(type => !String.IsNullOrEmpty(type.Namespace))
				.Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
											 type.BaseType.GetGenericTypeDefinition() == typeof(AppEntityTypeConfiguration<>));
			foreach (Type type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.Configurations.Add(configurationInstance);
			}
			//...or do it manually below. For example,
			//modelBuilder.Configurations.Add(new AddressMap());

			base.OnModelCreating(modelBuilder);
		}

		public static AppDbContext Create()
		{
			return new AppDbContext();
		}

    }
}