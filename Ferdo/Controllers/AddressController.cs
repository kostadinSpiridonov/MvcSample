using Ferdo.Data;
using Ferdo.Data.Entities;
using Ferdo.Data.Repositories;
using Ferdo.Mappings;
using Ferdo.Models;
using System.Web.Mvc;

namespace Ferdo.Controllers
{
    public class AddressController : Controller
    {
        private readonly BaseRepository<Address> addressRepository;

        public AddressController()
        {
            this.addressRepository = new BaseRepository<Address>(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            var models = this.addressRepository.GetAll();
            return View(Mapper.MapToAddressViewModel(models));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.addressRepository.Add(Mapper.MapToAddress(model));

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            var model = this.addressRepository.GetById(id);
            return View(Mapper.MapToAddressViewModel(model));
        }

        [HttpPost]
        public ActionResult Edit(AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(!this.addressRepository.Any(x=>x.Id==model.Id))
            {
                ModelState.AddModelError(string.Empty, "The address does not exist");
                return View(model);
            }

            this.addressRepository.Update(Mapper.MapToAddress(model));

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            this.addressRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}