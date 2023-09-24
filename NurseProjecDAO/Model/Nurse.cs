using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Model
{
    public class Nurse : Persona
    {

        public string Especialidad { get; set; }
        public DateTime AñoTitulacion { get; set; }
        public byte[] LugarTitulacion { get; set; }
        public byte[] Cvc { get; set; }



        public Nurse(int id, string name, string lastName, string secondLastName, byte[] photoData, DateTime birthdate, string phone, string ci, string email, string addres, string latitude, string longitude, string municipio,string especialidad, DateTime añoTitulacion, byte[] lugarTitulacion, byte[] cvc)
        : base(name, lastName, secondLastName, photoData, birthdate, phone, ci, email, addres, latitude, longitude, municipio)
        {
            Id = id;
            Especialidad = especialidad;
            AñoTitulacion = añoTitulacion;
            LugarTitulacion = lugarTitulacion;
            Cvc = cvc;
        }


        public Nurse( string name, string lastName, string secondLastName, byte[] photoData, DateTime birthdate, string phone, string ci, string email, string addres, string latitude, string longitude, string municipio, string especialidad, DateTime añoTitulacion, byte[] lugarTitulacion, byte[] cvc)
        : base(name, lastName, secondLastName, photoData, birthdate, phone, ci, email, addres, latitude, longitude, municipio)
        {
            
            Especialidad = especialidad;
            AñoTitulacion = añoTitulacion;
            LugarTitulacion = lugarTitulacion;
            Cvc = cvc;
        }
        public Nurse()
        {
            
        }
    }
}
