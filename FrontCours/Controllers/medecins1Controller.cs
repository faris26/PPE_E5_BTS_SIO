using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using ORM_PPE_SLAM;

namespace FrontCours.Controllers
{
    public class medecins1Controller : Controller
    {
        private data_model db = new data_model();

        // GET: medecins1
        public async Task<ActionResult> Index()
        {
            var medecins = db.medecins.Include(m => m.Departement).Include(m => m.Specialite);
            return View(await medecins.ToListAsync());
        }

        // GET: medecins1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medecin medecin = await db.medecins.FindAsync(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        // GET: medecins1/Create
        public ActionResult Create()
        {
            ViewBag.NumeroDepartement = new SelectList(db.departements, "NumeroDepartement", "NomRegion");
            ViewBag.IdSpecialite = new SelectList(db.specialites, "IdSpecialite", "LbSpecialite");
            return View();
        }

        // POST: medecins1/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdMedecin,NomMedecin,PrenomMedecin,AdresseMedecin,TelephoneMedecin,SpecialiteMedecin,IdSpecialite,NumeroDepartement")] medecin medecin)
        {
            if (ModelState.IsValid)
            {
                db.medecins.Add(medecin);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NumeroDepartement = new SelectList(db.departements, "NumeroDepartement", "NomRegion", medecin.NumeroDepartement);
            ViewBag.IdSpecialite = new SelectList(db.specialites, "IdSpecialite", "LbSpecialite", medecin.IdSpecialite);
            return View(medecin);
        }

        // GET: medecins1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medecin medecin = await db.medecins.FindAsync(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            ViewBag.NumeroDepartement = new SelectList(db.departements, "NumeroDepartement", "NomRegion", medecin.NumeroDepartement);
            ViewBag.IdSpecialite = new SelectList(db.specialites, "IdSpecialite", "LbSpecialite", medecin.IdSpecialite);
            return View(medecin);
        }

        // POST: medecins1/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdMedecin,NomMedecin,PrenomMedecin,AdresseMedecin,TelephoneMedecin,SpecialiteMedecin,IdSpecialite,NumeroDepartement")] medecin medecin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medecin).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NumeroDepartement = new SelectList(db.departements, "NumeroDepartement", "NomRegion", medecin.NumeroDepartement);
            ViewBag.IdSpecialite = new SelectList(db.specialites, "IdSpecialite", "LbSpecialite", medecin.IdSpecialite);
            return View(medecin);
        }

        // GET: medecins1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medecin medecin = await db.medecins.FindAsync(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        // POST: medecins1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            medecin medecin = await db.medecins.FindAsync(id);
            db.medecins.Remove(medecin);
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
