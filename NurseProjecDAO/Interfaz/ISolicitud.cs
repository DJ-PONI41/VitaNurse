using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Interfaz
{
    internal interface ISolicitud : IBase<Solicitud>
    {
        DataTable Select_esp(int idNurse);
    }
}
