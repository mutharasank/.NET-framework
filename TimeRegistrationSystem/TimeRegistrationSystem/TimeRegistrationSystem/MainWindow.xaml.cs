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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeRegistrationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime date;


        public MainWindow()
        {
            InitializeComponent();
            FillcbxDepartments();
            PopulateListProjects();
            PopulateCbxProjectsOverview();
        }

        /// <summary>
        /// Handles SelectionChanged Event for ComboBox Departments from Registrations Assignment Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            if (projects != null)
                projects.Clear();
            Department department = (Department)cbxDepartments.SelectedItem;
            int department_id = department.id;
            projects = Service.Instance.LoadProjectsByDepartment(department_id);
            cbxProjects.ItemsSource = projects;
            cbxDepartments.DataContext = projects;
            cbxProjects.DisplayMemberPath = "Name";
        }

        /// <summary>
        /// Handles SelectionChanged Event for ComboBox with Projects from Registrations Assignment Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Project project;
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            if (cbxProjects.SelectedItem != null)
            {
                project = (Project)cbxProjects.SelectedItem;
                employees = Service.Instance.GetEmployeesByProject(project);
                lstEmployees.ItemsSource = employees;
                lstEmployees.DataContext = employees;
                lstEmployees.DisplayMemberPath = "Name";
                LoadEmployeesNotInProject();
            }
            else
            {
                employees.Clear();
            }


        }
        /// <summary>
        /// fills ComboBoxes with Departments
        /// </summary>
        private void FillcbxDepartments()
        {
            ObservableCollection<Department> departments = new ObservableCollection<Department>();
            departments = Service.Instance.LoadDepartments();
            cbxDepartments.ItemsSource = departments;
            cbxDepartments.DataContext = departments;
            cbxDepartments.DisplayMemberPath = "name";

            cbxDepartmentsCRUD.ItemsSource = departments;
            cbxDepartmentsCRUD.DataContext = departments;
            cbxDepartmentsCRUD.DisplayMemberPath = "name";

        }
        /// <summary>
        /// Populates List of projects in the CRUD Panel
        /// </summary>
        private void PopulateListProjects()
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            projects = Service.Instance.GetAllProjects();
            lstProjectsCRUD.ItemsSource = projects;
            lstProjectsCRUD.DataContext = projects;
            lstProjectsCRUD.DisplayMemberPath = "Name";
        }

        /// <summary>
        /// Populates List of employees which are not in selected project
        /// </summary>
        private void LoadEmployeesNotInProject()
        {
            ObservableCollection<Employee> allEmployees = new ObservableCollection<Employee>();
            Project project;

            if (allEmployees != null)
            {
                allEmployees.Clear();
            }
            if (cbxProjects.SelectedItem != null)
            {
                project = (Project)cbxProjects.SelectedItem;
                allEmployees = Service.Instance.GetAllEmployeesNotInProject(project);
                lstEmployeesToAssign.ItemsSource = allEmployees;
                lstEmployeesToAssign.DataContext = allEmployees;
                lstEmployeesToAssign.DisplayMemberPath = "Name";
            }
        }
        /// <summary>
        ///  Sets the value of Date field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            date = DatePicker1.SelectedDate.Value;
        }

        /// <summary>
        /// Saves Assigned Time registration to Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            Project project;
            Employee employee;
            float hours = 0;
            employee = (Employee)lstEmployeesToAssign.SelectedItem;
            project = (Project)cbxProjects.SelectedItem;
            String text = txbHours.Text;

            try
            {
                hours = float.Parse(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Missing input for hours,employee,date or project" + " " + "\n" + ex.Message);
            }

            Service.Instance.AssignTimeRegistration(project.Id, Service.Instance.GetIdOfEmployee(employee.Name), hours, date);

            MessageBox.Show("Time Registration saved to DataBase");
           
            if (cbxProjects.SelectedItem != null)
            {
                
                project = (Project)cbxProjects.SelectedItem;
                // get list of employee objects
                employees = Service.Instance.GetEmployeesByProject(project);
                lstEmployees.ItemsSource = employees;
                lstEmployees.DataContext = employees;
                lstEmployees.DisplayMemberPath = "Name";
            }
        }
        /// <summary>
        /// Creates and stores Project to Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateCRUD_Click_1(object sender, RoutedEventArgs e)
        {
            Department department = (Department)cbxDepartmentsCRUD.SelectedItem;
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            try
            {
                Service.Instance.CreateProject(txtNameCRUD.Text, department.id, DatePickerCRUD.SelectedDate.Value);
                projects = Service.Instance.GetAllProjects();
                lstProjectsCRUD.ItemsSource = projects;
                lstProjectsCRUD.InvalidateVisual();
                lstProjectsCRUD.DataContext = projects;
                lstProjectsCRUD.DisplayMemberPath = "Name";
                MessageBox.Show("Project Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        /// <summary>
        /// Handles Updating of Selected Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCRUD_Click_1(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            Project project = (Project)lstProjectsCRUD.SelectedItem;
            Department department = (Department)cbxDepartmentsCRUD.SelectedItem;
            try
            {
                Service.Instance.UpdateProject(txtNameCRUD.Text, department.id, DatePickerCRUD.SelectedDate.Value, project.Name);
                projects = Service.Instance.GetAllProjects();
                lstProjectsCRUD.ItemsSource = projects;
                lstProjectsCRUD.InvalidateVisual();
                lstProjectsCRUD.DataContext = projects;
                lstProjectsCRUD.DisplayMemberPath = "Name";
                MessageBox.Show("Record Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        /// <summary>
        /// Handler for lstProjectsCRUD from Project CRUD panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstProjectsCRUD_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Project project = (Project)lstProjectsCRUD.SelectedItem;
            if (project != null)
            {
                txtNameCRUD.Text = project.Name;
            }
        }

        /// <summary>
        /// Deletes Project with right click from List with projects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            Project p = (Project)lstProjectsCRUD.SelectedValue;
            Service.Instance.RemoveAssignedRegistrations(p);
            Service.Instance.RemoveTimeRegistrations(p);
            Service.Instance.RemoveProject(p);
            MessageBox.Show("The project and all assigned time registrations are deleted.");
            projects = Service.Instance.GetAllProjects();
            lstProjectsCRUD.ItemsSource = projects;
            lstProjectsCRUD.InvalidateVisual();
            lstProjectsCRUD.DataContext = projects;
            lstProjectsCRUD.DisplayMemberPath = "Name";
        }

        /// <summary>
        /// Creates and Updates View in Database and Populates DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInformation_Click_1(object sender, RoutedEventArgs e)
        {
            Service.Instance.UpdateDataGrid();
            Service.Instance.CreateInformationView();
            Service.Instance.PopulateDataGrid(DataGridInformation);
        }

        /// <summary>
        /// Handles and displays estimated and registered hours for project from List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxProjectsOverview_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Project project;
            double estimatedHours;
            double registeredHours;
            project = (Project)cbxProjectsOverview.SelectedItem;
            estimatedHours = Service.Instance.GetTotalEstimatedHours(project);
            registeredHours = Service.Instance.GetTotalRegisteredHours(project);
            txtEstimated.Text = estimatedHours.ToString();
            txtRegistered.Text = registeredHours.ToString();
        }

        /// <summary>
        /// Populates ComboBox with Projects from Database
        /// </summary>
        private void PopulateCbxProjectsOverview()
        {
            ObservableCollection<Project> projects = new ObservableCollection<Project>();
            if (projects != null)
                projects.Clear();
            projects = Service.Instance.GetAllProjects();
            cbxProjectsOverview.ItemsSource = projects;
            cbxProjectsOverview.DataContext = projects;
            cbxProjectsOverview.DisplayMemberPath = "Name";
        }
    }
}
