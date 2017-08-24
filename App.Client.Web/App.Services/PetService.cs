using App.Core.Interface.Service;
using System;
using System.Collections.Generic;
using App.Core.Domain;
using App.Core.Interface.Data;

namespace Apps.Services
{
    public class PetService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Pet> _petRepository;

        public PetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _petRepository = _unitOfWork.Repository<Pet>();
        }

        public void CreatePet(Pet pet)
        {
            if (pet == null)
                throw new ArgumentNullException(nameof(pet));
            _petRepository.Add(pet);
            _unitOfWork.SaveChanges();
        }

        public void DeletePet(int petId)
        {
            if (petId == 0)
                throw new ArgumentNullException(nameof(petId));

            Pet pet = _petRepository.GetById(petId);
            if (pet == null) return;

            _petRepository.Remove(pet);
            _unitOfWork.SaveChanges();

            return;
        }

        public Pet GetPetById(int petId)
        {
            if (petId == 0)
                throw new ArgumentNullException(nameof(petId));

            return _petRepository.GetById(petId);
        }

        public IEnumerable<Pet> GetPets()
        {
            return _petRepository.Query().Get();
        }

        public void UpdatePet(Pet pet)
        {
            if (pet == null)
                throw new ArgumentNullException(nameof(pet));
            _petRepository.Update(pet);
            _unitOfWork.SaveChanges();
        }
    }
}
