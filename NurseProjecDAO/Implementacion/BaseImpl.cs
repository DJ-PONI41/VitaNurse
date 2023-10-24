using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NurseProjecDAO.Implementacion
{
    public class BaseImpl
    {

     
        string connectionString = @"Server=LAPTOP-70PJN8G4\SQLEXPRESS;Database=NurseProjectDB;User Id=sa;Password=Univalle";

        internal string query = "";
        public SqlCommand CreateBasicCommand()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;
        }

        public SqlCommand CreateBasicCommand(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);

            return command;
        }

        //Herencia para 2 listas 
        public List<SqlCommand> Create2BasicCommand(string sql1, string sql2)
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command1 = new SqlCommand(sql1, connection);
            commands.Add(command1);

            SqlCommand command2 = new SqlCommand(sql2, connection);
            commands.Add(command2);

            return commands;
        }


        //Herencia para 3 listas
        public List<SqlCommand> Create3BasicCommand(string sql1, string sql2 ,string sql3)
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command1 = new SqlCommand(sql1, connection);
            commands.Add(command1);

            SqlCommand command2 = new SqlCommand(sql2, connection);
            commands.Add(command2);

            SqlCommand command3 = new SqlCommand(sql3, connection);
            commands.Add(command3);

            return commands;
        }


        // TRANSACCIONES

        public string GetGenerateIDTable(string table)
        {
            query = "SELECT IDENT_CURRENT('" + table + "') +IDENT_INCR('" + table + "')";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                command.Connection.Open();
                return command.ExecuteScalar().ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally { command.Connection.Close(); }
        }


        private string GetLastInsertedId(string table)
        {
            string query = $"SELECT IDENT_CURRENT('{table}')";
            SqlCommand command = CreateBasicCommand(query);

            try
            {
                command.Connection.Open();                
                return command.ExecuteScalar().ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally { command.Connection.Close(); }
            
        }



        public int ExecuteBasicCommand(SqlCommand command)
        {
            try
            {
                command.Connection.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public int ExecuteNBasicCommand(List<SqlCommand> commands)
        {
            SqlTransaction transaction = null;
            SqlCommand command0 = commands[0];
            int n = 0;
            try
            {
                command0.Connection.Open();
                transaction = command0.Connection.BeginTransaction();

                foreach (SqlCommand item in commands)
                {
                    item.Transaction = transaction;
                    n += item.ExecuteNonQuery();
                }
                // return command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                command0.Connection.Close();
            }
            return n;
        }



        public int ExecuteCrudCommand(SqlCommand command)
        {
            SqlTransaction transaction = null;
            int rowsAffected = 0;

            try
            {
                command.Connection.Open();
                transaction = command.Connection.BeginTransaction();

                command.Transaction = transaction;
                rowsAffected = command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }

            return rowsAffected;
        }



        public DataTable ExecuteDataTableCommand(SqlCommand command)
        {
            DataTable table = new DataTable();

            try
            {
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }

    }
}
