using MetodosEnLinea;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using SignalR.Hubs;
using System;

namespace SignalR.Models
{
    public class VotoUsuarioModel
    {
        public void IniciarNotificacionMonitoreo()
        {
            DBNotificacion notificacion = new DBNotificacion();
            notificacion.EstablecerDependencia(_dependency_OnChanged);
            notificacion.Iniciar_Dependencia();
        }

        public void DetenerNotificacionMonitoreo()
        {
            DBNotificacion notificacion = new DBNotificacion();
            notificacion.Detener_Dependencia();
        }

        void _dependency_OnChanged(object sender, RecordChangedEventArgs<VotoUsuario> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                DBConnection conexion = new DBConnection();
                VotoUsuario voto = e.Entity;
                voto.Fecha = DateTime.Now;
                conexion.RegistrarTiempoNotificacion(e.Entity);
                FrutaHub.NotificarVoto(e.Entity);
            }
        }

        public void RegistrarVotoUsuario(VotoUsuario pVoto)
        {
            DBConnection conexion = new DBConnection();
            pVoto.Fecha = DateTime.Now;
            conexion.RegistrarVoto(pVoto);
        }
    }
}