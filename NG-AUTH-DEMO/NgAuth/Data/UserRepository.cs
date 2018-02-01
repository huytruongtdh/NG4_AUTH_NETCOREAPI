using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace NgAuth.Data
{
    public static class TaskHelper
    {
        public static void RunTaskSynchronously(this Task t)
        {
            var task = Task.Run(async () => await t);
            task.Wait();
        }

        public static T RunTaskSynchronously<T>(this Task<T> t)
        {
            T res = default(T);
            var task = Task.Run(async () => res = await t);
            task.Wait();
            return res;
        }
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void Add(ApplicationUser user)
        {
            var task = Task.Run(async () => await _userManager.CreateAsync(user, user.Password));
            var result = task.Result;

            if (!result.Succeeded)
                throw new Exception(string.Join(";", result.Errors.Select(s=> s.Code + ": " + s.Description)));
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users.AsEnumerable();
        }

        public ApplicationUser GetById(string id)
        {
            return Task.Run(async() => await _userManager.FindByIdAsync(id))
                .Result;
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return _userManager.FindByNameAsync(userName)
                .RunTaskSynchronously();
        }

        public void Update(ApplicationUser user, string newPassword = null)
        {
            using(var scope= new TransactionScope())
            {
                var result = Task.Run(async () => await _userManager.UpdateAsync(user)).Result;
                if (!result.Succeeded)
                    throw new Exception(ShowErrorMessages(result.Errors));

                if (!string.IsNullOrEmpty(newPassword))
                {
                    result = Task.Run(async () => await _userManager.RemovePasswordAsync(user)).Result;
                    if (!result.Succeeded)
                        throw new Exception(ShowErrorMessages(result.Errors));

                    result = Task.Run(async () => await _userManager.AddPasswordAsync(user, newPassword)).Result;
                    if (!result.Succeeded)
                        throw new Exception(ShowErrorMessages(result.Errors));
                }
            }

            //using(var scope = new TransactionScope())
            //{
            //    // update user info
            //    var r = _userManager.UpdateAsync(user).RunTaskSynchronously();
            //    if (!r.Succeeded)
            //        throw new Exception("UpdateAsync unsuccessful");

            //    // remove old password for next step
            //    r = _userManager.RemovePasswordAsync(user).RunTaskSynchronously();
            //    if (!r.Succeeded)
            //        throw new Exception("UpdateAsync unsuccessful");

            //    // add new password
            //    r = _userManager.AddPasswordAsync(user, user.Password).RunTaskSynchronously();
            //    if (!r.Succeeded)
            //        throw new Exception("UpdateAsync unsuccessful");
            //}
        }

        private string ShowErrorMessages(IEnumerable<IdentityError> errors)
        {
            return string.Join("\n", errors.Select(s=> s.Code + ": " + s.Description));
        }
    }
}
