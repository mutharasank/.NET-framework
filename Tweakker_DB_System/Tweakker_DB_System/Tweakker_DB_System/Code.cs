using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_DB_System
{
    class Code
    {
        public int mcc { get; set; }
        public int mnc { get; set; }
        public int network_id { get; set; }


        public Code(int mcc, int mnc, int network_id)
        {
            this.mcc = mcc;
            this.mnc = mnc;
            this.network_id = network_id;
        }

        public Code()
        { }


    }
}
