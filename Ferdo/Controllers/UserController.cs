using Ferdo.Data;
using Ferdo.Data.Entities;
using Ferdo.Data.Repositories;
using Ferdo.Mappings;
using Ferdo.Models;
using System.Web.Mvc;

namespace Ferdo.Controllers
{
    public class UserController : Controller
    {
        private readonly BaseRepository<User> userRepository;
        private readonly BaseRepository<Address> addressRepository;

        public UserController()
        {
            var dbC = new ApplicationDbContext();
            this.userRepository = new BaseRepository<User>(dbC);
            this.addressRepository = new BaseRepository<Address>(dbC);
        }

        public ActionResult Index()
        {
            var users = this.userRepository.GetAll();
            var viewModel = Mapper.MapToUserViewModel(users);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            this.LoadViewData();
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.LoadViewData();
                return View(model);
            }

            Session["loggedUser"] = model.Name;
            var user = Mapper.MapToUser(model);
            this.userRepository.Add(user);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            this.LoadViewData();
            var user = this.userRepository.GetById(id);
            var mappedUser = Mapper.MapToUserViewModel(user);
            return View(mappedUser);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.LoadViewData();
                return View(model);
            }

            if (!this.userRepository.Any(x => x.Id == model.Id))
            {
                this.LoadViewData();
                this.ModelState.AddModelError(string.Empty, "The user does not exist!");
                return View(model);
            }

            var user = Mapper.MapToUser(model);
            this.userRepository.Update(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            this.userRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private void LoadViewData()
        {
            var addresses = this.addressRepository.GetAll();
            this.ViewBag.Addresses = Mapper.MapToAddressViewModel(addresses);
        }
    }
}
