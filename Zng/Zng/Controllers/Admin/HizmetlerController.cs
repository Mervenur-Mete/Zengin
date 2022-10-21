using Sitecore.FakeDb;
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
    public class HizmetlerController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Hizmetler
        public ActionResult Index()
        {
            return View(db.Hizmetlers.ToList());
        }

        // GET: Hizmetler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetler hizmetler = db.Hizmetlers.Find(id);
            if (hizmetler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetler);
        }

        // GET: Hizmetler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hizmetler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hizmetler hizmetler, HttpPostedFileBase ImageName)
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

                hizmetler.ImageName = fileName;

            }
            else
            {
                hizmetler.ImageName = "urun.png";
            }
            if (ModelState.IsValid)
            {
                db.Hizmetlers.Add(hizmetler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hizmetler);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetler hizmetler = db.Hizmetlers.Find(id);
            if (hizmetler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetler);
        }

        // GET: Hizmetler/Edit/5

        // POST: Hizmetler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Hizmetler hizmetler, HttpPostedFileBase ImageName)
        {
            var newHizmet = db.Hizmetlers.Find(hizmetler.Id);

            if (ImageName != null)
            {
                int imgWidth = 600;
                int imgHeight = 400;

                var resizedImage = ImageService.ResizeImage(ImageName.InputStream, imgWidth, imgHeight);

                string extension = Path.GetExtension(ImageName.FileName);
                string fileName = "piransoft_" + Guid.NewGuid().ToString() + extension;

                string outputFileName = Server.MapPath("~/Images/" + fileName);
                ImageService.SaveImage(resizedImage, outputFileName);

                if (newHizmet.ImageName != "urun.png")
                {
                    string eski = Server.MapPath("~/Images/" + newHizmet.ImageName);
                    FileInfo file = new FileInfo(eski);
                    if (file.Exists)//check file exsit or not
                    {
                        file.Delete();
                    }
                }
                newHizmet.ImageName = fileName;
            }
            else
            {
                newHizmet.ImageName = newHizmet.ImageName;
            }
            newHizmet.Baslik = hizmetler.Baslik;
            newHizmet.Icerik = hizmetler.Icerik;
            newHizmet.Tarih = hizmetler.Tarih;
            newHizmet.Description = hizmetler.Description;
            newHizmet.Keyword = hizmetler.Keyword;


            if (ModelState.IsValid)
            {
                db.Entry(newHizmet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmetler);
        }
        // GET:hizmetler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetler hizmetler = db.Hizmetlers.Find(id);
            if (hizmetler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetler);
        }

        // POST: hizmetler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hizmetler = db.Hizmetlers.Find(id);
            db.Hizmetlers.Remove(hizmetler);
            db.SaveChanges();
            //resim siliniyor
            if (hizmetler.ImageName != "urun.png")
            {
                string eski = Server.MapPath("~/Images/" + hizmetler.ImageName);
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

