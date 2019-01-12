using System.Collections.Generic;

namespace GenApp.Repository
{
    internal interface IUserRepository
    {
        IEnumerable<string> GetRolesForUser(string userId);
    }
}
