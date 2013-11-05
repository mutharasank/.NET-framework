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

        //Insert statement
        //public void Insert()
        //{
        //    string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

        //    //open connection
        //    if (this.OpenConnection() == true)
        //    {
        //        //create command and assign the query and connection from the constructor
        //        MySqlCommand cmd = new MySqlCommand(query, connection);

        //        //Execute command
        //        cmd.ExecuteNonQuery();

        //        //close connection
        //        this.CloseConnection();
        //    }
        //}

        //Update statement
        //public void Update()
        //{
        //    string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

        //    //Open connection
        //    if (this.OpenConnection() == true)
        //    {
        //        //create mysql command
        //        MySqlCommand cmd = new MySqlCommand();
        //        //Assign the query using CommandText
        //        cmd.CommandText = query;
        //        //Assign the connection using Connection
        //        cmd.Connection = connection;

        //        //Execute query
        //        cmd.ExecuteNonQuery();

        //        //close connection
        //        this.CloseConnection();
        //    }
        //}

        //Delete statement
        //public void Delete()
        //{
        //    string query = "DELETE FROM tableinfo WHERE name='John Smith'";

        //    if (this.OpenConnection() == true)
        //    {
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        cmd.ExecuteNonQuery();
        //        this.CloseConnection();
        //    }
        //}

        ////Select statement
        //public List<string>[] Select()
        //{
        //    string query = "SELECT * FROM tableinfo";

        //    //Create a list to store the result
        //    List<string>[] list = new List<string>[3];
        //    list[0] = new List<string>();
        //    list[1] = new List<string>();
        //    list[2] = new List<string>();

        //    //Open connection
        //    if (this.OpenConnection() == true)
        //    {
        //        //Create Command
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        //Create a data reader and Execute the command
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {
        //            list[0].Add(dataReader["id"] + "");
        //            list[1].Add(dataReader["name"] + "");
        //            list[2].Add(dataReader["age"] + "");
        //        }

        //        //close Data Reader
        //        dataReader.Close();

        //        //close Connection
        //        this.CloseConnection();

        //        //return list to be displayed
        //        return list;
        //    }
        //    else
        //    {
        //        return list;
        //    }
        //}

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
