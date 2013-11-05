using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweakker_DB_System
{
    class BusinessService
    {
     
        private static DataAccess instance;
        public static DataAccess Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataAccess();

                return instance;
            }

            set { }
        }
        private ObservableCollection<Country> GetAllCountries()
        {
           return DataAccess.Instance.GetAllCountries();
        }
        private void SaveSetting_BookmarkParameters(int current_country_id, string network_name,  string mcc, string mnc, string ranking, int ismno, string setting_name, string setting_alternative_name, string bookmark_name, string bookmark_url, string bookmark_pin)
        {
            DataAccess.Instance.SaveSettings_BookmarkParameters(current_country_id, network_name, mcc, mnc, ranking, ismno, setting_name, setting_alternative_name, bookmark_name, bookmark_url, bookmark_pin);
           
        }


        //public void Insert()
        //{
        //    dbConnect.Insert();
        //}


        //public void Update()
        //{
        //    dbConnect.Update();
        //}


        //public void Delete()
        //{
        //    dbConnect.Delete();
        //}


        //public List<string>[] Select()
        //{
        //    return dbConnect.Select();

        //}


        //public int Count()
        //{
        //    return dbConnect.Count();
        //}


        //public void Backup()
        //{
        //    dbConnect.Backup();
        //}


        //public void Restore()
        //{
        //    dbConnect.Restore();
        //}
    }
}
