using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GenApp.Repository
{
    internal interface IUserRepository
    {
        IEnumerable<string> GetRolesForUser(string userId);
    }
}
