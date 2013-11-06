using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TimeRegistrationSystem
{
    class Service
    {
        private static Service instance;
        public static Service Instance
        {
            get
            {
                if (instance == null)
                    instance = new Service();

                return instance;
            }

            set { }
        }


        public ObservableCollection<Department> LoadDepartments()
        {
            return Dao.Instance.LoadDepartments();
        }

        public ObservableCollection<Project> LoadProjectsByDepartment(int department_id)
        {
            return Dao.Instance.LoadProjectsByDepartment(department_id);
        }
        public ObservableCollection<Employee> GetEmployeesByProject(Project project)
        {
            return Dao.Instance.GetEmployeesByProject(project);
        }

        public void AssignTimeRegistration(int project_id, int employee_id, float hours, DateTime date)
        {
            Dao.Instance.AssignTimeRegistration(project_id, employee_id, hours, date);
        }

        public ObservableCollection<Employee> GetAllEmployeesNotInProject(Project project)
        {
            return Dao.Instance.GetAllEmployeesNotInProject(project);
        }
        public int GetIdOfEmployee(String name)
        {
            return Dao.Instance.getIdOfEmployee(name);
        }

        public void CreateProject(String name, int department_id, DateTime date)
        {
            Dao.Instance.CreateProject(name, department_id, date);
        }
        public void UpdateProject(String name, int department_id, DateTime date, String nameToCompare)
        {
            Dao.Instance.UpdateProject(name, department_id, date, nameToCompare);
        }
        public ObservableCollection<Project> GetAllProjects()
        {
            return Dao.Instance.GetAllProjects();
        }

        public void RemoveProject(Project p)
        {
            Dao.Instance.RemoveProject(p);
        }
        public void RemoveAssignedRegistrations(Project p)
        {
            Dao.Instance.RemoveAssignedRegistrations(p);
        }

        public void RemoveTimeRegistrations(Project p)
        {
            Dao.Instance.RemoveTimeRegistrations(p);
        }
        public void PopulateDataGrid(DataGrid dataGrid)
        {
            Dao.Instance.PopulateDataGrid(dataGrid);
        }
        public void CreateInformationView()
        {
            Dao.Instance.CreateInformationView();
        }
        public void UpdateDataGrid()
        {
            Dao.Instance.UpdateDataGrid();
        }
        public double GetTotalEstimatedHours(Project p)
        {
            return Dao.Instance.GetTotalEstimatedHours(p);
        }
        public double GetTotalRegisteredHours(Project p)
        {
            return Dao.Instance.GetTotalRegisteredHours(p);
        }
    }
}
