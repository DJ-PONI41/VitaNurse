﻿using NurseProjecDAO.Interfaz;
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

        public int Insert(User t)
        {
            query = @"INSERT INTO Person(names,lastName,secondLastName,birthdate,phone,ci,email,addres,latitude,longitude,municipio)
		            VALUES(@names,@lastName,@secondLastName,@birthdate,@phone,@ci,@email,@addres,@latitude,@longitude,@municipio)";

            string query2 = @"INSERT INTO [User](id,nameUser,password,role)
			                Values(@id,@nameUser,HASHBYTES('MD5',@password),@role)";

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
            commands[1].Parameters.AddWithValue("@nameUser", t.NameUser);
            commands[1].Parameters.AddWithValue("@password", t.Password).SqlDbType = SqlDbType.VarChar;
            commands[1].Parameters.AddWithValue("@role", t.Role);
            
            

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
            query = @"SELECT id, nameUser,role
                    FROM [User]
                    WHERE status= 1 AND nameUser=@nameUser 
                    AND password=HASHBYTES('MD5',@password)";
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
    }
}
