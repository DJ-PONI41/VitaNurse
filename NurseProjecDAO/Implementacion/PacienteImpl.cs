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
    public class PacienteImpl : BaseImpl, IPaciente
    {
        public int Delete(Paciente t)
        {
            query = @"UPDATE Person SET status = 0, lastUpdate = CURRENT_TIMESTAMP
              WHERE id = @idPerson;
              UPDATE Paciente SET status = 0, lastUpdate = CURRENT_TIMESTAMP
              WHERE id = @idPaciente
               UPDATE [User] SET status = 0, lastUpdate = CURRENT_TIMESTAMP
              WHERE id = @idUser";




            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idPerson", t.Id);
            command.Parameters.AddWithValue("@idPaciente", t.Id);
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
        //este no se 
        public int Insert(Paciente t)
        {        


            query = @"INSERT INTO Person(names,lastName,secondLastName,birthdate,phone,ci,email,addres,latitude,longitude,municipio)
		            VALUES(@names,@lastName,@secondLastName,@birthdate,@phone,@ci,@email,@addres,@latitude,@longitude,@municipio)";

            string query2 = @"INSERT INTO Paciente(id,historial)
			                Values(@id,@historial)";

            List<SqlCommand> commands = Create2BasicCommand(query, query2);

            commands[0].Parameters.AddWithValue("@names", t.Name);
            commands[0].Parameters.AddWithValue("@lastName", t.LastName);
            commands[0].Parameters.AddWithValue("@secondLastName", t.SecondLastName);
            //commands[0].Parameters.AddWithValue("@photo", t.Photo);
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
            commands[1].Parameters.AddWithValue("@historial", t.Historial);

            return ExecuteNBasicCommand(commands);
        }


        //este se usa 
        public int InsertP2(User t, Paciente t2)
        {
            query = @"INSERT INTO Person(names,lastName,secondLastName,photo,birthdate,phone,ci,email,addres,latitude,longitude,municipio)
		            VALUES(@names,@lastName,@secondLastName,@photo,@birthdate,@phone,@ci,@email,@addres,@latitude,@longitude,@municipio)";

            string query2 = @"INSERT INTO [User](id,nameUser,password,role)
			                Values(@id,@nameUser,HASHBYTES('MD5',@password),@role)";

            string query3 = @"INSERT INTO Paciente(id,historial)
			                Values(@id2,@historial)";

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
            commands[2].Parameters.AddWithValue("@historial", t2.Historial);



            return ExecuteNBasicCommand(commands);
        }


        public Paciente Get(short Id)
        {           

            try
            {
                Paciente t = null;
                string query = @"SELECT P.id, P.names AS Nombre, P.lastName AS 'Apellido Paterno', P.secondLastName AS 'Apellido Materno', ISNULL(P.birthdate, CURRENT_TIMESTAMP) AS 'Fecha de nacimiento',ISNULL(P.photo, NULL) AS Fotografia
						,P.phone AS Celular,P.ci AS CI, 
                        P.email AS Correo,P.addres Direccion,P.latitude,P.longitude,P.municipio AS Municipio,E.historial AS 'Historial Medico'
                        FROM Person P 
                        INNER JOIN Paciente E ON P.id = E.id
						
                        WHERE P.id = @id";

                SqlCommand command = CreateBasicCommand(query);
                command.Parameters.AddWithValue("@id", Id);

                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Paciente();
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
                    t.Historial = table.Rows[0]["Historial Medico"].ToString();

                    if (table.Rows[0]["Fotografia"] != DBNull.Value)
                    {
                        byte[] photoData = (byte[])table.Rows[0]["Fotografia"];

                        t.PhotoData = photoData;
                    }
                    else
                    {
                        t.PhotoData = null;
                    }


                }
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            

        }



        public DataTable Select()
        {
            query = @"SELECT P.id, P.names AS Nombre, P.lastName AS 'Apellido Paterno', P.secondLastName AS 'Apellido Materno', ISNULL(P.birthdate, CURRENT_TIMESTAMP) AS 'Fecha de nacimiento',P.phone AS Celular,P.ci AS CI, 
                        P.email AS Correo,P.addres Direccion,U.role AS Rol,E.historial AS 'Historial Medico'
                        FROM Person P 
                        INNER JOIN Paciente E ON P.id = E.id
						INNER JOIN [User] U ON E.id = U.id
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

        public int Update(Paciente t)
        {
            throw new NotImplementedException();
        }
    }
}
