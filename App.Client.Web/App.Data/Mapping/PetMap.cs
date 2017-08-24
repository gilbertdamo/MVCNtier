using App.Core.Domain;

namespace App.Data.Mapping
{
    public class PetMap : AppEntityTypeConfiguration<Pet>
    {
        public PetMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.Name)
            .IsRequired();

            Property(t => t.Type)
            .IsRequired();



            // Table & Column Mappings
            ToTable("Pet");

            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Type).HasColumnName("Type");
        }
    }
}
