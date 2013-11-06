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
    class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Department_id;
        public int Registration_id;
        public String Date { get; set; }

        public Project(int Id, string Name, int Department_id, String Date)
        {

            this.Id = Id;
            this.Name = Name;
            this.Department_id = Department_id;
            this.Date = Date;
        }



        public Project(int Id, string Name, int Department_id, int registration_id, String Date)
        {

            this.Id = Id;
            this.Name = Name;
            this.Department_id = Department_id;
            this.Registration_id = Registration_id;
            this.Date = Date;
        }


    }
}
