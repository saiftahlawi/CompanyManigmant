using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;
using System.IO;
namespace WebApplication6.Controllers
{
    public class CompaniesController : Controller
    {
        private ProjectDBEntities db = new ProjectDBEntities();

        // GET: Companies
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return View(db.Companies.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
              /*  if (company.Capital < 10000)
                {
                    ModelState.AddModelError("", "Capital should grater than 10000");
                    return View(company);
                }*/
                string path = "";
                if (imgFile.FileName.Length > 0)
               {
                    path = "~/images/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }
                company.Logo = path;
        

                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(company);
        }




        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Logo,Password,ConfEmail,ConfPass")] Company company, HttpPostedFileBase imgFile)
        {
           
                /*  if (company.Capital < 10000)
                  {
                      ModelState.AddModelError("", "Capital should grater than 10000");
                      return View(company);
                  }*/
                string path = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = "~/images/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }
                company.Logo = path;

            var befor = db.Companies.AsNoTracking().Where(x=> x.CID == company.CID).ToList().FirstOrDefault();
            company.Password = befor.Password;
            company.ConfEmail = befor.Email;
            company.ConfPass = befor.Password;

            db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
        
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
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

        public int Count()
        {
            return db.Companies.ToList().Count;
        }

        public int Max()
        {
            return db.Companies.ToList().Max(m=>m.Capital).Value;
        }
        public int Main()
        {
            return db.Companies.ToList().Min(m => m.Capital).Value;
        }
        public double Avg()
        {
            return db.Companies.ToList().Average(m => m.Capital).Value;
        }
        public ActionResult getManyRows()
        {
            // var rec = db.Companies.ToList();//select all record
            // var rec = db.Companies.ToList().OrderBy(x => x.Capital).ThenBy(x => x.CName);//all recourd stored by capital asc
            // var rec = db.Companies.ToList().OrderByDescending(x => x.Capital).ThenByDescending(x => x.CName); //all recourd stored by capital Descending
            //var rec = db.Companies.Where(x => x.Capital >= 30000 && x.Capital <= 600000).ToList(); //all recourd if condition is ttrue
            double avg = db.Companies.Average(x => x.Capital).Value; //get avarage
            var rec = db.Companies.Where(x => x.Capital > avg).ToList();
            return View(rec);
        }
        public ActionResult getOneRecord()
        {
            // var rec = db.Companies.Find(2); //find by key (get all company where cID=2)
            //var rec = db.Companies.Single(x => x.CID == 2);
            //var rec = db.Companies.Where(x => x.CID == 2).ToList().FirstOrDefault();

            //  var rec = db.Companies.ToList().OrderByDescending(X => X.Capital).FirstOrDefault(); //first Recourd
            var rec = db.Companies.ToList().OrderByDescending(X => X.Capital).LastOrDefault(); //Last Recourd
            return View(rec);
        }

        [HttpGet]
        public ActionResult Login()
        {
            Session["user"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")]Company company)
        {
            var rec = db.Companies.Where(x => x.Email == company.Email && x.Password == company.Password).ToList().FirstOrDefault();
            if(rec != null)
            {
                Session["user"] = rec.CName;
                return RedirectToAction("index");
            }else
            {
                ViewBag.Error = "invalid user";
                return View(company);
            }
           
        }
        public ActionResult HomePage()
        {
            var rec = db.Companies.ToList();
            return View(rec);
        }
    }
}
