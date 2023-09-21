using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Model
{
    public class Persona:BaseModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Photo { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Ci { get; set; }
        public string Email { get; set; }
        public string Addres { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Municipio { get; set; }

        
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="secondLastName"></param>
        /// <param name="photo"></param>
        /// <param name="birthdate"></param>
        /// <param name="phone"></param>
        /// <param name="ci"></param>
        /// <param name="email"></param>
        /// <param name="addres"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="municipio"></param>
        public Persona( string name, string lastName, string secondLastName/*, string photo*/, DateTime birthdate, string phone, string ci, string email, string addres, string latitude, string longitude, string municipio)
        {
            
            Name = name;
            LastName = lastName;
            SecondLastName = secondLastName;
            //Photo = photo;
            Birthdate = birthdate;
            Phone = phone;
            Ci = ci;
            Email = email;
            Addres = addres;
            Latitude = latitude;
            Longitude = longitude;
            Municipio = municipio;
        }
         public Persona() 
            {
            
            }
    }
}
