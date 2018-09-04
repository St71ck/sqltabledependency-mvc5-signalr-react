using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SignalR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            new VotoUsuarioModel().IniciarNotificacionMonitoreo();
            //new ControlMonitoreoModel().IniciarNotificacionMonitoreo();
        }

        protected void Application_End()
        {
            //new ControlMonitoreoModel().DetenerNotificacionMonitoreo();
            new VotoUsuarioModel().DetenerNotificacionMonitoreo();
        }

        protected void Application_Error(object sender, EventArgs e)
        {

            Exception err = Server.GetLastError();
        }
    }
}
