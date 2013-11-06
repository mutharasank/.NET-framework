using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRegistrationSystem
{
    class Employee
    {
        public int id { get; set; }
        public string Name { get; set; }
        private double Cpr { get; set; }
        private int department_id;
        private int administrator_id;

        public Employee(int id, string name, double cpr, int department_id, int administrator_id)
        {
            this.Name = name;
            this.Cpr = cpr;
            this.department_id = department_id;
            this.id = id;
            this.administrator_id = administrator_id;
        }
        public Employee(string name)
        {
            this.Name = name;
        }
    }
}
