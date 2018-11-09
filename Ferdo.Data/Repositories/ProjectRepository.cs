using Ferdo.Data.Entities;
using System.Data.Entity;
using System.Linq;

namespace Ferdo.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>
    {
        public ProjectRepository()
            : base(new ApplicationDbContext())
        {

        }

        public override Project Add(Project model)
        {
            foreach (var user in model.Users)
            {
                this.applicationDbContext.Users.Attach(user);
            }

            return base.Add(model);
        }

        public override Project Update(Project model)
        {
            var userIds = model.Users.Select(x => x.Id);
            var entity = this.dbSet.First(x => x.Id == model.Id);

            var deletedUsers = entity.Users.Where(x => !userIds.Contains(x.Id)).Select(x => x.Id);
            var addedUsers = model.Users.Where(x => !entity.Users.Any(y => y.Id == x.Id));

            entity.Users = entity.Users.Where(x => !deletedUsers.Contains(x.Id)).ToList();
            foreach (var user in addedUsers)
            {
                this.applicationDbContext.Users.Attach(user);
                entity.Users.Add(user);
            }


            this.applicationDbContext.Entry(entity).CurrentValues.SetValues(model);

            this.applicationDbContext.SaveChanges();
            return entity;
        }
    }
}
