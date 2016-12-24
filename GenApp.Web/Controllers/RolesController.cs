using System;
using System.Threading.Tasks;
using System.Web.Http;
using GenApp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GenApp.Web.Controllers
{
    // [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {
        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);
            if (role != null)
            {
                return Ok(ModelFactory.Create(role));
            }
            return NotFound();
        }

        [Route("", Name = "GetAllRoles")]
        [HttpGet]
        public IHttpActionResult GetAllRoles()
        {
            var roles = AppRoleManager.Roles;
            return Ok(roles);
        }

        [System.Web.Http.Route("Create")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Create( RoleBindingModel.CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole { Name = model.Name };
            
            var result =  await AppRoleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
           
            var locationHeader = new Uri(Url.Link("GetRoleById", new { id = role.Id }));
            return Created(locationHeader, ModelFactory.Create(role));

        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await AppRoleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    GetErrorResult(result);
                }
                return Ok();
            }
            return NotFound();
        }

        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUserInRoles(RoleBindingModel.UsersInRoleModel model)
        {
            var role = await AppRoleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ModelState.AddModelError("", "Role does not exists");
                return BadRequest(ModelState);
            }
            foreach (var enRolledUser in model.EnrolledUsers)
            {
                var user = AppUserManager.FindByIdAsync(enRolledUser);
                if (user == null)
                {
                    ModelState.AddModelError("", @"User {user} does not exist");
                    continue;
                }

                if (!AppUserManager.IsInRole(enRolledUser, role.Name))
                {
                    IdentityResult result = await this.AppUserManager.AddToRoleAsync(enRolledUser, role.Name);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("",
                            $"User {enRolledUser} could not be added to the role");
                    }
                }
            }

            foreach (var removedUser in model.RemovedUsers)
            {
                var user = AppUserManager.FindByIdAsync(removedUser);
                if (removedUser == null)
                {
                    ModelState.AddModelError("", @"User {removedUser} does not exists");
                    continue;
                }
                IdentityResult result = await this.AppUserManager.RemoveFromRoleAsync(removedUser, role.Name);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", @"User {removedUser} could not be removed from the Role");
                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
