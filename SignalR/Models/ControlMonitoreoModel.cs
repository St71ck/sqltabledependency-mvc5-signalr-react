using MetodosEnLinea;
using SignalR.Hubs;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR.Models
{
    public class ControlMonitoreoModel
    {
        public void IniciarNotificacionMonitoreo()
        {
            DBNotificacion notificacion = new DBNotificacion();
            notificacion.Observar_ControlMonitoreo(_dependency_OnChanged);
            notificacion.Iniciar_ControlMonitoreo();
        }

        public void DetenerNotificacionMonitoreo()
        {
            DBNotificacion notificacion = new DBNotificacion();
            notificacion.Detener_ControlMonitoreo();
        }

        void _dependency_OnChanged(object sender, RecordChangedEventArgs<ControlMonitoreo> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                FrutaHub.CambioSucursal(e.Entity.SucursalId);
            }
        }
    }
}