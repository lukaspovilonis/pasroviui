using System.Data.SqlClient;
using System.Web.Mvc;

namespace PSK.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var connectionString = @"Data Source=(LocalDB)\mssqllocaldb;Integrated Security=false;AttachDbFileName=|DataDirectory|\Database.mdf";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

           //do some stuff with db :>

            connection.Close();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}