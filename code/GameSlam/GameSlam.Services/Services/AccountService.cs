﻿
using GameSlam.Core.Models;
using System;
using System.Threading.Tasks;

namespace GameSlam.Services.Services
{
    public class AccountService
    {
        public const String AdminRoleStr = "Admin";
        public const String AuthorizedUserStr = "AuthorizedUser";
        public const string CompanyDomainName = "@gameslam.com";
        private readonly ApplicationSignInManager signInManager;
        private readonly ApplicationUserManager userManager;
                  
        public AccountService(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            return await userManager.FindByIdAsync(await signInManager.GetVerifiedUserIdAsync());
        }

        public String GetRegisteredUserRole(string email)
        {
            if (email.ToLower().EndsWith(CompanyDomainName))
                return AdminRoleStr;
            return AuthorizedUserStr;
        }
    }
}
