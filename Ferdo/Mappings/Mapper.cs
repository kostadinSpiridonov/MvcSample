using Ferdo.Data.Entities;
using Ferdo.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Ferdo.Mappings
{
    public static class Mapper
    {
        public static AddressViewModel MapToAddressViewModel(Address address)
        {
            return new AddressViewModel
            {
                Country = address.Country,
                City = address.City,
                Id = address.Id
            };
        }

        public static IEnumerable<AddressViewModel> MapToAddressViewModel(IEnumerable<Address> addresses)
        {
            foreach (var address in addresses)
            {
                yield return MapToAddressViewModel(address);
            }
        }

        public static IEnumerable<ProjectViewModel> MapToProjectViewModel(IEnumerable<Project> projects)
        {
            foreach (var project in projects)
            {
                yield return MapToProjectViewModel(project);
            }
        }

        public static Project MapToProject(ProjectViewModel model)
        {
            return new Project
            {
                Name = model.Name,
                Id = model.Id,
                Users = model.UsersIds.Select(x => new User { Id = x }).ToList()
            };
        }

        public static ProjectViewModel MapToProjectViewModel(Project project)
        {
            return new ProjectViewModel
            {
                Name = project.Name,
                Id = project.Id,
                UsersIds = project.Users.Select(x => x.Id)
            };
        }

        public static UserViewModel MapToUserViewModel(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                AddressId = user.AddressId,
                Address = MapToAddressViewModel(user.Address)
            };
        }

        public static IEnumerable<UserViewModel> MapToUserViewModel(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                yield return MapToUserViewModel(user);
            }
        }

        public static User MapToUser(UserViewModel model)
        {
            return new User
            {
                AddressId = model.AddressId,
                Name = model.Name,
                Id = model.Id
            };
        }

        public static Address MapToAddress(AddressViewModel address)
        {
            return new Address
            {
                Country = address.Country,
                City = address.City,
                Id = address.Id
            };
        }
    }
}