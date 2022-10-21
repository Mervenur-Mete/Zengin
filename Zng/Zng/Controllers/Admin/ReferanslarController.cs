using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zng.Helper;
using Zng.Models;

namespace Zng.Controllers.Admin
{
    public class ReferanslarController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Referanslar
        public ActionResult Index()
        {
            return View(db.Referanslars.ToList());
        }

        // GET: Referanslar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslars.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // GET: Referanslar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Referanslar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Referanslar referanslar, HttpPostedFileBase ImageName)
        {
            if (ImageName != null)
            {
                int imgWidth = 600;
                int imgHeight = 400;

                var resizedImage = ImageService.ResizeImage(ImageName.InputStream, imgWidth, imgHeight);

                string extension = Path.GetExtension(ImageName.FileName);
                string fileName = "zng_" + Guid.NewGuid().ToString() + extension;

                string outputFileName = Server.MapPath("~/Images/" + fileName);
                ImageService.SaveImage(resizedImage, outputFileName);

                referanslar.ImageName = fileName;

            }
            else
            {
                referanslar.ImageName = "urun.png";
            }
            if (ModelState.IsValid)
            {
                db.Referanslars.Add(referanslar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(referanslar);
        }
        // GET: Referanslar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslars.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // POST: Referanslar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Referanslar referanslar, HttpPostedFileBase ImageName)
        {
            var newReferans = db.Referanslars.Find(referanslar.Id);

            if (ImageName != null)
            {
                int imgWidth = 600;
                int imgHeight = 400;

                var resizedImage = ImageService.ResizeImage(ImageName.InputStream, imgWidth, imgHeight);

                string extension = Path.GetExtension(ImageName.FileName);
                string fileName = "piransoft_" + Guid.NewGuid().ToString() + extension;

                string outputFileName = Server.MapPath("~/Images/" + fileName);
                ImageService.SaveImage(resizedImage, outputFileName);

                if (newReferans.ImageName != "urun.png")
                {
                    string eski = Server.MapPath("~/Images/" + newReferans.ImageName);
                    FileInfo file = new FileInfo(eski);
                    if (file.Exists)//check file exsit or not
                    {
                        file.Delete();
                    }
                }
                newReferans.ImageName = fileName;
            }
            else
            {
                newReferans.ImageName = newReferans.ImageName;
            }
            newReferans.Baslik = referanslar.Baslik;
            newReferans.Icerik = referanslar.Icerik;
            newReferans.Tarih = referanslar.Tarih;
            newReferans.Description = referanslar.Description;
            newReferans.Keyword = referanslar.Keyword;

            if (ModelState.IsValid)
            {
                db.Entry(newReferans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(referanslar);
        }

        // GET: Referanslar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslars.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // POST: Referanslar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var referanslar = db.Referanslars.Find(id);
            db.Referanslars.Remove(referanslar);
            db.SaveChanges();
            //resim siliniyor
            if (referanslar.ImageName != "urun.png")
            {
                string eski = Server.MapPath("~/Images/" + referanslar.ImageName);
                FileInfo file = new FileInfo(eski);
                if (file.Exists)//check file exsit or not
                {
                    file.Delete();
                }
            }
            return RedirectToAction("Index");
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
