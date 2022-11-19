using QLSV_BlockChain.Common;
using QLSV_BlockChain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace QLSV_BlockChain.Areas.KiemDinh.Controllers
{
    public class KiemDinhDiemController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();
        // GET: KiemDinh/KiemDinhDiem
        public ActionResult Index()
        {
            if (Session["userAdmin"] == null)
            {
                return Redirect("~/Admin/HomeAdmin/DangNhap");
            }
            var lstBangDiem = db. BangDiems.ToList();
            return View(lstBangDiem);
        }

        public ActionResult Dangxuat()
        {
            Session["userAdmin"] = null;
            return Redirect("~/Admin/HomeAdmin/DangNhap");
        }

        public ActionResult Confirm(int id)
        {
            var bangDiem = db.BangDiems.FirstOrDefault(p => p.MaBangDiem == id);
            return View(bangDiem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KiemDinhQuyTrinh(FormCollection quytrinhKiemDinh)
        {
            String hash = "";
            SHA256 mySHA256 = SHA256.Create();
            String maQT = quytrinhKiemDinh["MaBangDiem"].ToString();
            String khoabimat = quytrinhKiemDinh["khoabimat"].ToString();
            BangDiem qt = db.BangDiems.FirstOrDefault(p => p.MaBangDiem.Equals(maQT));
            int maND = (int)qt.MaNguoiDung;
            NguoiDung ND = db.NguoiDungs.FirstOrDefault(p => p.MaNguoiDung.Equals(maND));
            String soN = ND.SoN;
            BigInteger n = BigInteger.Parse(soN);
            BigInteger sk = BigInteger.Parse(khoabimat);
            String pathFile = "D:/QLSV_BlockChain/QLSV_BlockChain/Content/images/" + qt.TepTinChungThuc.ToString();
            FileStream fs = System.IO.File.OpenRead(pathFile);
            byte[] by = mySHA256.ComputeHash(fs);
            for (int i = 0; i < by.Length; i++)
                hash += MaHoaRSA.tranform_binary(by[i]);
            hash = MaHoaRSA.tranform_decimal(hash);
            // Chữ kí của cơ quan kiểm định
            string sig = MaHoaRSA.RSA_MaHoa(hash, sk, n).ToString();
            qt.ChuKy = sig;
            if (qt.ChuKy == null || qt.ChuKy.Trim().Equals(""))
            {
                ViewBag.ThongBao = "Ký lỗi !!! chữ ký rỗng";
                return RedirectToAction("Index","KiemDinhDiem");
            }
            else
            {
                qt.TrangThai = 1;
                var dateSign = DateTime.Now.ToString("dd/MM/yyyy");
                qt.NgayKy = dateSign;
                db.SubmitChanges();
                ViewBag.ThongBao = "Ký Thành công";
                return RedirectToAction("Index", "KiemDinhDiem");
            }
        }
    }
}