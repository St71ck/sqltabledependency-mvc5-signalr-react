using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MetodosEnLinea;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using SignalR.Models;

namespace SignalR.Hubs
{
    [HubName("frutaHub")]
    public class FrutaHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void EnviarVoto(string pFruta)
        {
            new FrutaModel().VotarFruta(pFruta); 
        }

        public static void DatosCambiaron()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FrutaHub>();
            hubContext.Clients.All.enviarMensaje(new { cambio = true });
        }


        public static void CambioSucursal(int pSucursalId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FrutaHub>();
            hubContext.Clients.All.enviarMensaje(new { sucursalId = pSucursalId });
        }

        public static void NotificarVoto(VotoUsuario pVoto)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<FrutaHub>();
            hubContext.Clients.All.enviarMensaje(pVoto);
        }
    }
}