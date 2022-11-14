using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLSV_BlockChain.Models;

namespace QLSV_BlockChain.Areas.DangTai.Controllers
{
    public class BangDiemController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();
        private const string SAVE_PATH = "/Content/images";
        // GET: DangTai/BangDiem
        public ActionResult Index()
        {
            if (Session["userAdmin"] == null)
            {
                return Redirect("~/Admin/HomeAdmin/DangNhap");
            }
            var lstBangDiem = db.BangDiems.ToList();
            return View(lstBangDiem);
        }

        // GET: Admin/KiemDinhs/Create
        public ActionResult Create()
        {
            ViewBag.SinhVien = new SelectList(db.SinhViens, "IDSinhVien", "TenSinhVien");
            ViewBag.NguoiDung = new SelectList(db.NguoiDungs.Where(p=> p.VaiTro.TenVaiTro.Equals("Kiểm Định")), "MaNguoiDung", "MoTa");
            ViewBag.HocKy = new SelectList(db.HocKies, "MaHocKy", "TenHocKy");
            ViewBag.MonHoc = new SelectList(db.MonHocs, "MaMonHoc", "TenMonHoc");
            return View();
        }

        // POST: Admin/KiemDinhs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBangDiem, MaSinhVien, MaNguoiDung, MaHocKy, MaMonHoc, NgayKy, ChuKy, TepTinChungThuc, Trang Thai, TrangThaiXacThuc")] BangDiem bangDiem, HttpPostedFileBase ImageUpload)
        {
            bangDiem.TrangThaiXacThuc = 0;
            if(ImageUpload != null)
            {
                string name = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                string filename = SAVE_PATH + name + DateTime.Now.ToString("dd0mm0yyyy0hh0mm0ss0mmm") + extension;
                bangDiem.TepTinChungThuc = filename;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath(SAVE_PATH), filename));
            }
            if (ModelState.IsValid)
            {
                db.BangDiems.InsertOnSubmit(bangDiem);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SinhVien = new SelectList(db.SinhViens, "MaSinhVien", "TenSinhVien");
            ViewBag.NguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TenNguoiDung");
            ViewBag.HocKy = new SelectList(db.HocKies, "MaHocKy", "TenHocKy");
            ViewBag.MonHoc = new SelectList(db.MonHocs, "MaMonHoc", "TenMonHoc");
            return View(bangDiem);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            BangDiem bangDiem = db.BangDiems.FirstOrDefault(p => p.MaBangDiem.Equals(id));
            if(bangDiem == null)
            {
                return HttpNotFound();
            }
            return View(bangDiem);
        }
    }
}