using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTemplateDemo1
{
    public class River
    {
        string name;
        int milesLong;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int MilesLong
        {
            get { return milesLong; }
            set { milesLong = value; }
        }
    }

}
