using App.Core.Domain;

namespace App.Client.Web.Models.ViewModels
{
    public static class ModelFactory
    {
        public static PetViewModel ToViewModel(this Pet src)
        {
            if (src == null) return null;

            return new PetViewModel()
            {
                Id = src.Id,
                Type = src.Type,
                Name = src.Name
            };
        }

        public static Pet ToDomain(this PetViewModel src)
        {
            if (src == null) return null;

            return new Pet()
            {
                Id = src.Id,
                Type = src.Type,
                Name = src.Name
            };
        }

    }
}