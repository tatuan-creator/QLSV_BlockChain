using System.Web.Mvc;

namespace QLSV_BlockChain.Areas.KiemDinh
{
    public class KiemDinhAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KiemDinh";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KiemDinh_default",
                "KiemDinh/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}