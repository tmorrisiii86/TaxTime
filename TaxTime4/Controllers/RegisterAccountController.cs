﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaxTime4.Controllers
{
    public class RegisterAccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public RegisterAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateUser(string username, string password)
        {
            IdentityUser newUser = new IdentityUser();
            newUser.UserName = username;
            newUser.Email = username;

            IdentityResult result = _userManager.CreateAsync(newUser, password).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(newUser, "Employee").Wait();
                _signInManager.SignInAsync(newUser, false).Wait();
            }
            return RedirectToAction("Index", "Customers");
        }
    }
}