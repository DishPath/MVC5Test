using DM2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DM2.Controllers
{
    public class User2Controller : Controller
    {
        DM1Entities db;

        public ActionResult List()
        {
            db = new DM1Entities();
            var x = db.tbl_user.ToList();
            return View(x);
        }

        public ActionResult AddNew()
        {
            ViewBag.Controller = "User2";
            ViewBag.Action = "AddNew";
            ViewBag.BtnText = "Save New";
            ViewBag.UserTypes = getUserTypes();
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(tbl_user entity)
        {
            db = new DM1Entities();
            db.tbl_user.Add(entity);
            db.SaveChanges();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            tbl_user x = null;
            if (id != null)
            {
                db = new DM1Entities();
                x = db.tbl_user.Find(id);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Controller = "User2";
            ViewBag.Action = "Edit";
            ViewBag.BtnText = "Update Existing";

            return View("AddNew", x);
        }

        [HttpPost]
        public ActionResult Edit(tbl_user en)
        {
            db = new DM1Entities();
            db.Entry(en).State = EntityState.Modified;
            db.SaveChanges();
            return View("List", db.tbl_user.ToList());
        }

        public ActionResult Delete(int? id)
        {
            tbl_user x = null;
            if (id != null)
            {
                db = new DM1Entities();
                x = db.tbl_user.Find(id);
                db.tbl_user.Remove(x);
                db.SaveChanges();
                return View("List", db.tbl_user.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public List<SelectListItem> getUserTypes()
        {
            db = new DM1Entities();
            List<SelectListItem> userTypes = new List<SelectListItem>();
            foreach (var item in db.tbl_usertype.ToList())
            {
                userTypes.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            return userTypes;
        }


    }
}