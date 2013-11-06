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
    public class Department
    {
        public int id { get; set; }
        public string name{get;set;}
      
        public Department(int id,string name)
        {
            this.id = id;
            this.name = name;
        }
      
    }
}
