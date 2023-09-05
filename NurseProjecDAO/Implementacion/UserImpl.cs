using NurseProjecDAO.Interfaz;
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
            throw new NotImplementedException();
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
