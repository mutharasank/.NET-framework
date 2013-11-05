﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tweakker_DB_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //    private BusinessService service;
        public ObservableCollection<String> tab_names = new ObservableCollection<String>();
        public ObservableCollection<TabItem> tabs = new ObservableCollection<TabItem>();
        ObservableCollection<Country> countries = new ObservableCollection<Country>();
        int current_country_id;
        Country country;

        public MainWindow()
        {
            InitializeComponent();
            FillComboBoxes();
        }
        /// <summary>
        /// Fills comboboxes controls with data from DB
        /// </summary>
        private void FillComboBoxes()
        {
            // Setting types logic 

            tab_names.Add(bookmark.Name);
            tab_names.Add(iap_mms.Name);
            tab_names.Add(iap_mms_streaming.Name);
            tab_names.Add(iapsetting.Name);
            tab_names.Add(mailsetting.Name);
            tab_names.Add(mmssetting.Name);
            tab_names.Add(singleapn_mms.Name);
            tab_names.Add(standard_settings.Name);
            tab_names.Add(streaming.Name);
            tab_names.Add(wap_iap.Name);
            tab_names.Add(wap_mms_iap.Name);
            tab_names.Add(wap_mms_streaming.Name);
            tab_names.Add(wapsetting.Name);

            tabs.Add(bookmark);
            tabs.Add(iap_mms);
            tabs.Add(iap_mms_streaming);
            tabs.Add(iapsetting);
            tabs.Add(mailsetting);
            tabs.Add(mmssetting);
            tabs.Add(singleapn_mms);
            tabs.Add(standard_settings);
            tabs.Add(streaming);
            tabs.Add(wap_iap);
            tabs.Add(wap_mms_iap);
            tabs.Add(wap_mms_streaming);
            tabs.Add(wapsetting);

            grid_parameters.Visibility = Visibility.Hidden;

            Debug.WriteLine(tab_names.Count);

            if (tab_names != null)
            {
                cbx_Type.ItemsSource = tab_names;
                cbx_Type.DataContext = tab_names;

            }
            else
            {
                MessageBox.Show("Error loading settings types");
            }

            // Countries logic

            countries = BusinessService.Instance.GetAllCountries();

            cbx_Countries.ItemsSource = countries;
            cbx_Countries.DataContext = countries;
            cbx_Countries.DisplayMemberPath = "name";

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            grid_parameters.Visibility = Visibility.Visible;


            foreach (TabItem t in tabs)
            {
                t.IsEnabled = false;

            }
            String selected_setting_type = "";
            if (cbx_Type.SelectedItem != null)
            {
                selected_setting_type = (String)cbx_Type.SelectedItem;

            }
            foreach (TabItem t in tabs)
            {
                if (t.Header.ToString().Equals(selected_setting_type))
                {
                    t.IsEnabled = true;
                    t.Focus();

                }

            }
        }

        private void btn_Save_bookmark_Click_1(object sender, RoutedEventArgs e)
        {

            if (country != null)
            {
                String setting_name = txt_setting_name.Text;
                String setting_alternative_name = txt_Alternative_name.Text;

                String network_name = txt_network_name.Text;
                String mcc = txt_mcc.Text;
                String mnc = txt_mnc.Text;
                String ranking = txb_Ranking.Text;
                String bookmark_name = txt_bookmark_name.Text;
                String bookmark_url = txt_bookmark_url.Text;
                String bookmark_pin = txt_bookmark_pin.Text;


                int ismno;
                if (checkbox_isMNO.IsChecked == true)
                {

                    ismno = 1;
                }
                else
                {
                    ismno = 0;
                }
                try
                {
                    BusinessService.Instance.SaveSettings_BookmarkParameters(current_country_id, network_name, mcc, mnc, ranking, ismno, setting_name, setting_alternative_name, bookmark_name, bookmark_url, bookmark_pin);
                    //   MessageBox.Show(current_country_id + " " + network_name + "  " + mcc + " " + mnc + " " + ranking + " " + ismno + " " + setting_name + " " + setting_alternative_name + " " + bookmark_name + " " + bookmark_url + " " + bookmark_pin);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving Setting with bookmark parameters : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }

        }

        private void cbx_Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbx_Countries.SelectedItem != null)
            {
                country = (Country)cbx_Countries.SelectedItem;
                txt_ISO.Text = country.iso;
                current_country_id = country.id;

            }

        }

        private void checkbox_isMNO_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void txt_Alternative_name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Save_iapmms_setting_Click_1(object sender, RoutedEventArgs e)
        {
            if (country != null)
            {
                String setting_name = txt_setting_name.Text;
                String setting_alternative_name = txt_Alternative_name.Text;
                String network_name = txt_network_name.Text;
                String mcc = txt_mcc.Text;
                String mnc = txt_mnc.Text;
                String ranking = txb_Ranking.Text;
               
                String iap_gprs_access_point_name = txt_iap_gprs_access_point_name.Text;
                String iap_gprs_auth_name = txt_iap_gprs_authname.Text;
                String iap_gprs_auth_secret = txt_iap_gprs_auth_secret.Text;
                String iap_gprs_auth_type = txt_iap_gprs_auth_type.Text;
                String iap_gprs_name = txt_iap_gprs_name.Text;
                String iap_gprs_proxy_port = txt_iap_gprs_proxy_port.Text;
                String iap_gprs_url = txt_iap_gprs_url.Text;
                String iap_mms_gprs_bootstrap_name = txt_iap_mms_gprs_bootstrap_name.Text;
                String mms_gprs_access_point_name = txt_mms_gprs_access_point_name.Text;
                String mms_gprs_auth_name = txt_mms_gprs_auth_name.Text;
                String mms_gprs_auth_secret = txt_mms_gprs_auth_secret.Text;
                String mms_gprs_auth_type = txt_mms_gprs_auth_type.Text;
                String mms_gprs_name = txt_mms_gprs_name.Text;
                String mms_gprs_proxy = txt_mms_gprs_proxy.Text;
                String mms_gprs_proxy_port = txt_mms_gprs_proxy_port.Text;
                String mms_gprs_url = txt_mms_gprs_url.Text;
                String pin = txt_pin.Text;
                String protocol = txt_protocol.Text;
                String security_method = txt_security_method.Text;
    

                int ismno;
                if (checkbox_isMNO.IsChecked == true)
                {

                    ismno = 1;
                }
                else
                {
                    ismno = 0;
                }
                try
                {
                    Debug.WriteLine( iap_gprs_access_point_name+ "item"+iap_gprs_auth_name+ "item"+iap_gprs_auth_secret +"item"+
                    iap_gprs_auth_type+ "item"+iap_gprs_name+ "item"+iap_gprs_proxy_port+ "item"+iap_gprs_url+"item"+
                    iap_mms_gprs_bootstrap_name+ "item"+mms_gprs_access_point_name+ "item"+mms_gprs_auth_name+"item"+
      mms_gprs_auth_secret + "item" + mms_gprs_auth_type + "item" + mms_gprs_name + "item" + mms_gprs_proxy + "item" +
             mms_gprs_proxy_port+ "item"+mms_gprs_url+ "item"+pin+ "item"+protocol+ "item"+security_method);

                    BusinessService.Instance.SaveSettings_iapmms(current_country_id, network_name, mcc, mnc, ranking, ismno, setting_name, setting_alternative_name,
                        iap_gprs_access_point_name, iap_gprs_auth_name, iap_gprs_auth_secret, iap_gprs_auth_type,
                iap_gprs_name, iap_gprs_proxy_port, iap_gprs_url, iap_mms_gprs_bootstrap_name, mms_gprs_access_point_name, mms_gprs_auth_name,
                mms_gprs_auth_secret, mms_gprs_auth_type, mms_gprs_name, mms_gprs_proxy, mms_gprs_proxy_port, mms_gprs_url, pin, protocol, security_method);




                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving Setting with bookmark parameters : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
        }


    }
}
