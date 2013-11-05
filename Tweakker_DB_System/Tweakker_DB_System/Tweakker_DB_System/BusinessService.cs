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
     
        private static BusinessService instance;
        public static BusinessService Instance
        {
            get
            {
                if (instance == null)
                    instance = new BusinessService();

                return instance;
            }

            set { }
        }
        public ObservableCollection<Country> GetAllCountries()
        {
           return DataAccess.Instance.GetAllCountries();
        }
        public ObservableCollection<Network> GetNetworksByCountryID(int p)
        {
            return DataAccess.Instance.GetNetworksByCountryID(p);
        }

        public ObservableCollection<Parameter> GetParametersBySettingID(int p)
        {
            return DataAccess.Instance.GetParametersBySettingID(p);
        }
   
           public ObservableCollection<Setting> GetSettingsByNetworkID(int p)
        {
            return DataAccess.Instance.GetSettingsByNetworkID(p);

        }


           public void UpdateSetting(Country c, Network n, Setting s)
           {
               DataAccess.Instance.UpdateSetting(c, n, s);
           }
           public void PopulateDataGrid(System.Windows.Controls.DataGrid datagrid_parameters, int p)
           {
               DataAccess.Instance.PopulateDataGrid(datagrid_parameters, p);
           }
           public void UpdateDataGridValues()
           {
               DataAccess.Instance.UpdateDataGridValues();
           }

           public void SaveSetting_BookmarkParameters(int current_country_id, string network_name, string mcc, string mnc, string ranking, int ismno, string setting_name, string setting_alternative_name, string bookmark_name, string bookmark_url, string bookmark_pin)
           {
               DataAccess.Instance.SaveSettings_BookmarkParameters(current_country_id, network_name, mcc, mnc, ranking, ismno, setting_name, setting_alternative_name, bookmark_name, bookmark_url, bookmark_pin);

           }
           public void SaveSettings_iapmms(int current_country_id, string network_name, string mcc, string mnc, string ranking, int ismno, string setting_name, string setting_alternative_name, string iap_gprs_access_point_name, string iap_gprs_auth_name, string iap_gprs_auth_secret, string iap_gprs_auth_type, string iap_gprs_name, string iap_gprs_proxy_port, string iap_gprs_url, string iap_mms_gprs_bootstrap_name, string mms_gprs_access_point_name, string mms_gprs_auth_name, string mms_gprs_auth_secret, string mms_gprs_auth_type, string mms_gprs_name, string mms_gprs_proxy, string mms_gprs_proxy_port, string mms_gprs_url, string pin, string protocol, string security_method)
           {
               DataAccess.Instance.SaveSettings_iapmms(current_country_id, network_name, mcc, mnc, ranking, ismno, setting_name, setting_alternative_name,
                           iap_gprs_access_point_name, iap_gprs_auth_name, iap_gprs_auth_secret, iap_gprs_auth_type,
                   iap_gprs_name, iap_gprs_proxy_port, iap_gprs_url, iap_mms_gprs_bootstrap_name, mms_gprs_access_point_name, mms_gprs_auth_name,
                   mms_gprs_auth_secret, mms_gprs_auth_type, mms_gprs_name, mms_gprs_proxy, mms_gprs_proxy_port, mms_gprs_url, pin, protocol, security_method);
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
