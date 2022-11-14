using QLSV_BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLSV_BlockChain.Areas.DangTai.Controllers
{
    public class MonHocController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();

        // GET: DangTai/MonHoc
        public ActionResult Index()
        {
            if (Session["userAdmin"] == null)
            {
                return Redirect("~/Admin/HomeAdmin/DangNhap") ;
            }
            var lstMonHoc = db.MonHocs.ToList();
            return View(lstMonHoc);
        }

        // GET: Admin/NguoiDungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMonHoc,TenMonHoc")] MonHoc monHoc)
        {
            if(db.MonHocs.FirstOrDefault(p => p.MaMonHoc.Equals(monHoc.MaMonHoc)) != null)
            {
                ViewBag.ThongBao = "Mã môn học đã tồn tại, vui lòng nhập mã khác !!!";
                return View(monHoc);
            }
            if (ModelState.IsValid)
            {
                db.MonHocs.InsertOnSubmit(monHoc);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(monHoc);
        }

        // GET: Admin/NguoiDungs/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHocs.FirstOrDefault(p => p.MaMonHoc.Equals(id));
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // POST: Admin/NguoiDungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMonHoc, TenMonHoc")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                var edit = db.MonHocs.FirstOrDefault(p => p.MaMonHoc.Equals(monHoc.MaMonHoc));
                edit.TenMonHoc = monHoc.TenMonHoc;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(monHoc);
        }

        // GET: Admin/NguoiDungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHocs.FirstOrDefault(p => p.MaMonHoc.Equals(id));
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MonHoc monHoc = db.MonHocs.FirstOrDefault(p => p.MaMonHoc.Equals(id));
            db.MonHocs.DeleteOnSubmit(monHoc);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}