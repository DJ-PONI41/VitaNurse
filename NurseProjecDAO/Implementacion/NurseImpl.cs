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
            commands[2].Parameters.AddWithValue("@lugarTitulacion", t2.LugarTitulacion);
            commands[2].Parameters.AddWithValue("@datos", t2.Cvc);



            return ExecuteNBasicCommand(commands);
        }

        public DataTable Select()
        {
            query = @"SELECT P.id, P.names AS Nombre, P.lastName AS 'Apellido Paterno', P.secondLastName AS 'Apellido Materno', ISNULL(P.birthdate, CURRENT_TIMESTAMP) AS 'Fecha de nacimiento',P.phone AS Celular,P.ci AS CI, 
                        P.email AS Correo,P.addres Direccion,U.role AS Rol,N.especialidad AS Especialidad,N.añoTitulacion AS 'Año de Titulacion'
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


        public Nurse Get(int Id)
        {
            Nurse t = null;
            string query = @"SELECT P.id, P.names AS Nombre, P.lastName AS 'Apellido Paterno', P.secondLastName AS 'Apellido Materno', ISNULL(P.birthdate, CURRENT_TIMESTAMP) AS 'Fecha de nacimiento',P.phone AS Celular,P.ci AS CI, 
                        P.email AS Correo,P.addres Direccion,P.latitude,P.longitude,P.municipio AS Municipio,
                        N.especialidad AS Especialidad,N.añoTitulacion AS 'Año de Titulacion'
                        FROM Person P 
                        INNER JOIN Nurse N ON P.id = N.id
						
                        WHERE P.id = @id";

            /*,N.lugarTitulacion AS 'Universidad de Egreso',N.datos AS Cvc*/

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", Id);

            DataTable table = ExecuteDataTableCommand(command);
            if (table.Rows.Count > 0)
            {
                t = new Nurse();
                t.Id = short.Parse(table.Rows[0]["id"].ToString());
                t.Name = table.Rows[0]["Nombre"].ToString();
                t.LastName = table.Rows[0]["Apellido Paterno"].ToString();
                t.SecondLastName = table.Rows[0]["Apellido Materno"].ToString();
                t.Ci = table.Rows[0]["CI"].ToString();
                t.Birthdate = (DateTime)table.Rows[0]["Fecha de nacimiento"];

                t.Phone = table.Rows[0]["Celular"].ToString();
                t.Email = table.Rows[0]["Correo"].ToString();
                t.Addres = table.Rows[0]["Direccion"].ToString();
                t.Latitude = table.Rows[0]["latitude"].ToString();
                t.Longitude = table.Rows[0]["longitude"].ToString();
                t.Municipio = table.Rows[0]["Municipio"].ToString();
                t.Especialidad = table.Rows[0]["Especialidad"].ToString();
                t.AñoTitulacion = (DateTime)table.Rows[0]["Año de Titulacion"];

            }

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return t;

        }

        public int Update(Nurse t)
        {
            throw new NotImplementedException();
        }
    }
}
