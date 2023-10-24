using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NurseProjecDAO.Tools
{
    public class Tools
    {

        public static bool ValidarTextoConÑ(string texto)
        {
            string patron = @"^[a-zA-ZñÑáéíóúÁÉÍÓÚÜ\s']+$";

            return Regex.IsMatch(texto, patron);
        }

        public static bool ValidarCi(string ci)
        {           

            Regex regex = new Regex(@"\b\d+(?:-\w+)?\b");
            bool esValido = regex.IsMatch(ci);

            
            return esValido;
        }


        public static bool validarCorreo(string email)
        {
            string patron = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";           

            return Regex.IsMatch(email, patron);

        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(?:6|7)\d{1,3}\d{1,4}\d{1,4}$";
            
            return Regex.IsMatch(phoneNumber, pattern);

        }


        public static bool ValidarContraseña(string contraseña)
        {
            bool contieneMinuscula = Regex.IsMatch(contraseña, @"[a-z]");
            bool contieneMayuscula = Regex.IsMatch(contraseña, @"[A-Z]");
            bool contieneCaracterEspecial = Regex.IsMatch(contraseña, @"[!@#$%^&*()_+\-=[\]{};':\""<>,.?/\\]");
            bool tieneLongitudSuficiente = contraseña.Length >= 8;

            return contieneMinuscula && contieneMayuscula && contieneCaracterEspecial && tieneLongitudSuficiente;
        }
        public static bool ValidarCantidad(string cantidadText, out short cantidad)
        {
            cantidad = 0;

            // Validar cantidad
            if (!short.TryParse(cantidadText, out cantidad) || cantidad <= 0)
            {
                return false;
            }

            return true;
        }             
             

        public static string EliminarEspacios(string cadena)
        {
            return Regex.Replace(cadena.Trim(), @"\s+", " ");
        }

        public static bool VlAdress(string texto)        {
            
            string patron = @"^[a-zA-Z0-9,#'°ñäëïöüáéíóú¿¡""\s]+$";            
            bool esValido = Regex.IsMatch(texto, patron);
            return esValido;
        }

        public static bool ValidateUsername(string username)
        {            
            string pattern = @"^[a-zA-Z0-9_]{4,8}$";
            bool isValid = Regex.IsMatch(username, pattern);
            return isValid;
        }

    }
}
