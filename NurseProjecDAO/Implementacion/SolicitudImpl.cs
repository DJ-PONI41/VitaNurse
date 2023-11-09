using NurseProjecDAO.Interfaz;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Implementacion
{
    internal class SolicitudImpl : BaseImpl, ISolicitud
    {
        public int Delete(Solicitud t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Solicitud t)
        {
            query = @"INSERT INTO solicitud(latitudePaciente,longitudePaciente,latitudeNurse,longitudeNurse,distancia,fechaHora,
						detalles,idPaciente,idNurse)
						VALUES(@latitudePaciente,@longitudePaciente,@latitudeNurse,@longitudeNurse,@distancia,@fechaHora,
						@detalles,@idPaciente,@idNurse)";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@latitudePaciente", t.LatitudePaciente);
            command.Parameters.AddWithValue("@longitudePaciente", t.LongitudePaciente);
            command.Parameters.AddWithValue("@latitudeNurse", t.LatitudeNurse);
            command.Parameters.AddWithValue("@longitudeNurse", t.LongitudeNurse);
            command.Parameters.AddWithValue("@distancia", t.Distancia);
            command.Parameters.AddWithValue("@fechaHora", t.FechaHora);            
            command.Parameters.AddWithValue("@detalles", t.Detalles);
            command.Parameters.AddWithValue("@idPaciente", t.IdPaciente);
            command.Parameters.AddWithValue("@idNurse", t.IdNurse);
           

            try
            {

                return ExecuteCrudCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Solicitud t)
        {
            throw new NotImplementedException();
        }
    }
}
