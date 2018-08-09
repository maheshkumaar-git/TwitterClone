using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.UI.Models;
using TwitterClone.BusinessLayer;
using TwitterClone.DataLayer;
using TwitterClone.DataLayer.Models;

namespace TwitterClone.UI.Controllers
{
    public class UserController : Controller
    {
        UserBL ubl = new UserBL();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string uname, string pwd)
        {
            Person person = ubl.ValidateUser(uname, pwd);
            if (person != null)
            {
                Session["UserName"] = person.FullName;
                Session["UserId"] = person.UserId;
                return RedirectToAction("Index", "Home", person);
            }
            else
            {
                TempData["err"] = "Invalid Login Details";
                return View();
            }
        }

        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PersonVM item)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person()
                {
                    UserId = item.UserId,
                    Password = item.Pwd,
                    FullName = item.Name,
                    Email = item.Email,
                    Active = true,
                    Joined = DateTime.Now
                };
                ubl.AddUser(person);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ViewResult Update()
        {
            Person person = ubl.SearchUser(Session["UserId"].ToString());
            PersonVM peopleVM = new PersonVM();
            peopleVM.UserId = person.UserId;
            peopleVM.Pwd = person.Password;
            peopleVM.Name = person.FullName;
            peopleVM.Email = person.Email;
            peopleVM.Active = person.Active;
            peopleVM.Joined = person.Joined;
            return View(peopleVM);
        }

        [HttpPost]
        public ActionResult Update(PersonVM item)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person()
                {
                    UserId = item.UserId,
                    Password = item.Pwd,
                    Email = item.Email,
                    FullName = item.Name,
                    Active = item.Active,
                    Joined = DateTime.Now
                };
                ubl.UpdateUser(person);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Deactivate()
        {
            Person person = ubl.SearchUser(Session["UserId"].ToString());
            if (person != null)
            {
                person.Active = false;
            };
            ubl.UpdateUser(person);
            var result = "success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Details(Person p)
        {
            return View(p);
        }
    }
}