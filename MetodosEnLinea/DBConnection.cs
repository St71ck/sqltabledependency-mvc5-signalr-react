using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MetodosEnLinea
{
    public class DBConnection
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SignalRDemo"].ConnectionString;

        #region Frutas
        public IEnumerable<Fruta> ObtenerFrutas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT dbo.Frutas.FrutaID, dbo.Frutas.Nombre, dbo.Frutas.Cantidad FROM dbo.Frutas", connection))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new Fruta()
                            {
                                FrutaID = x.GetInt32(0),
                                Nombre = x.GetString(1),
                                Cantidad = x.GetInt32(2)
                            }).ToList();
                }
            }
        }

        public void SetVotoFrutas(string pNombre)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE [dbo].[Frutas] SET Cantidad = Cantidad + 1 WHERE Nombre = @Nombre", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", pNombre);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Votos Usuario
        public void RegistrarVoto(VotoUsuario pVoto)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO [dbo].[VotosUsuario] (UsuarioId, Fruta, Fecha) VALUES(@UsuarioId, @Fruta, @Fecha)", connection))
                {
                    command.Parameters.AddWithValue("@UsuarioId", pVoto.UsuarioId);
                    command.Parameters.AddWithValue("@Fruta", pVoto.Fruta);
                    command.Parameters.AddWithValue("@Fecha", pVoto.Fecha);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public void RegistrarTiempoNotificacion(VotoUsuario pVoto)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO [dbo].[TiempoNotificacion] (VotoID, UsuarioId, Fruta, Fecha) VALUES(@VotoID, @UsuarioId, @Fruta, @Fecha)", connection))
                {
                    command.Parameters.AddWithValue("@VotoID", pVoto.VotoID);
                    command.Parameters.AddWithValue("@UsuarioId", pVoto.UsuarioId);
                    command.Parameters.AddWithValue("@Fruta", pVoto.Fruta);
                    command.Parameters.AddWithValue("@Fecha", pVoto.Fecha);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
