using MvcInternetApplication.Filters;
using SportStatistics.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SportStatistics.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            try
            {
                if (Request.UrlReferrer != null)
                {
                    ViewBag.ReturnUrl = Request.UrlReferrer.AbsoluteUri;
                }                

                var regex = new Regex("login");
                string url = ViewBag.ReturnUrl.ToLower();
                if (regex.IsMatch(url))
                {
                    ViewBag.ReturnUrl = null;
                }

                regex = new Regex("register");
                url = ViewBag.ReturnUrl.ToLower();
                if (regex.IsMatch(url))
                {
                    ViewBag.ReturnUrl = null;
                }
            }
            catch (Exception)
            {
                ViewBag.ReturnUrl = null;
            }

            return View();            
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Login))
                {
                    ModelState.AddModelError("Login", "Enter Login");
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Enter Password");
                }

                if (ModelState.IsValid)
                {
                    using (UsersContext db = new UsersContext())
                    {
                        int userId = WebSecurity.GetUserId(model.Login);
                        UserProfile user = db.UserProfiles.Find(userId);

                        if (user == null)
                        {
                            ModelState.AddModelError("Login", "This username is not found!");
                            return View();
                        }


                        if (!WebSecurity.Login(model.Login, model.Password, persistCookie: model.RememberMe))
                        {
                            ModelState.AddModelError("", "Username or password do not match!");
                            return View();
                        }

                        if (Roles.GetRolesForUser(model.Login).Count() == 0)
                        {
                            Roles.AddUsersToRole(new[] { model.Login }, "User");
                        }

                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    WebSecurity.Logout();
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationModel model)
        {
            try
            {              
                if (string.IsNullOrEmpty(model.Login))
                {
                    ModelState.AddModelError("Login", "Enter username");
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Enter password");
                }
                if (string.Compare(model.Password, model.ConfirmPassword) != 0)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords did not match");
                }
                if (model.Email != null && !(new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(model.Email)))  //!regex.IsMatch(model.Email))
                {
                    ModelState.AddModelError("Email", "Email was entered incorrectly");
                }
                if (model.Login == model.Password)
                {
                    ModelState.AddModelError("", "The password must not match your username");
                }

                UsersContext dbUsers = new UsersContext();

                var check = from c in dbUsers.UserProfiles
                            where c.Email == model.Email
                            select c;
                if (check.Count() != 0)
                {
                    ModelState.AddModelError("Email", "This Email already exists");
                }

                check = from c in dbUsers.UserProfiles
                        where c.UserName == model.Login
                        select c;

                if (check.Count() != 0)
                {
                    ModelState.AddModelError("Login", "This username has been taken.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {                        
                        MailAddress from = new MailAddress("projecttestmailqso@gmail.com", "Sport Statistics");
                        MailAddress to = new MailAddress(model.Email);
                        MailMessage message = new MailMessage(from, to);
                        message.Subject = "Email confirmation";
                        message.Body = string.Format("You have successfully registered on the sports statistics website!");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                        smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential("projecttestmailqso@gmail.com", "1a2b3c4d_");
                        smtp.Send(message);
                       
                        WebSecurity.CreateUserAndAccount(model.Login, model.Password);
                        Roles.AddUserToRole(model.Login, "User");
                                                
                        int id = WebSecurity.GetUserId(model.Login);

                        UserProfile user = dbUsers.UserProfiles.Find(id);
                        user.Email = model.Email;                   
                        dbUsers.Entry(user).State = EntityState.Modified;
                        dbUsers.SaveChanges();
                        WebSecurity.Login(user.UserName, model.Password);
                        
                        ViewBag.Message = "You have successfully registered on the sports statistics website!";
                        return View("UserAccount");
                    }
                    catch (MembershipCreateUserException e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult UserAccount()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login");
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login");
                }

                ViewBag.ChangePassword = "";

                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.OldPassword))
                {
                    if (!WebSecurity.Login(User.Identity.Name, model.OldPassword))
                    {
                        ModelState.AddModelError("OldPassword", "You entered an incorrect current password");
                    }
                }
                if (User.Identity.Name == model.NewPassword)
                {
                    ModelState.AddModelError("", "The password must not match your username");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        ViewBag.ChangePassword = "Password was changed";
                        return View();
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "There was an error changing the password, please repeat again");
                    }
                }
                ViewBag.ChangePassword = "";
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult ChangeName(bool? change)
        {
            try
            {
                string s = User.Identity.Name;

                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login");
                }
                if (change != null)
                {
                    ViewBag.Change = true;
                    ViewBag.NewName = User.Identity.Name;
                    return View();
                }
                ViewBag.Change = false;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeName(ChangeNameModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.NewName))
                {
                    UsersContext db = new UsersContext();
                    var check = from c in db.UserProfiles
                                where c.UserName == model.NewName
                                select c;

                    if (check.Count() != 0)
                    {
                        ModelState.AddModelError("NewName", "This username has been taken.");
                    }
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Enter password");
                }
                else
                {
                    if (!WebSecurity.Login(User.Identity.Name, model.Password))
                    {
                        ModelState.AddModelError("Password", "You entered an incorrect current password!");
                    }
                }

                if (ModelState.IsValid)
                {                    
                    using (UsersContext dbUser = new UsersContext())
                    {
                        int id = WebSecurity.GetUserId(User.Identity.Name);                     

                        UserProfile user = dbUser.UserProfiles.Find(id);
                        user.UserName = model.NewName;

                        dbUser.Entry(user).State = EntityState.Modified;
                        dbUser.SaveChanges();                

                        WebSecurity.Logout();
                        WebSecurity.Login(model.NewName, model.Password);
                    }
                    return RedirectToAction("ChangeName", "Account", new { change = true });
                }
                ViewBag.Change = false;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {          
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                ViewBag.Message = "";
                if (string.IsNullOrEmpty(model.UserName))
                {
                    ModelState.AddModelError("UserName", "Enter username");
                }             

                if (model.Email != null && !(new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(model.Email)))  //!regex.IsMatch(model.Email))
                {
                    ModelState.AddModelError("Email", "Email was entered incorrectly");
                }

                UsersContext dbUsers = new UsersContext();

                var check = from c in dbUsers.UserProfiles
                            where c.Email == model.Email
                            select c;
                if (check.Count() == 0)
                {
                    ModelState.AddModelError("Email", "This email was not found!");
                }

                check = from c in dbUsers.UserProfiles
                        where c.UserName == model.UserName
                        select c;

                if (check.Count() == 0)
                {
                    ModelState.AddModelError("Login", "This username was not found!");
                }

                if (ModelState.IsValid)
                {
                    string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-*";
                    char[] chars = new char[10];
                    Random rd = new Random();
                    for (int i = 0; i < 10; i++)
                    {
                        chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                    }
                    string newPassword = new string(chars);

                    string code = WebSecurity.GeneratePasswordResetToken(model.UserName);
                    WebSecurity.ResetPassword(code, newPassword);                                       
                    
                    MailAddress from = new MailAddress("projecttestmailqso@gmail.com", "Sport Statistics");
                    MailAddress to = new MailAddress(model.Email);
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = "Password recovery";
                    message.Body = string.Format("Your username: " + model.UserName + ", your password: " + newPassword);
                    message.IsBodyHtml = true;

                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);    
                    smtp.EnableSsl = true;
                                        
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("projecttestmailqso@gmail.com", "1a2b3c4d_");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credentials;
                    smtp.Send(message);
                    
                    ViewBag.Message = "Your password has been sent to your email" + " " + newPassword;
                    return View();
                }
                                
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Error");
            }
        }
    }
}