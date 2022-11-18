using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UrunController : Controller
    {

        // GET: Urun
        stoktakipEntities1 db = new stoktakipEntities1();
        [Authorize]
        public ActionResult Index()
        {
            var list = db.Urun.ToList();
            return View(list);

        }

        public ActionResult Ekle()
        {
            List<SelectListItem> deger1 = (from x in db.Kategori.ToList()
                                           select new SelectListItem
                                           { 
                                           Text=x.Ad,
                                        Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.ktgr = deger1;
            return View();
 
        }
         [HttpPost]
         public ActionResult Ekle(Urun Data , HttpPostedFile File)
        {
            string path = Path.Combine("~/Content/Image"+File.FileName);
            File.SaveAs(Server.MapPath(path));
            Data.Resim = File.FileName.ToString();
            db.Urun.Add(Data);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult Sil(int id)
        {
            var urun = db.Urun.Where(x => x.İd == id).FirstOrDefault();
            db.Urun.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}