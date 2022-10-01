using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO.NET.Models;

namespace ADO.NET.Controllers
{
    public class BirdsController : Controller
    {
        // GET: Birds
        public ActionResult ShowAll()
        {
            Birdsrepository birdsresp = new Birdsrepository();
            ModelState.Clear();


            return View(birdsresp.DB());
            
        }
        public ActionResult UpdateBirds()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateBirds(BirdsModel bird)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Birdsrepository birdsresp = new Birdsrepository();

                    if (birdsresp.UpdateBirds(bird))
                    {
                        ViewBag.Message = "Birds details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditDetails(int id)
        {
            Birdsrepository birdsresp = new Birdsrepository();



            return View(birdsresp.DB().Find(bird => bird.ID == id));

        }
        [HttpPost]

        public ActionResult EditDetails(int id, BirdsModel bird)
        {
            try
            {
                Birdsrepository birdsresp = new Birdsrepository();

                birdsresp.EditBirds(bird);
                return RedirectToAction("ShowAll");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult DeleteBird(int id)
        {
            try
            {
                Birdsrepository birdsresp = new Birdsrepository();
                if (birdsresp.DeleteBirds(id))
                {
                    ViewBag.AlertMsg = "Bird details deleted successfully";

                }
                return RedirectToAction("ShowAll");

            }
            catch
            {
                return View();
            }
        }
    }
}