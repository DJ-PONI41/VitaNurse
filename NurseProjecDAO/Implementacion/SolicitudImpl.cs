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
    public class SolicitudImpl : BaseImpl, ISolicitud
    {
        public int Delete(Solicitud t)
        {
            query = @"UPDATE solicitud SET  estadoSolicitud = 0
                    WHERE id = @idSolicitud";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idSolicitud", t.Id);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Insert(Solicitud t)
        {
            query = @"INSERT INTO solicitud(latitudePaciente,longitudePaciente,fechaHora,
						detalles,idPaciente,idNurse)
						VALUES(@latitudePaciente,@longitudePaciente,@fechaHora,
						@detalles,@idPaciente,@idNurse)";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@latitudePaciente", t.LatitudePaciente);
            command.Parameters.AddWithValue("@longitudePaciente", t.LongitudePaciente);
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

        public DataTable Select_esp(int idNurse)
        {
            query = @"SELECT S.id AS ID, (Pe.names + ' ' + Pe.lastName + ' ' + Pe.secondLastName) AS Nombre, Pe.municipio AS Municipio, S.fechaHora AS Fecha, S.estadoSolicitud AS Estado FROM Solicitud S
                      INNER JOIN Paciente P ON P.id = S.idPaciente
                      INNER JOIN Person Pe ON Pe.id = P.id
                      WHERE S.status = 1 AND S.idNurse = idNurse";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idNurse", idNurse);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Solicitud Get(int id)
        {
            try
            {
                Solicitud solicitud = null;

                string query = @"SELECT S.Id, (Pe.names + ' ' + Pe.lastName + ' ' + Pe.secondLastName) AS Nombre, Pe.municipio AS Municipio, S.LatitudePaciente, S.LongitudePaciente, S.FechaHora, S.Detalles, S.IdPaciente
                                 FROM Solicitud S
                                 INNER JOIN Paciente P ON P.id = S.idPaciente
                                 INNER JOIN Person Pe ON Pe.id = P.id
                                 WHERE S.Id = @id";

                using (SqlCommand command = CreateBasicCommand(query))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (DataTable table = ExecuteDataTableCommand(command))
                    {
                        if (table.Rows.Count > 0)
                        {
                            DataRow row = table.Rows[0];

                            solicitud = new Solicitud
                            {
                                Id = int.Parse(row["id"].ToString()),
                                LatitudePaciente = row["LatitudePaciente"].ToString(),
                                LongitudePaciente = row["LongitudePaciente"].ToString(),
                                FechaHora = (DateTime)row["FechaHora"],
                                Detalles = row["Detalles"].ToString(),
                                IdPaciente = int.Parse(row["IdPaciente"].ToString())
                            };

                            //solicitud = new Solicitud
                            //{
                            //    //Id = int.Parse(row["Id"].ToString()),
                            //    //LatitudePaciente = row["LatitudePaciente"].ToString(),
                            //    //LongitudePaciente = row["LongitudePaciente"].ToString(),
                            //    //LatitudeNurse = row["LatitudeNurse"].ToString(),
                            //    //LongitudeNurse = row["LongitudeNurse"].ToString(),
                            //    //Distancia = row["Distancia"].ToString(),
                            //    //FechaHora = (DateTime)row["FechaHora"],
                            //    //Detalles = row["Detalles"].ToString(),
                            //    //IdPaciente = int.Parse(row["IdPaciente"].ToString()),
                            //    //IdNurse = int.Parse(row["IdNurse"].ToString()),
                            //    //Status = (byte)row["Status"],
                            //    //RegisterDate = (DateTime)row["RegisterDate"],
                            //    //LastUpdate = (DateTime)row["LastUpdate"]
                                
                            //};
                        }
                    }
                }

                return solicitud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Solicitud t)
        {
            try
            {
                string query = @"
            UPDATE Solicitud
            SET LatitudeNurse = @latitudeNurse,
                LongitudeNurse = @longitudeNurse,
                Distancia = @distancia,
                EstadoSolicitud = 1,
                LastUpdate = CURRENT_TIMESTAMP
            WHERE Id = @id";

                SqlCommand command = CreateBasicCommand(query);

                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@latitudeNurse", t.LatitudeNurse);
                command.Parameters.AddWithValue("@longitudeNurse", t.LongitudeNurse);
                command.Parameters.AddWithValue("@distancia", t.Distancia);

                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                // Manejar la excepción o registrarla adecuadamente
                throw ex;
            }
        }

    }
}
