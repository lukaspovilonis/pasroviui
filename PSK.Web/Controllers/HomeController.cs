using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using NHibernate.Util;
using PSK.Model;
using PSK.NHibernate;

namespace PSK.Web.Controllers
{
	public class HomeController : Controller
	{
		private IRepository _repository;


		public HomeController(IRepository repository)
		{
			_repository = repository;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Test()
		{
			var dummy = new Dummy();
			_repository.SaveOrUpdate(dummy);
			var meow = _repository.GetAll<Dummy>();
			return Json(meow.First().Id, JsonRequestBehavior.AllowGet);
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