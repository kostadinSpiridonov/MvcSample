using Ferdo.Data;
using Ferdo.Data.Entities;
using Ferdo.Data.Repositories;
using Ferdo.Mappings;
using Ferdo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ferdo.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectRepository projectRepository;
        private readonly BaseRepository<User> userRepository;

        public ProjectController()
        {
            var dbC = new ApplicationDbContext();
            this.userRepository = new BaseRepository<User>(dbC);
            this.projectRepository = new ProjectRepository();
        }

        public ActionResult Index()
        {
            var projects = this.projectRepository.GetAll();
            return View(Mapper.MapToProjectViewModel(projects));
        }

        public ActionResult Create()
        {
            this.LoadViewData();
            string name = Session["loggedUser"].ToString();
            return View(new ProjectViewModel
            {
                UsersIds = this.userRepository.GetAll().Where(x => x.Name == name).Select(x => x.Id)
            });
        }

        [HttpPost]
        public ActionResult Create(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.LoadViewData();
                return View(model);
            }

            var project = Mapper.MapToProject(model);
            this.projectRepository.Add(project);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            this.LoadViewData();
            var project = this.projectRepository.GetById(id);
            var mappedProject = Mapper.MapToProjectViewModel(project);
            return View(mappedProject);
        }

        [HttpPost]
        public ActionResult Edit(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.LoadViewData();
                return View(model);
            }

            if (!this.projectRepository.Any(x => x.Id == model.Id))
            {
                this.LoadViewData();
                this.ModelState.AddModelError(string.Empty, "The project does not exist!");
                return View(model);
            }

            var project = Mapper.MapToProject(model);
            this.projectRepository.Update(project);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            this.projectRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private void LoadViewData()
        {
            var users = this.userRepository.GetAll();
            this.ViewBag.Users = Mapper.MapToUserViewModel(users);
        }
    }
}
