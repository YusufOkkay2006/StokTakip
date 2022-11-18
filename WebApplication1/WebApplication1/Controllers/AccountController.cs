using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        stoktakipEntities1 db = new stoktakipEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        
        [HttpPost]
        public ActionResult Login(Kullanıcı P)

        {
            var bilgiler = db.Kullanıcı.FirstOrDefault(x => x.Email==P.Email && x.Şifre==P.Şifre);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Email , false);
                Session["Mail"]=bilgiler.Email.ToString();
                Session["Ad"]=bilgiler.Ad.ToString();
                Session["Soyad"]=bilgiler.Soyad.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.hata="Kullanıcı Adı veya şifre yanlış";
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Kullanıcı data)
        {
            db.Kullanıcı.Add(data);
            data.Rol ="U";
            db.SaveChanges();
           return RedirectToAction("Login", "Account");
            
        }
    }
}
