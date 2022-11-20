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
            KiemTraChuoi();
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
            String pathFile = "C:/QLSV_BlockChain/QLSV_BlockChain" + qt.TepTinChungThuc.ToString();
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
                qt.previousHash = FindPrepHash(qt.MaSinhVien);
                qt.TimeStamp = DateTime.Now.ToString();
                qt.TrangThai = 1;
                var dateSign = DateTime.Now.ToString("dd/MM/yyyy");
                qt.NgayKy = dateSign;
                db.SubmitChanges();
                ViewBag.ThongBao = "Ký Thành công";
                return RedirectToAction("Index", "KiemDinhDiem");
            }
        }
        public string FindPrepHash(int? IDSinhVien)
        {
            var sv = db.BangDiems.Where(p => p.MaSinhVien == IDSinhVien && p.TrangThai == 1).ToList();
            int tong = sv.Count;
            if(sv.Count == 0)
            {
                return "0";
            }
            else
            {
                var ptcuoi = sv[sv.Count-1];
                return ptcuoi.ChuKy;
            }
        }

        public void KiemTraChuoi()
        {
            var sinhVien = db.SinhViens.ToList();
            foreach(var item in sinhVien)
            {
                var chuoiDiem = db.BangDiems.Where(p => p.MaSinhVien == item.IDSinhVien && p.TrangThai == 1).ToList();
                for(int i=1;i<chuoiDiem.Count;i++)
                {
                    if (chuoiDiem[i].previousHash != chuoiDiem[i - 1].ChuKy)
                    {
                        chuoiDiem[i].TrangThai = 2;
                    }
                    else
                    {
                        chuoiDiem[i].TrangThai = 1;
                    }
                }    
            }
        }

        public void KiemTraChuKy(int? id)
        {
            int demQTDaXacThuc = 0;
            SHA256 mySHA256 = SHA256.Create();
            SinhVien sinhVien = db.SinhViens.FirstOrDefault(p => p.IDSinhVien == id);
            var bangDiem = db.BangDiems.Where(x => x.MaSinhVien == id).ToList();
            foreach (var qt in bangDiem)
            {
                String hash = "";
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaNguoiDung == qt.MaNguoiDung);
                BigInteger e = BigInteger.Parse(nd.SoE);
                BigInteger n = BigInteger.Parse(nd.SoN);
                String ktchuki = MaHoaRSA.RSA_MaHoa(qt.ChuKy, e, n);
                String pathFile = "C:/QLSV_BlockChain/QLSV_BlockChain/" + qt.TepTinChungThuc.ToString();
                FileStream fs = System.IO.File.OpenRead(pathFile);
                byte[] by = mySHA256.ComputeHash(fs);
                for (int i = 0; i < by.Length; i++)
                    hash += MaHoaRSA.tranform_binary(by[i]);
                hash = MaHoaRSA.tranform_decimal(hash);
                if (ktchuki.Equals(hash))
                {
                    qt.TrangThai = 1;
                    demQTDaXacThuc += 1;
                    db.SubmitChanges();
                }
                else
                {
                    qt.TrangThai = 2;
                    db.SubmitChanges();
                }
            }
        }

    }
}