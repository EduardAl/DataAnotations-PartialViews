using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAnotations_PartialViews.Models;

namespace DataAnotations_PartialViews.Controllers
{
    public class marcasController : Controller
    {
        private autosEntities db = new autosEntities();

        // GET: marcas
        public ActionResult Index()
        {
            return View(db.marcas.ToList());
        }

        // GET: marcas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marca marca = db.marcas.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // GET: marcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: marcas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_marca,marca1,pais,imagen")] marca marca, HttpPostedFileBase file)
        {
            if (file == null) return View(marca);

            string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
            marca.imagen = archivo;
            file.SaveAs(Server.MapPath("~/Uploads/" + archivo));
            if (ModelState.IsValid)
            {
                db.marcas.Add(marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marca);
        }

        // GET: marcas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marca marca = db.marcas.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: marcas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_marca,marca1,pais,imagen")] marca marca, HttpPostedFileBase file)
        {
            string archivo;
            if (file != null)
            {
                archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                marca.imagen = archivo;
                archivo = marca.imagen;
                try
                {
                    file.SaveAs(Server.MapPath("~/Uploads/" + archivo));
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            if (ModelState.IsValid)
            {
                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marca);
        }

        // GET: marcas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marca marca = db.marcas.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            marca marca = db.marcas.Find(id);
            try
            {
                String borrar = Server.MapPath("~/Uploads/" + marca.imagen.ToString());
                System.IO.File.Delete(borrar);
            }
            catch (Exception)
            {

                throw;
            }
            db.marcas.Remove(marca);
            try
            {
                db.SaveChanges();

            }
            catch (Exception)
            {
                marca.ErrorMessage = "No se puede elminar. Esta marca posee registros de modelos.";
                ModelState.AddModelError("marca1", "");
                return View("Delete", marca);
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
