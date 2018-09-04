using MetodosEnLinea;
using System.Collections.Generic;
using System.Linq;

namespace SignalR.Models
{
    public class FrutaModel
    {
        #region Propiedades
        public int FrutaID { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        #endregion

        public List<FrutaModel> ObtenerFrutas()
        {
            DBConnection connection = new DBConnection();
            return connection.ObtenerFrutas().Select(fruta => new FrutaModel()
            {
                Cantidad = fruta.Cantidad,
                FrutaID = fruta.FrutaID,
                Nombre = fruta.Nombre
            }).ToList();
        }

        public void VotarFruta(string pNombre)
        {
            DBConnection connection = new DBConnection();
            connection.SetVotoFrutas(pNombre);
        }
    }
}