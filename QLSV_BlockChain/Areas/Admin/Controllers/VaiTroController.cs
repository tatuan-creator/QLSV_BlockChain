using QLSV_BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSV_BlockChain.Areas.Admin.Controllers
{
    public class VaiTroController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();
        // GET: Admin/VaiTro
        public ActionResult Index()
        {
            if (Session["userAdmin"] == null)
            {
                return RedirectToAction("Dangnhap", "HomeAdmin");
            }
            var vaiTro = db.VaiTros.ToList();
            return View(vaiTro);
        }
    }
}