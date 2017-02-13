using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Prodavac
{
    public class ProdavacAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Prodavac";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Prodavac_default",
                "Prodavac/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}