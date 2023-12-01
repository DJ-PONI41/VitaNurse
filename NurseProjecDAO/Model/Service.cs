using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseProjecDAO.Model
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte State { get; set; }

        // Constructor
        public Service(int id, string name, string description, double price, byte state)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            State = state;
        }

        public Service(string name, string description, double price, byte state)
        {
            Name = name;
            Description = description;
            Price = price;
            State = state;
        }

        public Service()
        {

        }
    }
}
