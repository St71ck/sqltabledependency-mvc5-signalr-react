using MetodosEnLinea;
using SignalR.Hubs;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult SurveyQuiz()
        {
            var poll = new
            {
                question = "Cuál es su fruta favorita?",
                choices = new FrutaModel().ObtenerFrutas().Select(x => new { name = x.Nombre, count = x.Cantidad }).ToList()
            };
            return Json(poll, JsonRequestBehavior.AllowGet);
        }

        public string RegistrarVotoUsuario(string pUsuario, string pFruta)
        {
            VotoUsuario voto = new VotoUsuario
            {
                Fecha = DateTime.Now,
                Fruta = pFruta,
                UsuarioId = pUsuario
            };
            new VotoUsuarioModel().RegistrarVotoUsuario(voto);
            return "";
        }
    }
}