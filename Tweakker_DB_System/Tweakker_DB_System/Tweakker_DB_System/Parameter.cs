using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_DB_System
{
    class Parameter
    {
        public int setting_id { get; set; }
        public string parameter_name { get; set; }
        public string parameter_value{get;set;}

        public Parameter(int setting_id, string parameter_name, string parameter_value)
        {
            this.setting_id = setting_id;
            this.parameter_name = parameter_name;
            this.parameter_value = parameter_value;
        }
    }
}
