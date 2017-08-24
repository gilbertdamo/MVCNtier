using App.Core.Domain;
using System.Collections.Generic;

namespace App.Core.Interface.Service
{
    public interface IPetService
    {
        void CreatePet(Pet pet);
        void UpdatePet(Pet pet);
        void DeletePet(int petId);
        Pet GetPetById(int petId);
        IEnumerable<Pet> GetPets();
    }
}
