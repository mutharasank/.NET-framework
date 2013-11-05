using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_DB_System
{
    class Country
    {
        public int id {get;set;}
         public String name { get; set; }
        public String iso { get; set; }
      

        public Country(int id, string iso, string name)
        {
            this.id = id;
           
            this.iso = iso;
            this.name = name;
            
        }
   
    }
}
