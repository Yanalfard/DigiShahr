﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Services;
using DataLayer.Models;

namespace DigiShahr.Utilit
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private Core db = new Core();
        private string _permissionId = "";
        public PermissionCheckerAttribute(string permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;
                TblUser selectUser = db.User.Get().SingleOrDefault(i => i.TellNo == userName);
                List<string> check = _permissionId.Split(',').ToList();

                string name = selectUser.Role.Name.Trim();
                if (!check.Contains(name))
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}
