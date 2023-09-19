using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Model
{
    public class Paciente:Persona
    {
        public string Historial { get; set; }



        public Paciente(int id, string name, string lastName, string secondLastName/*, string photo*/, DateTime birthdate, string phone, string ci, string email, string addres, string latitude, string longitude, string municipio, string historial)
        : base(name, lastName, secondLastName/*, photo*/, birthdate, phone, ci, email, addres, latitude, longitude, municipio)
        {
            Id = id;            
            Historial = historial;
        }

        public Paciente( string name, string lastName, string secondLastName/*, string photo*/, DateTime birthdate, string phone, string ci, string email, string addres, string latitude, string longitude, string municipio, string historial)
        : base(name, lastName, secondLastName/*, photo*/, birthdate, phone, ci, email, addres, latitude, longitude, municipio)
        {
            
            Historial = historial;
        }

    }
}
