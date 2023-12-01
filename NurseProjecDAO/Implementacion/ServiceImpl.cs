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
    public class ServiceImpl : BaseImpl, IService
    {
        public int Delete(Service t)
        {
            string query = "UPDATE state = 0 WHERE id = @Id";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Id", t.Id);
            command.Parameters.AddWithValue("@State", t.State);

            return ExecuteCrudCommand(command);
        }

        public Service Get(int id)
        {
            try
            {
                Service service = null;

                string query = @"
        SELECT
            id, name AS Nombre, description AS Descripcion,
            price AS Precio, state AS Estado
        FROM
            Service
        WHERE
            id = @id";

                using (SqlCommand command = CreateBasicCommand(query))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (DataTable table = ExecuteDataTableCommand(command))
                    {
                        if (table.Rows.Count > 0)
                        {
                            DataRow row = table.Rows[0];

                            service = new Service
                            {
                                Id = Convert.ToInt32(row["id"]),
                                Name = row["Nombre"].ToString(),
                                Description = row["Descripcion"].ToString(),
                                Price = double.Parse(row["Precio"].ToString()),
                                State = Convert.ToByte(row["Estado"])
                            };
                        }
                    }
                }

                return service;
            }
            catch (Exception ex)
            {
                // Manejar la excepción o registrarla adecuadamente
                throw ex;
            }

        }

        public int Insert(Service t)
        {
            string query = "INSERT INTO Service (name, description, price, state) VALUES (@Name, @Description, @Price, @State)";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Name", t.Name);
            command.Parameters.AddWithValue("@Description", t.Description);
            command.Parameters.AddWithValue("@Price", t.Price);
            command.Parameters.AddWithValue("@State", t.State);

            return ExecuteCrudCommand(command);

        }

        public DataTable Select()
        {
            query = @"select * from Service where state=1";

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

        public DataTable Select2()
        {
            query = @"SELECT name AS Nombre , description AS Descripción , price AS 'Precio BS.'
                        FROM [Service]
                        where state = 1";

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

        public int Update(Service t)
        {
            string query = "UPDATE Service SET name = @Name, description = @Description, price = @Price, state = @State WHERE id = @Id";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Id", t.Id);
            command.Parameters.AddWithValue("@Name", t.Name);
            command.Parameters.AddWithValue("@Description", t.Description);
            command.Parameters.AddWithValue("@Price", t.Price);
            command.Parameters.AddWithValue("@State", t.State);

            return ExecuteCrudCommand(command);

        }
    }
}
