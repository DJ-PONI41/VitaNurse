using NurseProjecDAO.Interfaz;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Implementacion
{
    public class NurseImpl : BaseImpl, INurse
    {
        public int Delete(Nurse t)
        {
            query = @"UPDATE Person SET status = 0, lastUpdate = CURRENT_TIMESTAMP
              WHERE id = @idPerson;
              UPDATE Nurse SET status = 0, lastUpdate = CURRENT_TIMESTAMP
              WHERE id = @idNurse
               UPDATE [User] SET status = 0, lastUpdate = CURRENT_TIMESTAMP
              WHERE id = @idUser";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idPerson", t.Id);
            command.Parameters.AddWithValue("@idNurse", t.Id);
            command.Parameters.AddWithValue("@idUser", t.Id);

            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Insert(Nurse t)
        {
            throw new NotImplementedException();
        }
        public int InsertN2(User t, Nurse t2)
        {
            query = @"INSERT INTO Person(names,lastName,secondLastName,photo,birthdate,phone,ci,email,addres,latitude,longitude,municipio)
		            VALUES(@names,@lastName,@secondLastName,@photo,@birthdate,@phone,@ci,@email,@addres,@latitude,@longitude,@municipio)";

            string query2 = @"INSERT INTO [User](id,nameUser,password,role)
			                Values(@id,@nameUser,HASHBYTES('MD5',@password),@role)";

            string query3 = @"INSERT INTO Nurse(id,especialidad,añoTitulacion,lugarTitulacion,datos)
			                Values(@id2,@especialidad,@añoTitulacion,@lugarTitulacion,@datos)";

            List<SqlCommand> commands = Create3BasicCommand(query, query2, query3);

            commands[0].Parameters.AddWithValue("@names", t.Name);
            commands[0].Parameters.AddWithValue("@lastName", t.LastName);
            commands[0].Parameters.AddWithValue("@secondLastName", t.SecondLastName);
            commands[0].Parameters.AddWithValue("@photo", t.PhotoData);
            commands[0].Parameters.AddWithValue("@birthdate", t.Birthdate);
            commands[0].Parameters.AddWithValue("@phone", t.Phone);
            commands[0].Parameters.AddWithValue("@ci", t.Ci);
            commands[0].Parameters.AddWithValue("@email", t.Email);
            commands[0].Parameters.AddWithValue("@addres", t.Addres);
            commands[0].Parameters.AddWithValue("@latitude", t.Latitude);
            commands[0].Parameters.AddWithValue("@longitude", t.Longitude);
            commands[0].Parameters.AddWithValue("@municipio", t.Municipio);


            short id = short.Parse(GetGenerateIDTable("Person"));

            commands[1].Parameters.AddWithValue("@id", id);
            commands[1].Parameters.AddWithValue("@nameUser", t.NameUser);
            commands[1].Parameters.AddWithValue("@password", t.Password).SqlDbType = SqlDbType.VarChar;
            commands[1].Parameters.AddWithValue("@role", t.Role);

            commands[2].Parameters.AddWithValue("@id2", id);
            commands[2].Parameters.AddWithValue("@especialidad", t2.Especialidad);
            commands[2].Parameters.AddWithValue("@añoTitulacion", t2.AñoTitulacion);
            commands[2].Parameters.Add("@lugarTitulacion", SqlDbType.VarBinary).Value = t2.LugarTitulacion;
            //commands[2].Parameters.AddWithValue("@lugarTitulacion", t2.LugarTitulacion);
            commands[2].Parameters.Add("@datos", SqlDbType.VarBinary).Value = t2.Cvc;
            //commands[2].Parameters.AddWithValue("@datos", t2.Cvc);


            return ExecuteNBasicCommand(commands);
        }

        public DataTable Select()
        {
            query = @"SELECT P.id, P.names AS Nombre, P.lastName AS 'Apellido Paterno', P.secondLastName AS 'Apellido Materno', ISNULL(P.birthdate, CURRENT_TIMESTAMP) AS 'Fecha de nacimiento'
						,P.phone AS Celular,P.ci AS CI,P.email AS Correo,P.addres Direccion,U.role AS Rol,N.especialidad AS Especialidad,N.añoTitulacion AS 'Año de Titulacion'
						,N.lugarTitulacion AS 'Titulo Profesional',N.datos CV
                        FROM Person P 
                        INNER JOIN Nurse N ON P.id = N.id
						INNER JOIN [User] U ON N.id = U.id
                        WHERE P.status = 1 ";

            SqlCommand command = CreateBasicCommand(query);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public Nurse Get(int id)
        {
            try
            {
                Nurse nurse = null;

                string query = @"
            SELECT
                P.id, P.names AS Nombre, P.lastName AS ApellidoPaterno, P.secondLastName AS ApellidoMaterno,
                ISNULL(P.birthdate, CURRENT_TIMESTAMP) AS FechaNacimiento, P.phone AS Celular, P.ci AS CI,
                ISNULL(P.photo, NULL) AS Fotografia, P.email AS Correo, P.addres AS Direccion,
                P.latitude, P.longitude, P.municipio AS Municipio,
                N.especialidad AS Especialidad, N.añoTitulacion AS AñoTitulacion,
                ISNULL(N.lugarTitulacion, NULL) AS TituloProfesional, ISNULL(N.datos, NULL) AS CV
            FROM
                Person P
            INNER JOIN
                Nurse N ON P.id = N.id
            WHERE
                P.id = @id";

                using (SqlCommand command = CreateBasicCommand(query))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (DataTable table = ExecuteDataTableCommand(command))
                    {
                        if (table.Rows.Count > 0)
                        {
                            DataRow row = table.Rows[0];

                            nurse = new Nurse
                            {
                                Id = short.Parse(row["id"].ToString()),
                                Name = row["Nombre"].ToString(),
                                LastName = row["ApellidoPaterno"].ToString(),
                                SecondLastName = row["ApellidoMaterno"].ToString(),
                                Ci = row["CI"].ToString(),
                                Birthdate = (DateTime)row["FechaNacimiento"],
                                Phone = row["Celular"].ToString(),
                                Email = row["Correo"].ToString(),
                                Addres = row["Direccion"].ToString(),
                                Latitude = row["latitude"].ToString(),
                                Longitude = row["longitude"].ToString(),
                                Municipio = row["Municipio"].ToString(),
                                Especialidad = row["Especialidad"].ToString(),
                                AñoTitulacion = (DateTime)row["AñoTitulacion"],
                                PhotoData = row["Fotografia"] as byte[],
                                LugarTitulacion = row["TituloProfesional"] as byte[],
                                Cvc = row["CV"] as byte[]
                            };
                        }
                    }
                }

                return nurse;
            }
            catch (Exception ex)
            {
                // Manejar la excepción o registrarla adecuadamente
                throw ex;
            }
        }


        public int Update(Nurse t)
        {
            throw new NotImplementedException();
        }
        public int UpdateN(Nurse t)
        {

            query = @"UPDATE Person SET names = @names,lastName = @lastName,secondLastName = @secondLastName,photo = @photo,birthdate = @birthdate , phone = @phone,ci = @ci
                    ,email = @email,addres = @addres,latitude = @latitude,longitude = @longitude,municipio = @municipio, lastUpdate = CURRENT_TIMESTAMP
		             WHERE id = @idP";    

            string query2 = @"UPDATE Nurse Set especialidad = @especialidad,añoTitulacion = @añoTitulacion,lugarTitulacion = @lugarTitulacion,datos = @datos, lastUpdate = CURRENT_TIMESTAMP
			                WHERE id = @idN";


            List<SqlCommand> commands = Create2BasicCommand(query, query2);

            commands[0].Parameters.AddWithValue("@idP",t.Id);
            commands[0].Parameters.AddWithValue("@names", t.Name);
            commands[0].Parameters.AddWithValue("@lastName", t.LastName);
            commands[0].Parameters.AddWithValue("@secondLastName", t.SecondLastName);
            commands[0].Parameters.AddWithValue("@photo", t.PhotoData);
            commands[0].Parameters.AddWithValue("@birthdate", t.Birthdate);
            commands[0].Parameters.AddWithValue("@phone", t.Phone);
            commands[0].Parameters.AddWithValue("@ci", t.Ci);
            commands[0].Parameters.AddWithValue("@email", t.Email);
            commands[0].Parameters.AddWithValue("@addres", t.Addres);
            commands[0].Parameters.AddWithValue("@latitude", t.Latitude);
            commands[0].Parameters.AddWithValue("@longitude", t.Longitude);
            commands[0].Parameters.AddWithValue("@municipio", t.Municipio);
                       

            commands[1].Parameters.AddWithValue("@idN", t.Id);
            commands[1].Parameters.AddWithValue("@especialidad",t.Especialidad);
            commands[1].Parameters.AddWithValue("@añoTitulacion", t.AñoTitulacion);
            commands[1].Parameters.Add("@lugarTitulacion", SqlDbType.VarBinary).Value = t.LugarTitulacion;
            //commands[2].Parameters.AddWithValue("@lugarTitulacion", t2.LugarTitulacion);
            commands[1].Parameters.Add("@datos", SqlDbType.VarBinary).Value = t.Cvc;
            //commands[2].Parameters.AddWithValue("@datos", t2.Cvc);
                       

            try
            {
                return ExecuteNBasicCommand(commands);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
