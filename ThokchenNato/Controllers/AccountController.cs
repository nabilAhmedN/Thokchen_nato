using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThokchenNato.DatabaseContext;
using ThokchenNato.Models.Data;
using ThokchenNato.Models.ViewModels;

namespace ThokchenNato.Controllers
{
    public class AccountController : Controller
    {
        ThokchenNatoDbContext db = new ThokchenNatoDbContext();
        User user = new User();


        // GET: Account
        public ActionResult Index()
        {
            return Redirect("~/account/login");
        }


        [HttpGet]
        public ActionResult Login()
        {
            // Confirm user is not logged in

            string username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
                return RedirectToAction("user-profile");

            // Return view
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginUserVM loginUserVM)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(loginUserVM);
            }

            // Check if the user is valid

            bool isValid = false;


            if (db.users.Any(x => x.Username.Equals(loginUserVM.Username) && x.Password.Equals(loginUserVM.Password)))
            {
                isValid = true;
            }


            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginUserVM);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(loginUserVM.Username, loginUserVM.RememberMe);
                return Redirect(FormsAuthentication.GetRedirectUrl(loginUserVM.Username, loginUserVM.RememberMe));
            }

        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/account/login");
        }


        [Authorize]
        public ActionResult UserNavPartial()
        {
            // Get username
            string username = User.Identity.Name;

            // Declare model
            UserNavPartialVm model;


            // Get the user
            User user = db.users.FirstOrDefault(x => x.Username == username);

            // Build the model
            model = new UserNavPartialVm()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };


            // Return partial view with model
            return PartialView(model);
        }


        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }


        [HttpPost]
        public ActionResult CreateAccount(UserVM userVM)
        {
            //model state checking
            if (!ModelState.IsValid)
            {
                return View("CreateAccount", userVM);
            }

            //password matching with confirm password
            if (!userVM.Password.Equals(userVM.ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View("CreateAccount", userVM);
            }

            //unique username checking
            if (db.users.Any(x => x.EmailAddress.Equals(userVM.EmailAddress)))
            {
                ModelState.AddModelError("", "Email " + userVM.EmailAddress + " is taken.");
                userVM.EmailAddress = "";
                return View("CreateAccount", userVM);
            }

            User user = new User()
            {
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                EmailAddress = userVM.EmailAddress,
                Username = userVM.Username,
                Password = userVM.Password

            };
            db.users.Add(user);
            db.SaveChanges();


            int id = user.Id;

            UserRoles userRoles = new UserRoles()
            {
                UserId = id,
                RoleId = 2
            };

            db.userRoles.Add(userRoles);
            db.SaveChanges();

            //TempData["SM"] = "You are now registered and can login.";
            return Redirect("Login");
        }


        // GET: /account/user-profile
        [HttpGet]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile()
        {
            // Get username
            string username = User.Identity.Name;

            // Declare model
            UserProfileVM model;


            // Get user
            User dto = db.users.FirstOrDefault(x => x.Username == username);

            // Build model
            model = new UserProfileVM(dto);


            // Return view with model
            return View("UserProfile", model);
        }



        // POST: /account/user-profile
        [HttpPost]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile(UserProfileVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("UserProfile", model);
            }

            // Check if passwords match if need be
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View("UserProfile", model);
                }
            }

            // Get username
            string username = User.Identity.Name;

            // Make sure username is unique
            if (db.users.Where(x => x.Id != model.Id).Any(x => x.Username == username))
            {
                ModelState.AddModelError("", "Username " + model.Username + " already exists.");
                model.Username = "";
                return View("UserProfile", model);
            }

            // Edit DTO
            User dto = db.users.Find(model.Id);

            dto.FirstName = model.FirstName;
            dto.LastName = model.LastName;
            dto.EmailAddress = model.EmailAddress;
            dto.Username = model.Username;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                dto.Password = model.Password;
            }

            // Save
            db.SaveChanges();


            // Set TempData message
            TempData["SM"] = "You have edited your profile!";

            // Redirect
            return Redirect("~/account/user-profile");
        }
    }
}