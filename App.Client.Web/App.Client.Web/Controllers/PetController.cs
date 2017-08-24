using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using App.Client.Web.Models.ViewModels;
using App.Data;
using App.Core.Interface.Service;
using System.Collections.Generic;

namespace App.Client.Web.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }
        // GET: Pet
        public ActionResult Index()
        {
            List<PetViewModel> model = _petService.GetPets().Select(s => s.ToViewModel()).ToList();
            return View(model);
        }

        // GET: Pet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PetViewModel petViewModel = _petService.GetPetById(id.Value).ToViewModel();
            if (petViewModel == null)
                return HttpNotFound();

            return View(petViewModel);
        }

        // GET: Pet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Name")] PetViewModel petViewModel)
        {
            if (ModelState.IsValid)
            {
                _petService.CreatePet(petViewModel.ToDomain());
                return RedirectToAction("Index");
            }

            return View(petViewModel);
        }

        // GET: Pet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PetViewModel petViewModel = _petService.GetPetById(id.Value).ToViewModel();
            if (petViewModel == null)
                return HttpNotFound();

            return View(petViewModel);
        }

        // POST: Pet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Name")] PetViewModel petViewModel)
        {
            if (ModelState.IsValid)
            {
                _petService.UpdatePet(petViewModel.ToDomain());
                return RedirectToAction("Index");
            }
            return View(petViewModel);
        }

        // GET: Pet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PetViewModel petViewModel = _petService.GetPetById(id.Value).ToViewModel();
            if (petViewModel == null)
                return HttpNotFound();

            return View(petViewModel);
        }

        // POST: Pet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _petService.DeletePet(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
