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
    public class repuestoesController : Controller
    {
        private autosEntities db = new autosEntities();

        // GET: repuestoes
        public async Task<ActionResult> Index()
        {
            var repuestos = db.repuestos.Include(r => r.modelo);
            return View(await repuestos.ToListAsync());
        }

        // GET: repuestoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repuesto repuesto = await db.repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        // GET: repuestoes/Create
        public ActionResult Create()
        {
            ViewBag.id_modelos = new SelectList(db.modelos, "id_modelos", "modelo1");
            return View();
        }

        // POST: repuestoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_rep,nombre,precio,descuento,id_modelos")] repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                db.repuestos.Add(repuesto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_modelos = new SelectList(db.modelos, "id_modelos", "modelo1", repuesto.id_modelos);
            return View(repuesto);
        }

        // GET: repuestoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repuesto repuesto = await db.repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_modelos = new SelectList(db.modelos, "id_modelos", "modelo1", repuesto.id_modelos);
            return View(repuesto);
        }

        // POST: repuestoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_rep,nombre,precio,descuento,id_modelos")] repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repuesto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_modelos = new SelectList(db.modelos, "id_modelos", "modelo1", repuesto.id_modelos);
            return View(repuesto);
        }

        // GET: repuestoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repuesto repuesto = await db.repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return HttpNotFound();
            }
            return View(repuesto);
        }

        // POST: repuestoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            repuesto repuesto = await db.repuestos.FindAsync(id);
            db.repuestos.Remove(repuesto);
            await db.SaveChangesAsync();
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
