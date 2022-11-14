using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLSV_BlockChain.Models;

namespace QLSV_BlockChain.Areas.DangTai.Controllers
{
    public class HocKyController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();
        // GET: DangTai/HocKy
        public ActionResult Index()
        {
            if (Session["userAdmin"] == null)
            {
                return Redirect("~/Admin/HomeAdmin/DangNhap");
            }
            var lstHocKy = db.HocKies.ToList();
            return View(lstHocKy);
        }

        // GET: Admin/NguoiDungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHocKy, TenHocKy")] HocKy hocKy)
        {
            if (ModelState.IsValid)
            {
                db.HocKies.InsertOnSubmit(hocKy);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(hocKy);
        }

        // GET: Admin/NguoiDungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKy hocKy = db.HocKies.FirstOrDefault(p => p.MaHocKy.Equals(id));
            if (hocKy == null)
            {
                return HttpNotFound();
            }
            return View(hocKy);
        }

        // POST: Admin/NguoiDungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHocKy, TenHocKy")] HocKy hocKy)
        {
            if (ModelState.IsValid)
            {
                var edit = db.HocKies.FirstOrDefault(p => p.MaHocKy.Equals(hocKy.MaHocKy));
                edit.TenHocKy = hocKy.TenHocKy;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(hocKy);
        }

        // GET: Admin/NguoiDungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKy hocKy = db.HocKies.FirstOrDefault(p => p.MaHocKy.Equals(id));
            if (hocKy == null)
            {
                return HttpNotFound();
            }
            return View(hocKy);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HocKy hocKy = db.HocKies.FirstOrDefault(p => p.MaHocKy.Equals(id));
            db.HocKies.DeleteOnSubmit(hocKy);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}