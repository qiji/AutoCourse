﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoCourse.BLL;

namespace AutoCourse.Controllers
{
    public class LoginController : Controller
    {

        //
        // GET: /Login/
        public ActionResult Index()
        {            
            ViewData["username"] = User.GetUserName();
            ViewData["schoolid"] = User.GetSchoolID();
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            string username = Request.Form["tbusername"];

            BLLManageUser mu = new BLLManageUser();
            AutoCourse.Models.ManageUser m = mu.Find(u => u.UserName == username);
            UserAuthentication.Authentication(Request.RequestContext.HttpContext, username, m);            
            ViewData["username"] = User.GetUserName();
            ViewData["schoolid"] = User.GetSchoolID();
            return Redirect("/School/index");
        }

        public ActionResult LoginOut()
        {
            UserAuthentication.LoginOut(Request.RequestContext.HttpContext);            
            ViewData["username"] = UserAuthentication.GetUserName(User);
            ViewData["schoolid"] = UserAuthentication.GetSchoolID(User);
            return Redirect("index");
        }
    }
}