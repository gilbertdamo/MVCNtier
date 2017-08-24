using System.Data.Entity.ModelConfiguration;

namespace App.Data.Mapping
{
	public abstract class AppEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
	{
		protected AppEntityTypeConfiguration()
		{
			PostInitialize();
		}

		protected virtual void PostInitialize()
		{

		}
	}
}