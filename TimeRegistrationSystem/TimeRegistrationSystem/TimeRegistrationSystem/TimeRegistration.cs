using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRegistrationSystem
{
    class TimeRegistration
    {
        public int employee_id { get; set; }
        public int project_id { get; set; }
        public String date { get; set; }

        public double registered_hours { get; set; }

        public TimeRegistration(int employee_id, int project_id, string date, double registered_hours)
        {
            this.employee_id = employee_id;
            this.project_id = project_id;
            this.date = date;

            this.registered_hours = registered_hours;
        }

    }
}
