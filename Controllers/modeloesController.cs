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
    public class modeloesController : Controller
    {
        private autosEntities db = new autosEntities();

        // GET: modeloes
        public async Task<ActionResult> Index()
        {
            var modelos = db.modelos.Include(m => m.marca);
            return View(await modelos.ToListAsync());
        }

        // GET: modeloes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelo modelo = await db.modelos.FindAsync(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // GET: modeloes/Create
        public ActionResult Create()
        {
            ViewBag.id_marca = new SelectList(db.marcas, "id_marca", "marca1");
            return View();
        }

        // POST: modeloes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_modelos,modelo1,asientos,combustible,precio,year_modelo,id_marca")] modelo modelo)
        {
            if (ModelState.IsValid)
            {
                db.modelos.Add(modelo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_marca = new SelectList(db.marcas, "id_marca", "marca1", modelo.id_marca);
            return View(modelo);
        }

        // GET: modeloes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelo modelo = await db.modelos.FindAsync(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_marca = new SelectList(db.marcas, "id_marca", "marca1", modelo.id_marca);
            return View(modelo);
        }

        // POST: modeloes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_modelos,modelo1,asientos,combustible,precio,year_modelo,id_marca")] modelo modelo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modelo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_marca = new SelectList(db.marcas, "id_marca", "marca1", modelo.id_marca);
            return View(modelo);
        }

        // GET: modeloes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modelo modelo = await db.modelos.FindAsync(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // POST: modeloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            modelo modelo = await db.modelos.FindAsync(id);
            db.modelos.Remove(modelo);
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
