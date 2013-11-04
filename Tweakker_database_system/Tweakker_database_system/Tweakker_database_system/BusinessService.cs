using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_database_system
{
   public class BusinessService
    {
        private DataAccess dbConnect;

        public void Insert()
        {
            dbConnect.Insert();
        }


        public void Update()
        {
            dbConnect.Update();
        }


        public void Delete()
        {
            dbConnect.Delete();
        }


        public List<string>[] Select()
        {
        return dbConnect.Select();
            
        }


        public int Count()
        {
         return dbConnect.Count();
        }


        public void Backup()
        {
            dbConnect.Backup();
        }


        public void Restore()
        {
            dbConnect.Restore();
        }
    }
}
