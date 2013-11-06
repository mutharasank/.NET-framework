using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_DB_System
{
    class Network
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool ismno { get; set; }
        public int ranking { get; set; }
        public int country_id { get; set; }
   
        public Network(int id, string name, bool ismno, int ranking, int country_id)
        {
            this.id = id;
            this.name = name;
            this.ismno = ismno;
            this.ranking = ranking;
            this.country_id = country_id;

        }
    }
}
