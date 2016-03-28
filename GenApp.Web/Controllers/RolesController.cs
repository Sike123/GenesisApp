using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GenApp.Web.Controllers
{
    public class RolesController :BaseApiController
    {
        
        public async Task<IHttpActionResult> GetRole(string Id)
        {
            
            //var role = await this.ApplicationRoleManager.FindByIdAsync(Id);
            //if (role != null)
            //{
            //    return Ok(TheModelFactory.Create(role));
            //}
            return NotFound();
        }
        

      
    }
}
