using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShoppinglistApp.Data;
using ShoppinglistApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppinglistApp.Controllers
{

	public class AccountController : Controller
	{
		private readonly ShoppingListContext _context;
		public AccountController(ShoppingListContext context)
		{
			this._context = context;
		}

		[HttpGet]
		public IActionResult Register(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(string firstName, string lastName, string password, string returnUrl = null)
		{

			var user = new User();
			user.FirstName = firstName;
			user.LastName = lastName;
			user.RegisterDate = new DateTime();
			//user.Password = password;


			if (ModelState.IsValid)
			{
				try
				{
					_context.Users.Add(user);
					await _context.SaveChangesAsync();
				}
				catch (DbException)
				{
					throw;
				}
				//return RedirectToAction(nameof(Index));
			}
			else
			{
				Debug.WriteLine("not valid");
			}
			var userList = new List<User>();
			userList = _context.Users.ToList();
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}
		private bool ValidateLogin(string userName, string password)
		{
			// For this sample, all logins are successful.
			return true;
		}

		[HttpPost]
		public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;

			// Normally Identity handles sign in, but you can do it directly
			//if (ValidateLogin(userName, password))
			//{
			//  var claims = new List<Claim>
			//      {
			//          new Claim("user", userName),
			//          new Claim("role", "Member")
			//      };

			//  await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

			//  if (Url.IsLocalUrl(returnUrl))
			//  {
			//    return Redirect(returnUrl);
			//  }
			//  else
			//  {
			//    return Redirect("/");
			//  }
			//}

			return View();
		}


		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}

}
