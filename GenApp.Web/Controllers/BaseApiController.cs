using System.Net.Http;
using System.Web.Http;
using GenApp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GenApp.Web.Controllers
{
    //added by pawan
    public class BaseApiController : ApiController
    {
        private readonly ApplicationRoleManager _applicationRoleManager=null;
        private readonly ApplicationUserManager _applicationUserManager = null;
        private readonly ModelFactory _modelFactory = null;

        protected ModelFactory ModelFactory =>
            _modelFactory ?? new ModelFactory(this.Request, this.AppUserManager);

        protected ApplicationRoleManager AppRoleManager => 
            _applicationRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        protected ApplicationUserManager AppUserManager =>
            _applicationUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }
            return null;
        }


       
    }
}
