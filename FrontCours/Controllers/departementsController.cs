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
using System.Net.Http;

namespace FrontCours.Controllers
{
    public class departementsController : Controller
    {

        // GET: departements
        public async Task<ActionResult> Index()
        {
            // url de l'api
            string url = "https://localhost:44345/api/departements";

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var liste = await response.Content.ReadAsAsync<IEnumerable<departement>>();

                return View(liste);

            }
        }

        // GET: departements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // url de l'api
            string url = "https://localhost:44345/api/departements/"+id;

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var dep = await response.Content.ReadAsAsync<departement>();


                return View(dep);

            }
        }

        //// GET: departements/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: departements/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "id_dep,nom_dep,reg_dep")] departement departement)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.departements.Add(departement);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(departement);
        //}

        // GET: departements/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    departement departement = await db.departements.FindAsync(id);
        //    if (departement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(departement);
        //}

        // POST: departements/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "id_dep,nom_dep,reg_dep")] departement departement)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(departement).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(departement);
        //}

        // GET: departements/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    departement departement = await db.departements.FindAsync(id);
        //    if (departement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(departement);
        //}

        // POST: departements/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    departement departement = await db.departements.FindAsync(id);
        //    db.departements.Remove(departement);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
