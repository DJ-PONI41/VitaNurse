using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Interfaz
{
    internal interface IService : IBase<Service>
    {
        Service Get(int id);
    }
}
