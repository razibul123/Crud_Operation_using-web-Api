using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi_Mvc_Crud.Models;
using System.Net.Http;

namespace WebApi_Mvc_Crud.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<Newempreg> empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:49451/api/EmpCrud");
            var consumeapi = hc.GetAsync("EmpCrud");
            consumeapi.Wait();

            var readdata = consumeapi.Result;

            if(readdata.IsSuccessStatusCode)
            {

                var displaydata = readdata.Content.ReadAsAsync<IList<Newempreg>>();

                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }

        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]

        public ActionResult Create(Newempreg insertemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:49451/api/EmpCrud");
            var insertrecord = hc.PostAsJsonAsync<Newempreg>("EmpCrud", insertemp);
            insertrecord.Wait();
            var savedata = insertrecord.Result;

            if(savedata.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }

            return View("Create");

        }

        public ActionResult Details(int id)
            {

            EmpClass empobj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:49451/api/");

            var consumeapi = hc.GetAsync("EmpCrud?id=" + id.ToString());

            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {

                var displaydata = readdata.Content.ReadAsAsync<EmpClass>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }

            return View(empobj);
        }
       
        public ActionResult Edit(int id)
        {

            EmpClass empobj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:49451/api/");

            var consumeapi = hc.GetAsync("EmpCrud?id=" + id.ToString());

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {

                var displaydata = readdata.Content.ReadAsAsync<EmpClass>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }

            return View(empobj);
        }

        [HttpPost]
        public ActionResult Edit(EmpClass ec)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:49451/api/EmpCrud");

            var insertrecord = hc.PutAsJsonAsync<EmpClass>("EmpCrud", ec);
            insertrecord.Wait();
            var savedata = insertrecord.Result;

            if (savedata.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }

            else
            {

                ViewBag.message = "Employe Record Not Update";
            }

            return View(ec);
        }

        public ActionResult Delete(int id)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:49451/api/EmpCrud");
            var delrecord = hc.DeleteAsync("EmpCrud/" + id.ToString());

            delrecord.Wait();

            var displaydata = delrecord.Result;

            if(displaydata.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}