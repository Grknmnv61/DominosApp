using Dominos.Common.Enums;
using Dominos.Web.UI.Business.Helper;
using Dominos.Web.UI.Models;
using System.Web.Mvc;

namespace Dominos.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new ViewModel();

            var instance = new Instance(SubmitEnum.List);
            instance.Provider.Execute(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ViewModel model, SubmitEnum submit)
        {
            var instance = new Instance(submit);
            instance.Provider.Execute(model);
            return View(model);
        }
    }
}