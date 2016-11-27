using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using RaceInfrastructure.DomainObjects;
using RaceInfrastructure;

namespace RaceWebsite.ClassesForRaceWebsite.Identity
{
    public class UserStore : IUserStore<IdentityUser, Guid>, IDisposable
    {
        private readonly IRaceUnitOfWork CurrentRaceUnitOfWork;

        public UserStore(IRaceUnitOfWork NewRaceUnitOfWork)
        {
            CurrentRaceUnitOfWork = NewRaceUnitOfWork;
        }

        public Task CreateAsync(IdentityUser NewIdentityUser)
        {
            if (NewIdentityUser == null)
                throw new ArgumentNullException("NewIdentityUser");

            User NewUser = GetUser(NewIdentityUser);

            CurrentRaceUnitOfWork.UserRepository.Add(NewUser);
            return CurrentRaceUnitOfWork.SaveAllChangesAsync();
        }

        public Task DeleteAsync(IdentityUser IdentityUserToBeDeleted)
        {
            if (IdentityUserToBeDeleted == null)
                throw new ArgumentNullException("user");

            User NewUser = GetUser(IdentityUserToBeDeleted);

            CurrentRaceUnitOfWork.UserRepository.Delete(NewUser);
            return CurrentRaceUnitOfWork.SaveAllChangesAsync();
        }

        public void Dispose()
        {

        }

        public Task<IdentityUser> FindByIdAsync(Guid UserId)
        {
            User FoundUser = CurrentRaceUnitOfWork.UserRepository.GetAll().Where(ResUser => ResUser.UserID == UserId).FirstOrDefault();
            return Task.FromResult<IdentityUser>(GetIdentityUser(FoundUser));
        }

        public Task<IdentityUser> FindByNameAsync(string UserName)
        {
            User FoundUser = CurrentRaceUnitOfWork.UserRepository.GetAll().FirstOrDefault(ResUser => ResUser.Name == UserName);
            return Task.FromResult<IdentityUser>(GetIdentityUser(FoundUser));
        }

        public Task UpdateAsync(IdentityUser ModifiedIdentityUser)
        {
            if (ModifiedIdentityUser == null)
                throw new ArgumentException("ModifiedIdentityUser");

            User FoundUser = CurrentRaceUnitOfWork.UserRepository.GetAll().FirstOrDefault(ResUser => ResUser.UserID == ModifiedIdentityUser.Id);
            PopulateUser(FoundUser, ModifiedIdentityUser);

            CurrentRaceUnitOfWork.UserRepository.Update(FoundUser);
            return CurrentRaceUnitOfWork.SaveAllChangesAsync();
        }

        private void PopulateIdentityUser(IdentityUser CurrentIdentityUser, User CurrentUser)
        {
            CurrentIdentityUser.Id = CurrentUser.UserID;
            CurrentIdentityUser.UserName = CurrentUser.Name;
            CurrentIdentityUser.PasswordHash = CurrentUser.PasswordHash;
        }

        private void PopulateUser(User CurrentUser, IdentityUser CurrentIdentityUser)
        {
            CurrentUser.UserID = CurrentIdentityUser.Id;
            CurrentUser.PasswordHash = CurrentIdentityUser.PasswordHash;
            CurrentUser.Name = CurrentIdentityUser.UserName;
        }

        private User GetUser(IdentityUser NewIdentityUser)
        {
            if (NewIdentityUser == null)
                return null;

            User NewUser = new User();
            PopulateUser(NewUser, NewIdentityUser);

            return NewUser;
        }

        private IdentityUser GetIdentityUser(User CurrentUser)
        {
            if (CurrentUser == null)
                return null;

            IdentityUser NewIdentityUser = new IdentityUser();
            PopulateIdentityUser(NewIdentityUser, CurrentUser);

            return NewIdentityUser;
        }
    }
}