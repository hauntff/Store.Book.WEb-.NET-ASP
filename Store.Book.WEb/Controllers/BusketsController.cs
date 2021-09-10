using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Store.Book.Model.DAO;
using Store.Book.WEb.Models;

namespace Store.Book.WEb.Controllers
{
    [Authorize]
    public class BusketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Buskets
        public async Task<ActionResult> Index()
        {
            return View(await db.Buskets.ToListAsync());
        }

        // GET: Buskets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Busket busket = await db.Buskets.FindAsync(id);
            if (busket == null)
            {
                return HttpNotFound();
            }
            return View(busket);
        }

        // GET: Buskets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buskets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id")] Busket busket)
        {
            if (ModelState.IsValid)
            {
                db.Buskets.Add(busket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(busket);
        }

        // GET: Buskets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Busket busket = await db.Buskets.FindAsync(id);
            if (busket == null)
            {
                return HttpNotFound();
            }
            return View(busket);
        }

        // POST: Buskets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] Busket busket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busket).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(busket);
        }

        // GET: Buskets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Busket busket = await db.Buskets.FindAsync(id);
            if (busket == null)
            {
                return HttpNotFound();
            }
            return View(busket);
        }

        // POST: Buskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Busket busket = await db.Buskets.FindAsync(id);
            db.Buskets.Remove(busket);
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
