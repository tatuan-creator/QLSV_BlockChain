using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSV_BlockChain.Areas.KiemDinh.Controllers
{
    public class KiemDinhDiemController : Controller
    {
        // GET: KiemDinh/KiemDinhDiem
        public ActionResult Index()
        {
            if (Session["userAdmin"] == null)
            {
                return Redirect("~/Admin/HomeAdmin/DangNhap");
            }
            return View();
        }

        public ActionResult Dangxuat()
        {
            Session["userAdmin"] = null;
            return Redirect("~/Admin/HomeAdmin/DangNhap");
        }
    }
}