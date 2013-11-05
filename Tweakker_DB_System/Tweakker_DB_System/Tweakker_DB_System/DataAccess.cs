using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add MySql Library
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data;

namespace Tweakker_DB_System
{
    class DataAccess
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string sql;
        private MySqlCommand cmd;
        DataSet ds = null;
        DataTable dt = null;
        MySqlDataAdapter dataAdapter = null;
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
        //Constructor
        public DataAccess()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "tweakker_database";
            uid = "root";
            password = "pass";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public ObservableCollection<Country> GetAllCountries()
        {
            ObservableCollection<Country> countries = new ObservableCollection<Country>();


            sql = "SELECT * FROM country";

            try
            {
                cmd = new MySqlCommand(sql, connection);
                // open the connection
                OpenConnection();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    int id = (int)reader[0];
                    string iso = (string)reader[1];
                    string name = (string)reader[2];


                    Country c = new Country(id, iso, name);
                    Debug.WriteLine(id + iso + name);
                    countries.Add(c);



                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading all countries" + ex.Message);
            }
            finally
            {
                connection.Close();

                // close the reader

            }
            return countries;
        }


        public ObservableCollection<Setting> GetSettingsByNetworkID(int p)
        {
            ObservableCollection<Setting> settings = new ObservableCollection<Setting>();


            sql = "SELECT * FROM setting WHERE network_id = " + "'" + p + "';";
            try
            {
                cmd = new MySqlCommand(sql, connection);
                // open the connection
                OpenConnection();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    int id = (int)reader[0];
                    string name = (string)reader[1];
                    string alternative_name = (string)reader[2];

                    int network_id = (int)reader[3];


                    Setting setting = new Setting(id, name, alternative_name, network_id);

                    settings.Add(setting);



                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings by network_id" + ex.Message);
            }
            finally
            {
                connection.Close();

                // close the reader

            }
            return settings;

        }
        public ObservableCollection<Network> GetNetworksByCountryID(int p)
        {
            ObservableCollection<Network> networks = new ObservableCollection<Network>();


            sql = "SELECT * FROM network WHERE country_id = " + "'" + p + "';";

            try
            {
                cmd = new MySqlCommand(sql, connection);
                // open the connection
                OpenConnection();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    int id = (int)reader[0];
                    string name = (string)reader[1];
                    int ranking = (int)reader[2];
                    bool ismno = (bool)reader[3];
                    int country_id = (int)reader[4];

                    // MessageBox.Show(id + name + ranking + ismno + country_id);
                    Network network = new Network(id, name, ismno, ranking, country_id);

                    networks.Add(network);



                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading networks by country_id" + ex.Message);
            }
            finally
            {
                connection.Close();

                // close the reader

            }
            return networks;
        }

        public ObservableCollection<Parameter> GetParametersBySettingID(int p)
        {
            ObservableCollection<Parameter> parameters = new ObservableCollection<Parameter>();

            sql = "SELECT * FROM setting_parameters WHERE setting_id = " + "'" + p + "';";
            try
            {
                cmd = new MySqlCommand(sql, connection);
                // open the connection
                OpenConnection();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {


                    string parameter_name = (string)reader[0];
                    string parameter_value = (string)reader[1];
                    int setting_id = (int)reader[2];

                    Parameter parameter = new Parameter(setting_id, parameter_name, parameter_value);

                    parameters.Add(parameter);



                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading networks by country_id" + ex.Message);
            }
            finally
            {
                connection.Close();

                // close the reader

            }
            return parameters;

        }



        public void UpdateSetting(Country c, Network n, Setting s)
        {
            ObservableCollection<Parameter> parameters = new ObservableCollection<Parameter>();
            OpenConnection();
            MySqlCommand myCommand = connection.CreateCommand();
            MySqlTransaction myTrans;
            myTrans = connection.BeginTransaction();
            //           BEGIN;
            //UPDATE country SET iso =c.iso WHERE country_id = c.id;
            //UPDATE network SET name = n.name, ranking = n.ranking, ismno=n.ismno WHERE country_id = c.id;
            //UPDATE setting SET name = s.name, alternative_name = s.alternative_name WHERE network_id = n.id;
            //COMMIT;
            try
            {

                // Start a local transaction

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                myCommand.Connection = connection;
                myCommand.Transaction = myTrans;

                myCommand.CommandText = "UPDATE country SET iso_code =@iso WHERE country_id = @country_id;";
                myCommand.Parameters.AddWithValue("@iso", c.iso);
                myCommand.Parameters.AddWithValue("@country_id", c.id);
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "UPDATE network SET name = @name, ranking = @ranking, ismno=@ismno WHERE country_id = @c_id;";
                myCommand.Parameters.AddWithValue("@name", n.name);
                myCommand.Parameters.AddWithValue("@ranking", n.ranking);
                myCommand.Parameters.AddWithValue("@ismno", n.ismno);
                myCommand.Parameters.AddWithValue("@c_id", c.id);
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "UPDATE setting SET name = @setting_name, alternative_name = @setting_alt_name WHERE network_id = @network_id";
                Debug.WriteLine(myCommand.Parameters.Count);
                myCommand.Parameters.AddWithValue("@setting_name", s.name);
                myCommand.Parameters.AddWithValue("@setting_alt_name", s.alternative_name);
                myCommand.Parameters.AddWithValue("@network_id", n.id);
                int id = Convert.ToInt32(myCommand.ExecuteScalar());



                myCommand.ExecuteNonQuery();
                myTrans.Commit();

                MessageBox.Show("Record Updated");
            }


            catch (Exception ex1)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                }

                Console.WriteLine("An exception of type " + ex1.GetType() +
                                  " was encountered while inserting the data.");
                Console.WriteLine("Neither record was written to database.");
            }


