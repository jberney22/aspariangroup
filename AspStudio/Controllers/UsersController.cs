using EagleApp.Areas.Identity.Pages.Account;
using EagleApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }
        [HttpGet]
        public IActionResult GetUserById(string id)
        {
            var users = _userManager.Users.FirstOrDefault(o => o.Id == id);
            return new JsonResult(users);
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            ViewBag.Message = "";
            var users = _userManager.Users.FirstOrDefault(o => o.Id == id);
            var rolename = await _userManager.GetRolesAsync(users);
            var editUserModel = new InputEditModel()
            {
                Email = users.Email,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Role = rolename.FirstOrDefault()
            };
            return View(editUserModel);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InputEditModel inputEditModel)
        {
            try
            {
                var appUser = new ApplicationUser { UserName = inputEditModel.Email, Email = inputEditModel.Email, FirstName = inputEditModel.FirstName, LastName = inputEditModel.LastName };
                var userObj = await _userManager.FindByEmailAsync(inputEditModel.Email);
                userObj.FirstName = inputEditModel.FirstName;
                userObj.LastName = inputEditModel.LastName;

                var identityResult = await _userManager.UpdateAsync(userObj);
                var roleExists = await _roleManager.RoleExistsAsync(inputEditModel.Role);
                if (!roleExists)
                {
                    var idResult = await _roleManager.CreateAsync(new IdentityRole(inputEditModel.Role));
                }

                var roles = await _userManager.GetRolesAsync(userObj);

               // var oldrole = _roleManager.FindByNameAsync(roles.FirstOrDefault());
                await _userManager.RemoveFromRolesAsync(userObj,roles);

                var roleresult = await _userManager.AddToRoleAsync(userObj, inputEditModel.Role);


                ViewBag.Message = identityResult.Succeeded ? "Success" : "Error";
                return View(inputEditModel);
                // return Redirect($"/Users/Edit?id={userObj.Id}");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var userObj = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(userObj);
            return RedirectToAction(nameof(ListUsers));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
               

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
