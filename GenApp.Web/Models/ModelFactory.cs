﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GenApp.Web.Models
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;
        private readonly ApplicationUserManager _appUserManager;


        public ModelFactory(HttpRequestMessage requestMessage,ApplicationUserManager appUserManager)
        {
            _urlHelper= new UrlHelper(requestMessage);
            _appUserManager = appUserManager;
        }

        public UserReturnModel Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new {id = appUser.Id}),
                Id = appUser.Id,
                UserName = appUser.UserName,
            //    FullName = $"{appUser.FirstName}",//,{appUser.LastName}",
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                //Level = appUser.Level,
                //JoinDate = appUser.JoinDate,
                Roles = _appUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _appUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }


        public RoleReturnModel Create(IdentityRole role)
        {
            return new RoleReturnModel
            {
                Url = _urlHelper.Link("GetRoleById", new {id = role.Id}),
                Id = role.Id,
                Name = role.Name
            };
        }
        public class UserReturnModel
        {

            public string Url { get; set; }
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool EmailConfirmed { get; set; }
            public int Level { get; set; }
            public DateTime JoinDate { get; set; }
            public IList<string> Roles { get; set; }
            public IList<System.Security.Claims.Claim> Claims { get; set; }

        }

        public class RoleReturnModel
        {
            public string Url { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
        }


    }

}
