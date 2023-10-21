using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NurseProjecDAO.Model;
namespace NurseProjecDAO.Model
{
    public class Solicitud:BaseModel
    {
    

        public int Id { get; set; }
        public string LatitudePaciente { get; set; }
        public string LongitudePaciente { get; set; }
        public string LatitudeNurse { get; set; }
        public string LongitudeNurse { get; set; }
        public string Distancia { get; set; }
        public DateTime FechaHora { get; set; }
       
        public string Detalles { get; set; }
        public int IdPaciente { get; set; }
        public int IdNurse { get; set;}


        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="latitudePaciente"></param>
        /// <param name="longitudePaciente"></param>
        /// <param name="latitudeNurse"></param>
        /// <param name="longitudeNurse"></param>
        /// <param name="distancia"></param>
        /// <param name="fechaHora"></param>
        /// <param name="estadoSolicitud"></param>
        /// <param name="detalles"></param>
        /// <param name="idPaciente"></param>
        /// <param name="idNurse"></param>
        public Solicitud( string latitudePaciente, string longitudePaciente, string latitudeNurse, string longitudeNurse, string distancia, DateTime fechaHora,  string detalles, int idPaciente, int idNurse)
        {
            
            LatitudePaciente = latitudePaciente;
            LongitudePaciente = longitudePaciente;
            LatitudeNurse = latitudeNurse;
            LongitudeNurse = longitudeNurse;
            Distancia = distancia;
            FechaHora = fechaHora;
            
            Detalles = detalles;
            IdPaciente = idPaciente;
            IdNurse = idNurse;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="latitudePaciente"></param>
        /// <param name="longitudePaciente"></param>
        /// <param name="latitudeNurse"></param>
        /// <param name="longitudeNurse"></param>
        /// <param name="distancia"></param>
        /// <param name="fechaHora"></param>
        /// <param name="estadoSolicitud"></param>
        /// <param name="detalles"></param>
        /// <param name="idPaciente"></param>
        /// <param name="idNurse"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>

        public Solicitud(int id, string latitudePaciente, string longitudePaciente, string latitudeNurse, string longitudeNurse, string distancia, DateTime fechaHora,  string detalles, int idPaciente, int idNurse, byte status, DateTime registerDate, DateTime lastUpdate)
        : base(status,registerDate,lastUpdate)
        {
            Id = id;
            LatitudePaciente = latitudePaciente;
            LongitudePaciente = longitudePaciente;
            LatitudeNurse = latitudeNurse;
            LongitudeNurse = longitudeNurse;
            Distancia = distancia;
            FechaHora = fechaHora;
            
            Detalles = detalles;
            IdPaciente = idPaciente;
            IdNurse = idNurse;
        }

        public Solicitud(int id, string latitudePaciente, string longitudePaciente, string latitudeNurse, string longitudeNurse, string distancia, DateTime fechaHora, string detalles, int idPaciente, int idNurse)
        {
            Id = id;
            LatitudePaciente = latitudePaciente;
            LongitudePaciente = longitudePaciente;
            LatitudeNurse = latitudeNurse;
            LongitudeNurse = longitudeNurse;
            Distancia = distancia;
            FechaHora = fechaHora;
            
            Detalles = detalles;
            IdPaciente = idPaciente;
            IdNurse = idNurse;
        }

        public Solicitud()
        {
            
        }
    }
}
