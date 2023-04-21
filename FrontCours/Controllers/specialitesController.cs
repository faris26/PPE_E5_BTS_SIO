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
using Newtonsoft.Json;
using System.Text;

namespace FrontCours.Controllers
{
    public class specialitesController : Controller
    {
       //private data_model db = new data_model();

        // GET: specialites
        public async Task<ActionResult> Index()
        {
            // url de l'api
            string url = "https://localhost:44345/api/specialites";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var liste = await response.Content.ReadAsAsync<IEnumerable<specialite>>();

                return View(liste);

            }
        }

        // GET: specialites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            // url de l'api
            string url = "https://localhost:44345/api/specialites/"+id;

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var spe = await response.Content.ReadAsAsync<specialite>();

                return View(spe);
            }
        }

        // GET: specialites/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: specialites/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_spe,lib_spe")] specialite specialite)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(specialite);
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", "123456789");
                    using (var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44345/api/specialites"))
                    {
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                        var reponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                        if (!reponse.IsSuccessStatusCode)
                        {
                            throw new Exception();
                        }

                        reponse.EnsureSuccessStatusCode();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(specialite);
        }

        //// GET: specialites/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    specialite specialite = await db.specialites.FindAsync(id);
        //    if (specialite == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(specialite);
        //}

        // POST: specialites/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "id_spe,lib_spe")] specialite specialite)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(specialite).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(specialite);
        //}

        // GET: specialites/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            string url = "https://localhost:44345/api/specialites/" + id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", "123456789");

                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var spe = await response.Content.ReadAsAsync<specialite>();
                return View(spe);
            }
        }

        // POST: specialites/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string url = "https://localhost:44345/api/specialites/" + id;

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
