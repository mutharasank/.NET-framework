using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_DB_System
{
    class Note
    {
        public int id { get; set; }
        public int setting_id { get; set; }
        public string text { get; set; }

        public Note(int id, int setting_id, string text)
        {
            this.id = id;
            this.setting_id = setting_id;
            this.text = text;
        }
        public Note()
        { }
    }
}
