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
    public class UserImpl : BaseImpl, IUser
    {
        public int Delete(User t)
        {
            throw new NotImplementedException();
        }


        // este no se usa 
        public int Insert(User t)
        {
            throw new NotImplementedException();

        }

        //este se usa para Paciente
        public int Insert2(User t, Paciente t2)
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

        //este se usa para Enferera

        public int Insert2(User t, Nurse t2)
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
            //commands[2].Parameters.Add("@lugarTitulacion", SqlDbType.VarBinary).Value = t2.LugarTitulacion;
            commands[2].Parameters.AddWithValue("@lugarTitulacion", t2.LugarTitulacion);
            //commands[2].Parameters.Add("@datos", SqlDbType.VarBinary).Value = t2.Cvc;
            commands[2].Parameters.AddWithValue("@datos", t2.Cvc);


            return ExecuteNBasicCommand(commands);
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(User t)
        {
            throw new NotImplementedException();
        }
        public DataTable Login(string nameUser, string password)
        {
            query = @"SELECT U.id, U.nameUser, U.role
                    FROM [User] U
                    WHERE U.status = 1 
                      AND U.nameUser = @nameUser 
                      AND U.password = HASHBYTES('MD5', @password)
                      AND (
                        U.role <> 'Enfermera' OR 
                        (U.role = 'Enfermera' AND EXISTS (
                          SELECT 1 
                          FROM Person P 
                          INNER JOIN nurse N ON P.id = N.id 
                          WHERE P.id = U.id AND N.approval = 1
                        ))
                      );
                    ";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("nameUser", nameUser);
            command.Parameters.AddWithValue("password", password).SqlDbType = SqlDbType.VarChar;
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable LoginNurse(string nameUser, string password)
        {
            query = @"SELECT U.id, U.nameUser, U.role
                      FROM [User] U
                      WHERE U.status = 1 
                        AND U.nameUser = '@nameUser' 
                        AND U.password = HASHBYTES('MD5', '@password' )
                        AND (U.role <> 'Enfermera' OR 
                             (U.role = 'Enfermera' AND EXISTS 
                             (SELECT 1 FROM Person P INNER JOIN nurse N ON P.id = N.id WHERE P.id = U.id AND N.approval = 1)))";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("nameUser", nameUser);
            command.Parameters.AddWithValue("password", password).SqlDbType = SqlDbType.VarChar;
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int UpdatePassword(string UserName, string password, string nuevaPasword, short UserID)
        {
            query = @"UPDATE [User]
                    SET password = HASHBYTES('MD5', @nuevaPasword),
                        lastUpdate = CURRENT_TIMESTAMP,
                        id = @userID
                    WHERE nameUser = @userName AND password = HASHBYTES('MD5', @password);";

            SqlCommand command = CreateBasicCommand(query);

            command.Parameters.AddWithValue("@userName", UserName);
            command.Parameters.AddWithValue("@password", password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@nuevaPasword", nuevaPasword).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@userID", UserID);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
