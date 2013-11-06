using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
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

namespace GradesAdministrationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///     /// </summary>
    public partial class MainWindow : Window
    {
        List<String> educations = new List<String>();
        List<String> exams = new List<String>();
        DataSet ds = null;
        DataTable dt = null;
        SqlDataAdapter dataAdapter = null;
        String date, selected, selectedEducation;

        //connection string
        string connStr = @"Data Source=localhost\SQLEXPRESS; database=GradesSystem;Integrated Security=True";
        SqlConnection con;
        string sql;
        SqlCommand cmd;

        long numVal;

        public MainWindow()
        {
            InitializeComponent();
            fillComboboxes();
            con = new SqlConnection(connStr);
            btnValidate.IsEnabled = false;
            btnSaveToDatabase.IsEnabled = false;
            fill();

        }
        //fills comboboxes with Strings
        private void fillComboboxes()
        {
            educations.Add("CS");
            educations.Add("Multimedia");
            educations.Add("Business");
            educations.Add("Marketing");
            educations.Add("Architecture");
            educations.Add("Law");
            educations.Add("Automotive development");
            educations.Add("Marketing");

            exams.Add("ITO exam");
            exams.Add("SK exam");
            exams.Add("SD exam");
            exams.Add("Law exam");
            exams.Add("Multimedia exam");
            exams.Add("Architecture exam");
            exams.Add("Marketing exam");

            cbxEducation.ItemsSource = educations;
            cbxChangeEducation.ItemsSource = educations;
            cbxAll.ItemsSource = exams;
            cbxAttempts.ItemsSource = exams;
            cbxExemptions.ItemsSource = exams;
        }

        //saves the student into the database
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string cpr = txbCPR.Text.ToString();
            string name = txtName.Text.ToString();
            //Validation of input
            try
            {
                numVal = Convert.ToInt64(cpr);
                string selectedEmployee = cbxEducation.SelectedItem.ToString();
                name.Equals(null);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please make sure you : " + "\n" + "1. Put number for CPR" + "\n"
                    + "2.Text for Name" + "\n" + "3.Selected Education" + "\n" + "error message : " + ex.Message);
            }
            {
                selectedEducation = cbxEducation.SelectedItem.ToString();
                String values = "(" + numVal + "," + name + "," + selectedEducation + "," + "'" + date + "'" + ")";
                //saves into the Database
                try
                {
                    sql = "INSERT INTO Students VALUES" + " " + values;
                    sql = "INSERT INTO Students ";
                    sql += "(Cpr,Name,Education,StartDate)";
                    sql += "VALUES (" + "'" + numVal + "'," + "' ";
                    sql += name + "'," + "'" + selectedEducation + "'" + "," + "'" + date + "'" + ")";
                    Debug.WriteLine(sql);
                    cmd = new SqlCommand(sql, con);
                    // open the connection
                    con.Open();
                    // execute the statement; a reader is returned
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<String> names = new List<String>();
                    // close the reader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("fejl: " + ex.Message);
                }
                finally
                {   // close the connection
                    //if (con.State == ConnectionState.Open) con.Close();
                    con.Close();
                }
            }
            MessageBox.Show("Saved to Database");
            //clears the input from the controls
            txbCPR.Text = "";
            txtName.Text = "";
            cbxEducation.SelectedItem = null;
            //fills the overview list with saved data
            lstOverview.Items.Add("You saved : " + "\n" + "Name : " + name + "\n" + "CPR :" + numVal + "\n"
                + "Education : " + selectedEducation + "\n" + "Start Date : " + date);
            lstOverview.InvalidateVisual();
        }
        //gets the selected date
        private void Calendar1_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {
            date = Calendar1.SelectedDate.Value.ToShortDateString();
        }
        // fills DataGrid with all students assigned for particular exam
        private void cbxAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnValidate.IsEnabled = true;
            selected = cbxAll.SelectedItem.ToString();
            if (exams[0].ToString().Equals(selected))
            {
                selected = exams[0];
            }
            if (exams[1].ToString().Equals(selected))
            {
                selected = exams[1];
            }
            if (exams[2].ToString().Equals(selected))
            {
                selected = exams[2];
            }
            con = new SqlConnection(connStr);
            // selects all students for particular exam
            sql = "SELECT * FROM Students WHERE Exam LIKE" + " " + "'" + selected + "'";
            ds = new DataSet();
            dataAdapter = new SqlDataAdapter(sql, con);
            dataAdapter.Fill(ds, "Students");
            dt = ds.Tables["Students"];
            selected = null;
            // controling the columns of the DataGrid
            //   dt.Columns.RemoveAt(3);
            //  dt.Columns.RemoveAt(2);
            dataGrid1.DataContext = dt.DefaultView;
        }
        //Fills DataGrid with all students which have used all their attempts and not passed the exam
        private void cbxAttempts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnValidate.IsEnabled = true;
            selected = cbxAttempts.SelectedItem.ToString();
            if (exams[0].ToString().Equals(selected))
            {
                selected = exams[0];
            }
            if (exams[1].ToString().Equals(selected))
            {
                selected = exams[1];
            }
            if (exams[2].ToString().Equals(selected))
            {
                selected = exams[2];
            }
            con = new SqlConnection(connStr);

            sql = "SELECT * FROM Students WHERE Passed LIKE 0 AND Attempts LIKE 3 AND Exam LIKE " + "'" + selected + "'";
            ds = new DataSet();
            dataAdapter = new SqlDataAdapter(sql, con);
            dataAdapter.Fill(ds, "Students");
            dt = ds.Tables["Students"];
            //  dt.Columns.RemoveAt(3);
            //  dt.Columns.RemoveAt(2);
            dataGrid1.DataContext = dt.DefaultView;
        }
        //fills the DataGrid with students who has Exemptions for specific Exam
        private void cbxExemptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnValidate.IsEnabled = true;
            selected = cbxExemptions.SelectedItem.ToString();
            if (exams[0].ToString().Equals(selected))
            {
                selected = exams[0];
            }
            if (exams[1].ToString().Equals(selected))
            {
                selected = exams[1];
            }
            if (exams[2].ToString().Equals(selected))
            {
                selected = exams[2];
            }
            con = new SqlConnection(connStr);
            sql = "SELECT * FROM Students WHERE Exemptions IS NOT NULL AND Exam LIKE" + "'" + selected + "'";
            ds = new DataSet();
            dataAdapter = new SqlDataAdapter(sql, con);
            dataAdapter.Fill(ds, "Students");
            dt = ds.Tables["Students"];
            //     dt.Columns.RemoveAt(3);
            //    dt.Columns.RemoveAt(2);
            dataGrid1.DataContext = dt.DefaultView;
        }
        //validates the input from the DataGrid and makes changes to the control schema if needed. 
        private void validateGrades()
        {
            //These containers needs to be here so they can be created everytime when the method is called.
            //Otherwise the numbers from the control boxes are incremented by itself rather than updating.
            List<String> temp = new List<String>();
            List<TextBox> boxes = new List<TextBox>();
            List<String> grades = new List<String>();
            object columnValue = null;
            grades.Add("SY");
            grades.Add("SJ");
            grades.Add("-2");
            grades.Add("0");
            grades.Add("2");
            grades.Add("4");
            grades.Add("7");
            grades.Add("10");
            grades.Add("12");
            boxes.Add(txtSY);
            boxes.Add(txtSJ);
            boxes.Add(txt0);
            boxes.Add(txt3);
            boxes.Add(txt2);
            boxes.Add(txt4);
            boxes.Add(txt7);
            boxes.Add(txt10);
            // Loops trough the Grades column and gets all values
            foreach (DataRow row in dt.Select())
            {
                columnValue = row["Grade"];
                //checks if the input is only SY,SJ,-3,0,2,4,7,10,12
                if (!grades.Contains(columnValue.ToString()))
                {
                    MessageBox.Show(columnValue.ToString() + " is not a valid grade." +
                        "\n" + "Allowed input for this column is : SY,SJ,-3,0,2,4,7,10,12");
                }
                else
                    temp.Add(columnValue.ToString());
            }
            //lambda expression for grouping
            var groups = temp.GroupBy(z => z);
            //this variable holds the grade
            String groupKey = null;
            //this variable keeps track of how many times the grade is written in the DataGrid
            String groupCount = null;
            foreach (var group in groups)
            {
                //I add "txt" to the grade string so I can have the textbox name which I use later.
                groupKey = "txt" + "" + group.Key;
                groupCount = group.Count().ToString();
                //Iterates over all boxes and if the name matches the key I put the count value in the TextBox field
                foreach (TextBox t in boxes)
                {
                    if (t.Name.Equals(groupKey))
                    {
                        t.Text = groupCount;
                    }
                }
            }
            //If the validation is successful we can save the changes to the Database
            btnSaveToDatabase.IsEnabled = true;
        }
        //calls the validation method
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            validateGrades();
        }
        // Saves any data which is change directly from the DataGrid. Requires successful validation first.
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommandBuilder catCB = new SqlCommandBuilder(dataAdapter);
                dataAdapter.UpdateCommand = catCB.GetUpdateCommand();
                dataAdapter.DeleteCommand = catCB.GetDeleteCommand();
                dataAdapter.InsertCommand = catCB.GetInsertCommand();
                dataAdapter.Update(dt);
                MessageBox.Show("Saved to Database");
            }
            catch (Exception s)
            {
                Console.WriteLine(s + "error saving to database");
            }
        }
        // fills combobox with distinct student names from database
        private void fill()
        {
            sql = "SELECT DISTINCT Name FROM Students";
            ds = new DataSet();
            dataAdapter = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            try
            {
                dataAdapter.Fill(ds, "Students");
                //Binding the data to the combobox.
                cbxStudents.DataContext = ds.Tables["Students"].DefaultView;
                cbxStudents.DisplayMemberPath = "Name";
                cbxStudents.SelectedValuePath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading students." + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        // Event listener for combobox of student names
        //Fills textfields with students data
        private void cbxStudents_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            List<String> data = new List<String>();
            try
            {
                String selectedName = cbxStudents.SelectedValue.ToString();
                sql = "SELECT Cpr,Education,Password FROM Students WHERE Name = " + "'" + selectedName + "'";
                txbName.Text = selectedName;
                cmd = new SqlCommand(sql, con);
                // open the connection
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txbCpr.Text = reader[0].ToString();
                    txbPass.Text = reader[2].ToString();
                }
                foreach (String s in data)
                {
                    Debug.WriteLine(s);
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("fejl: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        //saves changes to updated student
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string cpr = txbCpr.Text.ToString();
            string studentName = "";
            string name = txbName.Text.ToString();
            //Validation 
            try
            {
                long numVal = Convert.ToInt64(cpr);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            try{
                name.Equals(null);
                selectedEducation = cbxChangeEducation.SelectedItem.ToString();
                studentName = cbxStudents.SelectedValue.ToString();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please make sure you : " + "\n" + "1. Put number for CPR" + "\n"
                    + "2.Text for Name" + "\n" + "3.Selected Education" + "\n" + "error message : " + ex.Message);
            }
            {
                String values = "(" + numVal + "," + name + "," + selectedEducation + "," + "'" + date + "'" + ")";
                //saves into the Database
                try
                {
                    sql = "UPDATE Students SET Name='" + name + "',Cpr='" + cpr + "',Education='" + selectedEducation + "',Password='" + txbPass.Text + "' WHERE Name ='" + studentName + "'";
                    Debug.WriteLine(sql);
                    cmd = new SqlCommand(sql, con);
                    // open the connection
                    con.Open();
                    // execute the statement; a reader is returned
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<String> names = new List<String>();
                    // close the reader
                    reader.Close();
                    MessageBox.Show("Saved to Database");
                    //clears the input from the controls
                   
                    //fills the overview list with saved data
                    lstOverview.Items.Add("You saved : " + "\n" + "Name : " + txbName.Text + "\n" + "CPR :" + txbCpr.Text + "\n"
                        + "Education : " + selectedEducation + "\n" + "Start Date : " + date);
                    lstOverview.InvalidateVisual();
                    txbCpr.Text = "";
                    txbName.Text = "";
                    cbxStudents.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("fejl: " + ex.Message);
                }
                finally
                {   // close the connection
                    //if (con.State == ConnectionState.Open) con.Close();
                    con.Close();
                }
            }
        }
        // removes selected student
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string studentName = "";
            //Validation of input
            try
            {
                studentName = cbxStudents.SelectedValue.ToString();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please select a student." + ex.Message);
            }
            {
                //removes from the Database
                try
                {
                    sql = "DELETE from Students WHERE name ='" + studentName + "'";
                    Debug.WriteLine(sql);
                    cmd = new SqlCommand(sql, con);
                    // open the connection
                    con.Open();
                    // execute the statement; a reader is returned
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<String> names = new List<String>();
                    // close the reader
                    reader.Close();
                    MessageBox.Show("Saved to Database");
                    //clears the input from the controls
                    cbxStudents.SelectedItem = null;
                    cbxStudents.InvalidateVisual();
                    //fills the overview list with saved data
                    lstOverview.Items.Add("You deleted the student  : " + studentName);
                    lstOverview.InvalidateVisual();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("fejl: " + ex.Message);
                }
                finally
                {   // close the connection
                    //if (con.State == ConnectionState.Open) con.Close();

                    con.Close();
                }
            }
        }

    }


}

