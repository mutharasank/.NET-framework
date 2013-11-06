using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TimeRegistrationSystem
{
    class Dao
    {

        string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimeRegistrationSystem;Integrated Security=True";
        string sql;
        SqlCommand cmd;
        SqlConnection con;
        private static Dao instance;
        public static Dao Instance
        {
            get
            {
                if (instance == null)
                    instance = new Dao();

                return instance;
            }

            set { }
        }
        public Dao()
        {
            con = new SqlConnection(connStr);
        }

        /// <summary>
        /// Loads projecs by specific department
        /// </summary>
        /// <param name="department_id"></param>
        /// <returns></returns>
        public ObservableCollection<Project> LoadProjectsByDepartment(int department_id)
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            sql = "SELECT * FROM dbo.Projects WHERE department_id = " + "'" + department_id + "'";


            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader[0];
                string name = (string)reader[1];
                int department = (int)reader[2];
                //     int registration_id = (int)reader[3];
                String date = (String)reader[4];

                Project p = new Project(id, name, department, date);
                Debug.WriteLine(id + name + department + date);
                projects.Add(p);

            }                    // close the reader

            reader.Close();
            con.Close();

            return projects;

        }
        /// <summary>
        /// Loads all employees by specific project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public ObservableCollection<Employee> GetEmployeesByProject(Project project)
        {

            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            sql = "SELECT * FROM dbo.Employees JOIN dbo.AssignedRegistrations ON dbo.Employees.employee_id = dbo.AssignedRegistrations.employee_id  WHERE dbo.AssignedRegistrations.project_id=" + project.Id;


            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {


                int id = (int)reader[0];
                string name = (string)reader[1];
                long cpr = (long)reader[2];
                int department_id = (int)reader[3];
                int administrator_id = (int)reader[4];

                Employee e = new Employee(id, name, cpr, department_id, administrator_id);

                employees.Add(e);

            }                    // close the reader

            reader.Close();
            con.Close();

            return employees;

        }
        /// <summary>
        /// Gets all employees not in a project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public ObservableCollection<Employee> GetAllEmployeesNotInProject(Project project)
        {
            ObservableCollection<Employee> Allemployees = new ObservableCollection<Employee>();
            sql = "SELECT * FROM dbo.Employees WHERE dbo.Employees.employee_id NOT IN ";
            sql += "(SELECT DISTINCT dbo.Employees.employee_id FROM dbo.Employees JOIN dbo.AssignedRegistrations ";
            sql += "ON dbo.Employees.employee_id = dbo.AssignedRegistrations.employee_id  WHERE ";
            sql += "(dbo.AssignedRegistrations.project_id =" + "'" + project.Id + "'" + ")";

            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = (string)reader[1];
                    Employee employee = new Employee(name);
                    Allemployees.Add(employee);
                }                    // close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees not in project");
            }
            finally
            {
                con.Close();
            }

            return Allemployees;
        }

        /// <summary>
        /// Assigns a new Time Registration to employee
        /// </summary>
        /// <param name="project_id"></param>
        /// <param name="employee_id"></param>
        /// <param name="hours"></param>
        /// <param name="date"></param>
        public void AssignTimeRegistration(int project_id, int employee_id, float hours, DateTime date)
        {
            String day = date.Day.ToString();
            String month = date.Month.ToString();
            String year = date.Year.ToString();

            String final_date = day + ":" + month + ":" + year;
            sql = "INSERT INTO dbo.AssignedRegistrations ";
            sql += "(project_id,employee_id,estimated_hours,date) VALUES " + "('" + project_id + "'," + "'" + employee_id + "'," + "'" + hours + "','" + final_date + "')";

            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            con.Close();
        }




        /// <summary>
        /// Gets all projects from DataBase
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Project> GetAllProjects()
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();


            sql = "SELECT * FROM dbo.Projects";
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                int id = (int)reader[0];
                string name = (string)reader[1];
                int department = (int)reader[2];
                //int registration_id = (int)reader[3];
                String date = (String)reader[4];

                Project p = new Project(id, name, department, date);
                Debug.WriteLine(id + name + department, date);
                projects.Add(p);

            }                    // close the reader

            reader.Close();
            con.Close();

            return projects;
        }

        /// <summary>
        /// Creates new Project
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department_id"></param>
        /// <param name="date"></param>
        public void CreateProject(String name, int department_id, DateTime date)
        {
            String day = date.Day.ToString();
            String month = date.Month.ToString();
            String year = date.Year.ToString();

            String formated_date = day + ":" + month + ":" + year;


            sql = "INSERT INTO dbo.Projects ";
            sql += "(name,department_id,start_date) VALUES " + "('" + name + "'," + "'" + department_id + "'," + "'" + formated_date + "')";

            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Updates Project
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department_id"></param>
        /// <param name="date"></param>
        /// <param name="nameToCompare"></param>
        public void UpdateProject(String name, int department_id, DateTime date, String nameToCompare)
        {
            String day = date.Day.ToString();
            String month = date.Month.ToString();
            String year = date.Year.ToString();

            String formated_date = day + ":" + month + ":" + year;


            sql = "UPDATE dbo.Projects SET name = " + "'" + name + "'" + ",department_id=" + "'" + department_id + "'" + ",start_date=" + "'" + formated_date + "'" + " WHERE name =" + "'" + nameToCompare + "'";
            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(sql);
                MessageBox.Show("Record Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Removes Project from DataBase
        /// </summary>
        /// <param name="project"></param>
        public void RemoveProject(Project project)
        {

            try
            {
                sql = "DELETE FROM dbo.Projects WHERE project_id = " + "'" + project.Id + "'" + "AND name = " + "'" + project.Name + "'";
                cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(sql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// Removes All Assigned Registrations
        /// </summary>
        /// <param name="project"></param>
        public void RemoveAssignedRegistrations(Project project)
        {
            try
            {
                sql = "DELETE FROM dbo.AssignedRegistrations WHERE project_id = " + "'" + project.Id + "'";

                cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(sql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        /// <summary>
        /// Removes Time Registrations from Project
        /// </summary>
        /// <param name="p"></param>
        public void RemoveTimeRegistrations(Project p)
        {

            try
            {
                sql = "DELETE FROM dbo.TimeRegistrations WHERE project_id = " + "'" + p.Id + "'";
                cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(sql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Creates new department
        /// </summary>
        /// <param name="name"></param>
        public void CreateDepartment(String name)
        {

            sql = "INSERT INTO dbo.Departments ";
            sql += "(name) VALUES " + "(" + name + ")";

            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
        }

        /// <summary>
        /// Removes department from DataBase
        /// </summary>
        /// <param name="name"></param>
        public void RemoveDepartment(String name)
        {
            sql = "DELETE FROM dbo.Departments ";
            sql += "WHERE name = " + name;

            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
        }


        /// <summary>
        /// Removes department from DataBase
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Department> LoadDepartments()
        {
            ObservableCollection<Department> departments = new ObservableCollection<Department>();
            sql = "SELECT * FROM dbo.Departments ";


            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader[0];
                String name = (String)reader[1];

                Department dep = new Department(id, name);

                departments.Add(dep);
                Console.WriteLine(departments.Count);
            }
            reader.Close();
            con.Close();
            return departments;

        }


        /// <summary>
        /// Gets total estimated hours for a Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public double GetTotalEstimatedHours(Project project)
        {
            sql = "SELECT SUM (estimated_hours)FROM DBO.AssignedRegistrations WHERE project_id = " + "'" + project.Id + "'";
            cmd = new SqlCommand(sql, con);
            double result = 0;
            // open the connection

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = (double)reader[0];
                }
                Debug.WriteLine(sql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        /// <summary>
        /// Gets total Registered Hours from Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public double GetTotalRegisteredHours(Project project)
        {
            sql = "SELECT SUM (registered_hours)FROM DBO.TimeRegistrations WHERE project_id = " + "'" + project.Id + "'";
            cmd = new SqlCommand(sql, con);
            double result = 0;
            // open the connection

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = (double)reader[0];
                }
                Debug.WriteLine(sql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        /// <summary>
        /// Updates DataGrid
        /// </summary>
        public void UpdateDataGrid()
        {
            sql = "DROP VIEW dbo.registration_data";
            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
                        try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Fills DataGrid with data from DataBase
        /// </summary>
        /// <param name="dataGrid"></param>
        public void PopulateDataGrid(DataGrid dataGrid)
        {            DataSet ds = null;
            DataTable dt = null;
            SqlDataAdapter dataAdapter = null;
            con.Open();
            con = new SqlConnection(connStr);
            sql = "SELECT * from dbo.registration_data";
            ds = new DataSet();
            dataAdapter = new SqlDataAdapter(sql, con);
                        dataAdapter.Fill(ds, "dbo.registration_data");
            dt = ds.Tables["dbo.registration_data"];
            dataGrid.DataContext = dt.DefaultView;
            con.Close();
                    }

        /// <summary>
        /// Creates a View with registration data from DataBase
        /// </summary>
        public void CreateInformationView()
        {
            sql = "CREATE VIEW registration_data AS ";
            sql += "(SELECT dbo.AssignedRegistrations.employee_id,dbo.AssignedRegistrations.project_id,";
            sql += "dbo.AssignedRegistrations.date,dbo.AssignedRegistrations.estimated_hours,";
            sql += "dbo.TimeRegistrations.registered_hours as registered_hours,dbo.TimeRegistrations.date ";
            sql += "as registered_on FROM dbo.AssignedRegistrations JOIN dbo.TimeRegistrations ON ";
            sql += "dbo.AssignedRegistrations.employee_id = dbo.TimeRegistrations.employee_id)";

            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
                        try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// Gets number of employees
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public int GetNumberOfEmployees(string DepartmentName, String ProjectName)
        {
            sql = "SELECT COUNT(name) FROM dbo.Employees where department_name = ";
            sql += "'" + DepartmentName + "' " + "and project_name = " + "'" + ProjectName + "'";

            int numberOfEmployees = 0;
            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                numberOfEmployees = (int)reader[0];

                Console.WriteLine(numberOfEmployees);
            }                    // close the reader
            reader.Close();

            return numberOfEmployees;

        }
        /// <summary>
        /// Gets number of projects
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public int GetNumberOfProjects(string DepartmentName, String ProjectName)
        {
            int numberOfProjects = 0;
            sql = "SELECT COUNT(name) FROM dbo.Projects where department_name = ";
            sql += "'" + DepartmentName + "' " + "name = " + "'" + ProjectName + "'";


            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                numberOfProjects = (int)reader[0];

                Console.WriteLine(numberOfProjects);
            }                    // close the reader
            reader.Close();

            return numberOfProjects;

        }
        /// <summary>
        /// Gets Id of a Employee
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int getIdOfEmployee(String name)
        {
            int id = 0;
            sql = "SELECT employee_id FROM dbo.Employees WHERE name = " + "'" + name + "'";
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {


                id = (int)reader[0];


            }                    // close the reader

            reader.Close();
            con.Close();

            return id;
        }



        /// <summary>
        /// Removes Employee
        /// </summary>
        /// <param name="cpr"></param>
        public void RemoveEmployee(int cpr)
        {
            sql = "DELETE FROM dbo.Employees ";
            sql += "WHERE cpr = " + cpr;

            Debug.WriteLine(sql);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
        }

    }

}
