using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using BashParserCore.Models;
using BashParserCore.Models.AccountViewModels;
using BashParserCore.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BashParserCore.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        public IdentityService(
     UserManager<ApplicationUser> userManager,
     SignInManager<ApplicationUser> signInManager,
     RoleManager<IdentityRole> myRoleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = myRoleManager;

        }
        async public void createModerator()
        {
            createRole(_roleManager, "User");
            createRole(_roleManager, "Moderator");
            createRole(_roleManager, "Admin");

            var moder = new ApplicationUser { UserName = "moderator@gmail.com", Email = "moderator@gmail.com" };
            var resultModer =await _userManager.CreateAsync(moder, "Moderator1-password");

            if (resultModer.Succeeded)
            {
                List<string> roles = new List<string>() { "User"/*, "Moderator" */};
                _userManager.AddToRolesAsync(moder,roles).Wait();
                await _signInManager.SignInAsync(moder, isPersistent: false);
            }

                
            var admin = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
            //     _userManager.CreateAsync(admin, "Moderator1-password").Wait();
            var resultAdmin = await _userManager.CreateAsync(admin, "Admin1-password");

            if (resultAdmin.Succeeded)
            {
                List<string> roles = new List<string>() { "User", "Moderator", "Admin" };
                _userManager.AddToRolesAsync(admin, roles).Wait();
                await _signInManager.SignInAsync(admin, isPersistent: false);
            }
        }

        async public void createRole(RoleManager<IdentityRole> myRoleManager, string role)
        {
            if (!_roleManager.RoleExistsAsync(role).Result)
            {
                IdentityRole moderIdentityRole = new IdentityRole();
                moderIdentityRole.Name = role;
                await _roleManager.CreateAsync(moderIdentityRole);
            }
        }
    }

}
