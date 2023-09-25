using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NurseProjecDAO.Model;


namespace NurseProjecDAO
{
    public class User:Persona
    {

        public string NameUser { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        //public User(int id,string name, string lastName, string secondLastName/*, string photo*/, DateTime birthdate, string phone, string ci, string email, string addres, string latitude, string longitude, string municipio, string nameUser, string password, string role)
        //: base(name, lastName, secondLastName/*, photo*/, birthdate, phone, ci, email, addres, latitude, longitude, municipio)
        //{
        //    Id = id; 
        //    NameUser = nameUser;
        //    Password = password;
        //    Role = role;
        //}

        public User(string name, string lastName, string secondLastName, byte[] photoData, DateTime birthdate, string phone, string ci, string email, string addres, string latitude, string longitude, string municipio, string nameUser, string password, string role)
         :base( name,  lastName, secondLastName, photoData,  birthdate,  phone,  ci,  email,  addres,  latitude,  longitude,  municipio)
        {
            NameUser = nameUser;
            Password = password;
            Role = role;
        }

    }
}
