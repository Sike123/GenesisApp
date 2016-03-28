using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenApp.Web.Models;
using Microsoft.AspNet.Identity.Owin;

namespace GenApp.Web.Controllers
{
    //added by pawan
    public class BaseApiController : ApiController
    {
        private readonly ApplicationRoleManager _applicationRoleManager=null;

        protected ApplicationRoleManager ApplicationRoleManager
        {
            get
            {
                return _applicationRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
                
            }
        }

    }
}
