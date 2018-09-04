using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Delegates;
using TableDependency.SqlClient.Base.EventArgs;

namespace MetodosEnLinea
{
    public class DBNotificacion
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SignalRDemo"].ConnectionString;

        #region Control Monitoreo
        private SqlTableDependency<ControlMonitoreo> _dependencyControlMonitoreo;

        public void Observar_ControlMonitoreo(ChangedEventHandler<ControlMonitoreo> pDependencyOnChanged)
        {
            _dependencyControlMonitoreo = new SqlTableDependency<ControlMonitoreo>(_connectionString, "ControlMonitoreo");
            _dependencyControlMonitoreo.OnChanged += pDependencyOnChanged;
            _dependencyControlMonitoreo.OnError += _dependency_OnError;
        }

        public void Iniciar_ControlMonitoreo()
        {
            _dependencyControlMonitoreo.Start();
        }

        public void Detener_ControlMonitoreo()
        {
            _dependencyControlMonitoreo.Stop();
        }

        static void _dependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }
        #endregion

        #region Votos Usuario
        private SqlTableDependency<VotoUsuario> _dependencyVotosUsuario;

        public void EstablecerDependencia(ChangedEventHandler<VotoUsuario> pDependencyOnChanged)
        {
            _dependencyVotosUsuario = new SqlTableDependency<VotoUsuario>(_connectionString, "VotosUsuario");
            _dependencyVotosUsuario.OnChanged += pDependencyOnChanged;
            _dependencyVotosUsuario.OnError += _dependencyVotosUsuario_OnError;
        }

        public void Iniciar_Dependencia()
        {
            _dependencyVotosUsuario.Start();
        }

        public void Detener_Dependencia()
        {
            _dependencyVotosUsuario.Stop();
        }

        static void _dependencyVotosUsuario_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }
        #endregion
    }
}
