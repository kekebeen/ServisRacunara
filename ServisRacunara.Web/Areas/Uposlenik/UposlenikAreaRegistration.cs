using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Uposlenik
{
    public class UposlenikAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Uposlenik";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Uposlenik_default",
                "Uposlenik/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}