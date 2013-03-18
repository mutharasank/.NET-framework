using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrowser
{
    public class Staff
    {
        public String Filename { get; set; }

        public String Initials { get; set; }
        public String Fullname { get; set; }

     
        public Staff()
        { }

        public Staff(String Filename,String Fullname,String Initials)
        {
            this.Filename = Filename;
            this.Fullname = Fullname;
            this.Initials = Initials;

        }
    }


}
