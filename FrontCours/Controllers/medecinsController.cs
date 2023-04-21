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
using FrontCours.Models;

namespace FrontCours.Controllers
{
    public class medecinsController : Controller
    {
        //private data_model db = new data_model();

        // GET: medecins
        public async Task<ActionResult> Index()
        {
            // url de l'api
            string url = "https://localhost:44345/api/medecins";

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var liste = await response.Content.ReadAsAsync<IEnumerable<medecin>>();

                return View(liste);

            }
        }

        // GET: medecins/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // url de l'api
            string url = "https://localhost:44345/api/medecins/" + id;

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var medecin = await response.Content.ReadAsAsync<medecin>();
                return View(medecin);
            }

        }

        // GET: medecins
        public async Task<ActionResult> SearchMedecins(string nom)
        {
            // url de l'api
            string url = "https://localhost:44345/api/medecins?nom=" + nom;


            using (HttpClient client = new HttpClient())
            {

                if (string.IsNullOrEmpty(nom) || nom.Length < 2)
                {
                    return HttpNotFound();
                }

                //client.DefaultRequestHeaders.Add("token", "123456789");
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(content))
                    {
                        var liste = JsonConvert.DeserializeObject<IEnumerable<medecin>>(content);
                        return View("Index", liste);
                    }
                }

                throw new Exception();

            }
        }



        // GET: medecins/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            string url_dep = "https://localhost:44345/api/departements";
            string url_spe = "https://localhost:44345/api/specialites";
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response_dep = await client.GetAsync(url_dep);
                HttpResponseMessage response_spe = await client.GetAsync(url_spe);


                //si erreur, on propage une exception
                if (!response_dep.IsSuccessStatusCode || !response_spe.IsSuccessStatusCode)
                    throw new Exception();

                var dep = response_dep.Content.ReadAsAsync<IEnumerable<departement>>().Result.ToList();
                var spe = response_spe.Content.ReadAsAsync<IEnumerable<specialite>>().Result.ToList();

                //ViewBag.NumeroDepartement = new SelectList(dep, "NumeroDepartement", "NomDepartement");
                //ViewBag.IdSpecialite = new SelectList(spe, "IdSpecialite", "LbSpecialite");

                ViewBag.NumeroDepartement = new SelectList(dep, "NumeroDepartement", "NomRegion");
                ViewBag.IdSpecialite = new SelectList(spe, "IdSpecialite", "LbSpecialite");


                //ViewBag.C_FK_id_dep = new SelectList(dep, "NumeroDepartement", "NomDepartement");
                //ViewBag.C_FK_id_spe = new SelectList(spe, "IdSpecialite", "LbSpecialite");


                return View();
            }
        }



        // POST: medecins/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "IdMedecin,NomMedecin,PrenomMedecin,AdresseMedecin,TelephoneMedecin,SpecialiteMedecin,IdSpecialite,NumeroDepartement")] medecin med)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(med);

                using (HttpClient client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Add("token", "123456789");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                    using (var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44345/api/Medecins"))
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
            return View(med);
        }

        // GET: Medecins/Edit/5

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = "https://localhost:44345/api/medecins/" + id;
            string url_dep = "https://localhost:44345/api/Departements";
            string url_spe = "https://localhost:44345/api/Specialites";

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(url);
                HttpResponseMessage response_dep = await client.GetAsync(url_dep);
                HttpResponseMessage response_spe = await client.GetAsync(url_spe);

                if (!response.IsSuccessStatusCode || !response_spe.IsSuccessStatusCode || !response_dep.IsSuccessStatusCode)
                {
                    throw new Exception();

                }



                var dep = response_dep.Content.ReadAsAsync<IEnumerable<departement>>().Result.ToList();
                var spe = response_spe.Content.ReadAsAsync<IEnumerable<specialite>>().Result.ToList();
                var medecin = await response.Content.ReadAsAsync<medecin>();

                //ViewBag.NumeroDepartement = new SelectList(dep, "NumeroDepartement", "NomDepartement", medecin.NumeroDepartement);
                //ViewBag.IdSpecialite = new SelectList(spe, "IdSpecialite", "LbSpecialite", medecin.IdSpecialite);

                ViewBag.NumeroDepartement = new SelectList(dep, "NumeroDepartement", "NomRegion");
                ViewBag.IdSpecialite = new SelectList(spe, "IdSpecialite", "LbSpecialite");

                return View(medecin);

            }
        }


        // POST: medecins/Edit/5
        //Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier.Pour
        //plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdMedecin,NomMedecin,PrenomMedecin,AdresseMedecin,TelephoneMedecin,SpecialiteMedecin,IdSpecialite,NumeroDepartement")] medecin med)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(med);


                using (HttpClient client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Add("token", "123456789");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                    HttpContent cont = new StringContent(json, Encoding.UTF8, "application/json");

                    var send = await client.PutAsync("https://localhost:44345/api/medecins/" + med.IdMedecin, cont).ConfigureAwait(false);
                    if (!send.IsSuccessStatusCode)
                        throw new Exception();

                    send.EnsureSuccessStatusCode();

                    return RedirectToAction("Index");

                }
            }
            return View(med);
        }

        // GET: medecins/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            string url = "https://localhost:44345/api/medecins/" + id;

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var medecin = await response.Content.ReadAsAsync<medecin>();
                return View(medecin);
            }
        }

        // POST: medecins/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            string url = "https://localhost:44345/api/medecins/" + id;

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                //client.DefaultRequestHeaders.Add("token", "123456789");
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

        private string ReadToken()
        {
            string token = string.Empty;
            try
            {
                string filename = @"C:\Windows\Temp\token.txt";
                token = System.IO.File.ReadAllText(filename);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return token;
        }
    }
}
