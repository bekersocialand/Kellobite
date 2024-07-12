using Kelloggs.App_Start;
using Kelloggs.Functions;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kelloggs
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Operaciones globales que inician cuando la aplicación arranca por primera vez
        /// <list type="bullet">
        /// <item>
        /// <term>AreaRegistration.RegisterAllAreas</term>
        /// <description>Devuelve un registro estandarizado por areas de aplicaccion MVC</description>
        /// </item>
        /// <item>
        /// <term>RouteConfig.RegisterRoutes</term>
        /// <description>Devuelve un registro estandarizado por ruta de acceso de la aplicaccion MVC</description>
        /// </item>
        /// <item>
        /// <term>log4net.Config.XmlConfigurator.Configure</term>
        /// <description>Carga la aplicaccion de log del aplicativo</description>
        /// </item>
        /// <item>
        /// <term>FilterConfig.RegisterGlobalFilters</term>
        /// <description>Devuelve un registro estandarizado de los filtros de acceso de la aplicaccion MVC</description>
        /// </item>
        /// </list>
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            DDLog.objLog4Net.Info("Inicia aplicación");
        }

        /// <summary>
        /// Encaso de que se precente un error no controlado almacenara el error y mandara a la vista de error 404
        /// </summary>
        /// <param name="sender">objeto al que pertenece el error y datos en conjunto para identificaccion de envio</param>
        /// <param name="e">evento en que se precenta el error e información del mimso</param>
        protected void Application_Error(object sender, EventArgs e)
        {

            Exception exc = Server.GetLastError();
            HttpContext context = HttpContext.Current;
            if (context != null && context.Session != null && exc.GetType() == typeof(HttpException))
            {
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                {
                    return;
                }

                DDLog.objLog4Net.Debug("Application_Error: Ingresando");
                DDLog.objLog4Net.Error("Application_Error: Dirección no valida", exc);
                DDLog.objLog4Net.Debug("Application_Error: Terminando");
                if (Session["cambiarVista"] != null)
                {
                    Response.Redirect("/Home/Error");
                }
            }

            DDLog.objLog4Net.Debug("Application_Error: Global Page Error");
            DDLog.objLog4Net.Error("Application_Error:", exc);
            DDLog.objLog4Net.Debug("Application_Error: Terminando");
            Server.ClearError();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            DDLog.objLog4Net.Info("Inicia sección");

        }

        protected void Session_End(object sender, EventArgs e)
        {
            DDLog.objLog4Net.Info("Cierra sección");

        }
    }
}
