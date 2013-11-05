using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_DB_System
{
    class Setting
    {
        public int id { get; set; }
        public string name { get; set; }
        public string alternative_name { get; set; }
        public int network_id { get; set; }

        public Setting(int id, string name, string alternative_name, int network_id)
        {
            this.id = id;
            this.name = name;
            this.alternative_name = alternative_name;
            this.network_id = network_id;
        }

    }
}
