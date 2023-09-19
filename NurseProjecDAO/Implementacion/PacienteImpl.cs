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
            throw new NotImplementedException();
        }

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

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Paciente t)
        {
            throw new NotImplementedException();
        }
    }
}
