using Kelloggs.Functions;
using Kelloggs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Kelloggs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        public ActionResult Actividades()
        {
            return View();
        }

        public ActionResult Adulting()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Personajes()
        {
            return View();
        }

        public ActionResult Records()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveData(ContactoData data)
        {
            try { 
                    ProxyGet proxyQry = new ProxyGet();
                    Parameters param = new Parameters
                    {
                        Parameter = new List<Parameter>
                        {
                            new Parameter { Field = "User", Object = data.UserName },
                            new Parameter { Field = "Email", Object = data.Email },
                            new Parameter { Field = "Contry", Object = data.Contry }
                        }
                    };
                    string jsonDta = proxyQry.CallAPIPost("Kelots/Servicio", param);
                    var response = JsonConvert.DeserializeObject<SingleResponseJson<bool>>(jsonDta);
                    ResponseData sendData = new ResponseData()
                    {
                        Success = response.Success && response.Data,
                        Send = false,
                    };
                    Session["IsSuccess"] = sendData;
            }catch (Exception ex)
            {
                DDLog.objLog4Net.Error("Application_Error: Home-Controller call web api error post SaveData", ex);
                ResponseData sendData = new ResponseData()
                {
                    Success = false,
                    Send = false,
                };
                Session["IsSuccess"] = sendData;
            }

            return RedirectToAction("Index","Home");
        }

        public ActionResult FormData()
        {
            return View();
        }
    }
}