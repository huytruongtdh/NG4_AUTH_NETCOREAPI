using System.Collections;
using System.Collections.Generic;

namespace NgAuth.Data
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        void Update(ApplicationUser model, string newPassword = null);
        ApplicationUser GetById(string id);
        void Add(ApplicationUser user);
        ApplicationUser GetByUserName(string userName);
    }
}