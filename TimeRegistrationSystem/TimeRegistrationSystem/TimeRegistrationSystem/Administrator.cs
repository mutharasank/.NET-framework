using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRegistrationSystem
{
    class Administrator
    {
        public int id;
        public string Name { get; set; }
        public string Password { get; set; }

        public Administrator(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }
   }
}
