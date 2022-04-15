using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Otelrezervasyon.Models;

namespace Otelrezervasyon.Controllers
{
    public class VechilYonetController : Controller
    {
        private Db_RezervasyonEntities db = new Db_RezervasyonEntities();

        // GET: VechilYonet
        public ActionResult Index()
        {
            return View(db.Tbl_Reservasyon.ToList());
        }

        // GET: VechilYonet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Reservasyon tbl_Reservasyon = db.Tbl_Reservasyon.Find(id);
            if (tbl_Reservasyon == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Reservasyon);
        }

        // GET: VechilYonet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VechilYonet/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RezervasyonId,Odatürü,Katno,Odano,Fiyat")] Tbl_Reservasyon tbl_Reservasyon)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Reservasyon.Add(tbl_Reservasyon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Reservasyon);
        }

        // GET: VechilYonet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Reservasyon tbl_Reservasyon = db.Tbl_Reservasyon.Find(id);
            if (tbl_Reservasyon == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Reservasyon);
        }

        // POST: VechilYonet/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RezervasyonId,Odatürü,Katno,Odano,Fiyat")] Tbl_Reservasyon tbl_Reservasyon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Reservasyon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Reservasyon);
        }

        // GET: VechilYonet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Reservasyon tbl_Reservasyon = db.Tbl_Reservasyon.Find(id);
            if (tbl_Reservasyon == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Reservasyon);
        }

        // POST: VechilYonet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Reservasyon tbl_Reservasyon = db.Tbl_Reservasyon.Find(id);
            db.Tbl_Reservasyon.Remove(tbl_Reservasyon);
            db.SaveChanges();
            // Oda Resim silme işlemi :
            string ImageFileName = id.ToString() + ".jpeg";
            string FolderPath = Path.Combine(Server.MapPath("~/Vechilımages"), ImageFileName);
            if(System.IO.File.Exists(FolderPath))
            {
                System.IO.File.Delete(FolderPath);
            } 
            // resim silindi

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SaveImages()
        {
            return View() ;
        }

        [HttpPost]
        public ActionResult SaveImages(string hiddenId, HttpPostedFileBase UploadedIamge)
        {
            if (UploadedIamge.ContentLength > 0)
            {
                string ImageFileName = hiddenId + ".jpeg";
                string FolderPath = Path.Combine(Server.MapPath("~/Vechilımages"), ImageFileName);
                UploadedIamge.SaveAs(FolderPath);
            }
            ViewBag.Message = hiddenId + ".jpeg isimli resim başarıyla yüklendi.";
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