            finally
            {
                CloseConnection();

            }

        }


        public void PopulateDataGrid(System.Windows.Controls.DataGrid datagrid_parameters, int p)
        {

            OpenConnection();

            sql = "SELECT * FROM setting_parameters WHERE setting_id = " + "'" + p + "';";

            ds = new DataSet();
            dataAdapter = new MySqlDataAdapter(sql, connection);
            dataAdapter.Fill(ds, "setting_parameters");
            dt = ds.Tables["setting_parameters"];
            datagrid_parameters.DataContext = dt.DefaultView;
            CloseConnection();
        }

        public void UpdateDataGridValues()
        {

            MySqlCommandBuilder catCB = new MySqlCommandBuilder(dataAdapter);
            dataAdapter.UpdateCommand = catCB.GetUpdateCommand();
            dataAdapter.DeleteCommand = catCB.GetDeleteCommand();
            dataAdapter.InsertCommand = catCB.GetInsertCommand();
            dataAdapter.Update(dt);
        }


        public void SaveSettings_BookmarkParameters(int current_country_id, string network_name, string mcc, string mnc, string ranking, int ismno, string setting_name, string setting_alternative_name, string bookmark_name, string bookmark_url, string bookmark_pin)
        {

            if (network_name != null && mcc != null && mnc != null & ranking != null && setting_name != null && setting_alternative_name != null)
            {

                connection.Open();
                MySqlCommand myCommand = connection.CreateCommand();
                MySqlTransaction myTrans;
                // Start a local transaction
                myTrans = connection.BeginTransaction();
                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                myCommand.Connection = connection;
                myCommand.Transaction = myTrans;

                try
                {

                    myCommand.CommandText = "INSERT INTO network (name,ranking,isMNO,country_id) VALUES (@network_name,@ranking,@ismno,@current_country_id );SELECT LAST_INSERT_ID();";
                    myCommand.Parameters.AddWithValue("@network_name", network_name);
                    myCommand.Parameters.AddWithValue("@ranking", ranking);
                    myCommand.Parameters.AddWithValue("@ismno", ismno);
                    myCommand.Parameters.AddWithValue("@current_country_id", current_country_id);
                    int i = Convert.ToInt32(myCommand.ExecuteScalar());

                    myCommand.CommandText = "INSERT INTO code (mcc, mnc, network_id) VALUES (@mcc,@mnc,@network_id);";
                    myCommand.Parameters.AddWithValue("@mcc", mcc);
                    myCommand.Parameters.AddWithValue("@mnc", mnc);
                    myCommand.Parameters.AddWithValue("@network_id", i);
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = "INSERT INTO setting(name,alternative_name,network_id) VALUES (@name,@alternative_name,@i );SELECT LAST_INSERT_ID();";
                    Debug.WriteLine(myCommand.Parameters.Count);
                    myCommand.Parameters.AddWithValue("@name", setting_name);
                    myCommand.Parameters.AddWithValue("@alternative_name", setting_alternative_name);
                    myCommand.Parameters.AddWithValue("@i", i);
                    int id = Convert.ToInt32(myCommand.ExecuteScalar());

                    myCommand.CommandText = "INSERT INTO setting_parameters(parameter_name,parameter_value,setting_id) VALUES (@bookmark_name,@value,@id),(@bookmark_url,@url,@id1),(@bookmark_pin,@pin,@id2);";
                    myCommand.Parameters.AddWithValue("@bookmark_name", "Bookmark Name");
                    myCommand.Parameters.AddWithValue("@value", bookmark_name);
                    myCommand.Parameters.AddWithValue("@id", id);


                    myCommand.Parameters.AddWithValue("@bookmark_url", "Bookmark URL");
                    myCommand.Parameters.AddWithValue("url", bookmark_url);
                    myCommand.Parameters.AddWithValue("@id1", id);

                    myCommand.Parameters.AddWithValue("@bookmark_pin", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@pin", bookmark_pin);
                    myCommand.Parameters.AddWithValue("@id2", id);

                    myCommand.ExecuteNonQuery();
                    myTrans.Commit();
                    MessageBox.Show("Setting is saved to database!");

                }
                catch (Exception e)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (MySqlException ex)
                    {
                        if (myTrans.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + ex.GetType() +
                                              " was encountered while attempting to roll back the transaction.");
                        }
                    }

                    Console.WriteLine("An exception of type " + e.GetType() +
                                      " was encountered while inserting the data.");
                    Console.WriteLine("Neither record was written to database.");
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please fill all fields");
            }
        }

        public void SaveSettings_iapmms(int current_country_id, string network_name, string mcc, string mnc,
            string ranking, int ismno, string setting_name, string setting_alternative_name,
            string iap_gprs_access_point_name, string iap_gprs_auth_name, string iap_gprs_auth_secret,
            string iap_gprs_auth_type, string iap_gprs_name, string iap_gprs_proxy_port, string iap_gprs_url,
            string iap_mms_gprs_bootstrap_name, string mms_gprs_access_point_name, string mms_gprs_auth_name,
            string mms_gprs_auth_secret, string mms_gprs_auth_type, string mms_gprs_name, string mms_gprs_proxy,
            string mms_gprs_proxy_port, string mms_gprs_url, string pin, string protocol, string security_method)
        {

            //            Debug.WriteLine(iap_gprs_access_point_name + "item" + iap_gprs_auth_name + "item" + iap_gprs_auth_secret + "item" +
            //            iap_gprs_auth_type + "item" + iap_gprs_name + "item" + iap_gprs_proxy_port + "item" + iap_gprs_url + "item" +
            //            iap_mms_gprs_bootstrap_name + "item" + mms_gprs_access_point_name + "item" + mms_gprs_auth_name + "item" +
            //mms_gprs_auth_secret + "item" + mms_gprs_auth_type + "item" + mms_gprs_name + "item" + mms_gprs_proxy + "item" +
            //     mms_gprs_proxy_port + "item" + mms_gprs_url + "item" + pin + "item" + protocol + "item" + security_method);

            if (network_name != null && mcc != null && mnc != null & ranking != null && setting_name != null && setting_alternative_name != null)
            {
                connection.Open();
                MySqlCommand myCommand = connection.CreateCommand();
                MySqlTransaction myTrans;
                // Start a local transaction
                myTrans = connection.BeginTransaction();
                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                myCommand.Connection = connection;
                myCommand.Transaction = myTrans;

                try
                {
                    myCommand.CommandText = "INSERT INTO network (name,ranking,isMNO,country_id) VALUES (@network_name,@ranking,@ismno,@current_country_id );SELECT LAST_INSERT_ID();";
                    myCommand.Parameters.AddWithValue("@network_name", network_name);
                    myCommand.Parameters.AddWithValue("@ranking", ranking);
                    myCommand.Parameters.AddWithValue("@ismno", ismno);
                    myCommand.Parameters.AddWithValue("@current_country_id", current_country_id);
                    int i = Convert.ToInt32(myCommand.ExecuteScalar());

                    myCommand.CommandText = "INSERT INTO code (mcc, mnc, network_id) VALUES (@mcc,@mnc,@network_id);";
                    myCommand.Parameters.AddWithValue("@mcc", mcc);
                    myCommand.Parameters.AddWithValue("@mnc", mnc);
                    myCommand.Parameters.AddWithValue("@network_id", i);
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = "INSERT INTO setting(name,alternative_name,network_id) VALUES (@name,@alternative_name,@i );SELECT LAST_INSERT_ID();";
                    Debug.WriteLine(myCommand.Parameters.Count);
                    myCommand.Parameters.AddWithValue("@name", setting_name);
                    myCommand.Parameters.AddWithValue("@alternative_name", setting_alternative_name);
                    myCommand.Parameters.AddWithValue("@i", i);
                    int id = Convert.ToInt32(myCommand.ExecuteScalar());


                    myCommand.CommandText = "INSERT INTO setting_parameters(parameter_name,parameter_value,setting_id) VALUES (@iamms_iap_gprs_access_point_name,@value,@id),(@iap_mms_iap_gprs_auth_name,@value1,@id1),(@iap_mms_iap_gprs_auth_secret,@value2,@id2),(@iap_mms_iap_gprs_auth_type,@value3,@id3),(@iap_mms_iap_gprs_name,@value4,@id4),(@iap_mms_iap_gprs_proxy_port,@value5,@id5),(@iap_mms_iap_gprs_url,@value6,@id6),(@iap_mms_iap_mms_gprs_bootstrap_name,@value7,@id7),(@iap_mms_mms_gprs_access_point_name,@value8,@id8),(@iap_mms_mms_gprs_auth_name,@value9,@id9),(@iap_mms_mms_gprs_auth_secret,@value10,@id10),(@iap_mms_mms_gprs_auth_type,@value11,@id11),(@iap_mms_mms_gprs_name,@value12,@id12),(@iap_mms_mms_gprs_proxy,@value13,@id13),(@iap_mms_mms_gprs_proxy_port,@value14,@id14),(@iap_mms_mms_gprs_url,@value15,@id15);";

                    myCommand.Parameters.AddWithValue("@iap_gprs_access_point_name", "iap_gprs_access_point_name");
                    myCommand.Parameters.AddWithValue("@value", iap_gprs_access_point_name);
                    myCommand.Parameters.AddWithValue("@id", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_iap_gprs_auth_name", "Bookmark URL");
                    myCommand.Parameters.AddWithValue("value1", iap_gprs_auth_name);
                    myCommand.Parameters.AddWithValue("@id1", id);

                    myCommand.Parameters.AddWithValue("@iap_mms_iap_gprs_auth_secret", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value2", iap_gprs_auth_secret);
                    myCommand.Parameters.AddWithValue("@id2", id);

                    myCommand.Parameters.AddWithValue("@iap_mms_iap_gprs_auth_type", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value3", iap_gprs_auth_type);
                    myCommand.Parameters.AddWithValue("@id3", id);

                    myCommand.Parameters.AddWithValue("@iap_mms_iap_gprs_name", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value4", iap_gprs_name);
                    myCommand.Parameters.AddWithValue("@id4", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_iap_gprs_proxy_port", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value5", iap_gprs_proxy_port);
                    myCommand.Parameters.AddWithValue("@id5", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_iap_gprs_url", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value6", iap_gprs_url);
                    myCommand.Parameters.AddWithValue("@id6", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_iap_mms_gprs_bootstrap_name", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value7", iap_mms_gprs_bootstrap_name);
                    myCommand.Parameters.AddWithValue("@id7", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_access_point_name", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value8", mms_gprs_access_point_name);
                    myCommand.Parameters.AddWithValue("@id8", id);

                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_auth_name", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value9", mms_gprs_auth_name);
                    myCommand.Parameters.AddWithValue("@id9", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_auth_secret", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value10", mms_gprs_auth_secret);
                    myCommand.Parameters.AddWithValue("@id10", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_auth_type", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value11", mms_gprs_auth_type);
                    myCommand.Parameters.AddWithValue("@id11", id);


                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_name", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value12", mms_gprs_name);
                    myCommand.Parameters.AddWithValue("@id12", id);

                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_proxy", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value13", mms_gprs_proxy);
                    myCommand.Parameters.AddWithValue("@id13", id);

                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_proxy_port", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value14", mms_gprs_url);
                    myCommand.Parameters.AddWithValue("@id14", id);

                    myCommand.Parameters.AddWithValue("@iap_mms_mms_gprs_url", "Bookmark PIN");
                    myCommand.Parameters.AddWithValue("@value15", mms_gprs_proxy_port);
                    myCommand.Parameters.AddWithValue("@id15", id);

                    //myCommand.Parameters.AddWithValue("@iap_mms_pin", "Bookmark PIN");
                    //myCommand.Parameters.AddWithValue("@value16", pin);
                    //myCommand.Parameters.AddWithValue("@id16", id);

                    //myCommand.Parameters.AddWithValue("@iap_mms_protocol", "Bookmark PIN");
                    //myCommand.Parameters.AddWithValue("@value17", protocol);
                    //myCommand.Parameters.AddWithValue("@id17", id);

                    //myCommand.Parameters.AddWithValue("@iap_mms_security", "Bookmark PIN");
                    //myCommand.Parameters.AddWithValue("@value18", security_method);
                    //myCommand.Parameters.AddWithValue("@id18", id);

                    myCommand.ExecuteNonQuery();
                    myTrans.Commit();
                    MessageBox.Show("Setting is saved to database!");

                }
                catch (Exception e)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (MySqlException ex)
                    {
                        if (myTrans.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + ex.GetType() +
                                              " was encountered while attempting to roll back the transaction.");
                        }
                    }

                    Console.WriteLine("An exception of type " + e.GetType() +
                                      " was encountered while inserting the data.");
                    Console.WriteLine("Neither record was written to database.");
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please fill all fields");
            }
        }



        //Count statement
        //public int Count()
        //{
        //    string query = "SELECT Count(*) FROM tableinfo";
        //    int Count = -1;

        //    Open Connection
        //    if (this.OpenConnection() == true)
        //    {
        //        Create Mysql Command
        //        MySqlCommand cmd = new MySqlCommand(query, connection);

        //        ExecuteScalar will return one value
        //        Count = int.Parse(cmd.ExecuteScalar() + "");

        //        close Connection
        //        this.CloseConnection();

        //        return Count;
        //    }
        //    else
        //    {
        //        return Count;
        //    }
        //}

        //Backup
        //public void Backup()
        //{
        //    try
        //    {
        //        DateTime Time = DateTime.Now;
        //        int year = Time.Year;
        //        int month = Time.Month;
        //        int day = Time.Day;
        //        int hour = Time.Hour;
        //        int minute = Time.Minute;
        //        int second = Time.Second;
        //        int millisecond = Time.Millisecond;

        //        //Save file to C:\ with the current date as a filename
        //        string path;
        //        path = "C:\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
        //        StreamWriter file = new StreamWriter(path);


        //        ProcessStartInfo psi = new ProcessStartInfo();
        //        psi.FileName = "mysqldump";
        //        psi.RedirectStandardInput = false;
        //        psi.RedirectStandardOutput = true;
        //        psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
        //        psi.UseShellExecute = false;

        //        Process process = Process.Start(psi);

        //        string output;
        //        output = process.StandardOutput.ReadToEnd();
        //        file.WriteLine(output);
        //        process.WaitForExit();
        //        file.Close();
        //        process.Close();
        //    }
        //    catch (IOException ex)
        //    {
        //        MessageBox.Show("Error , unable to backup!");
        //    }
        //}

        //Restore
        //public void Restore()
        //{
        //    try
        //    {
        //        //Read file from C:\
        //        string path;
        //        path = "C:\\MySqlBackup.sql";
        //        StreamReader file = new StreamReader(path);
        //        string input = file.ReadToEnd();
        //        file.Close();


        //        ProcessStartInfo psi = new ProcessStartInfo();
        //        psi.FileName = "mysql";
        //        psi.RedirectStandardInput = true;
        //        psi.RedirectStandardOutput = false;
        //        psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
        //        psi.UseShellExecute = false;


        //        Process process = Process.Start(psi);
        //        process.StandardInput.WriteLine(input);
        //        process.StandardInput.Close();
        //        process.WaitForExit();
        //        process.Close();
        //    }
        //    catch (IOException ex)
        //    {
        //        MessageBox.Show("Error , unable to Restore!");
        //    }
        //}

    }
}
