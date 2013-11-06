using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRegistrationSystem
{
    class AssignedRegistration
    {
        public int Project_id { get; set; }
        public int Employee_id { get; set; }
        public DateTime Date { get; set; }
        public float Hours { get; set; }

        public AssignedRegistration(int Project_id, int Employee_id, DateTime Date, float Hours)
        {
            this.Project_id = Project_id;
            this.Employee_id = Employee_id;
            this.Hours = Hours;
            this.Date = Date;
        }
    }
}
