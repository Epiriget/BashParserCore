using BashParserCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BashParserCore.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private IHttpContextAccessor httpContextAccessor;
        private UserManager<ApplicationUser> userManager;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        
        public ApplicationUser getCurrentUser()
        {
            ApplicationUser currUser = null;
            if(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null) {
                currUser = userManager.FindByIdAsync(httpContextAccessor.HttpContext
                .User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            }
            return currUser;
        }
    }
}
