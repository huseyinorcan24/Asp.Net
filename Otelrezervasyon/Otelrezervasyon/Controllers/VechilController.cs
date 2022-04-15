using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Otelrezervasyon.Models;

namespace Otelrezervasyon.Controllers
{
    public class VechilController : Controller
    {
        Db_RezervasyonEntities db = new Db_RezervasyonEntities();
        // GET: Vechil
        public ActionResult Index()
        {
            return View(db.Tbl_Reservasyon.ToList());
        }
        public ActionResult Details(int? id)
        {
            Tbl_Reservasyon oda_bilgileri = db.Tbl_Reservasyon.Find(id);
            return View(oda_bilgileri);
        }

        [HttpGet]

        public ActionResult Rezervation(int? id)
        {
            Tbl_Reservasyon oda_bilgileri = db.Tbl_Reservasyon.Find(id);
            ViewData["Oda Türü"] = oda_bilgileri.Odatürü;
            ViewData["Kat Numarası"] = oda_bilgileri.Katno;
            ViewData["Oda Fiyatı"] = oda_bilgileri.Fiyat;
            return View();
        }

        [HttpPost]
        public ActionResult Rezervation([Bind (Include = "RezervationId,ReservasyonId,TcKimlik,AdSoyad,Almatarihi,TeslimTarihi")] Tbl_Rezervation rezervasyon)
        {
           if(ModelState.IsValid)
            {
                db.Tbl_Rezervation.Add(rezervasyon);
                db.SaveChanges();
            }
            ViewBag.Message = "Tebrikler, Rezervasyon İşleminiz Başarı İle Gerçekleştirilmiştir.";
            return View();
        }
    }
}