using System;
using System.Collections.Generic;
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

namespace PersonListDataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Person p = new Person { name = "Sam", age = 20, cpr = 271092345 };
        Person p1 = new Person { name = "Tim", age = 22, cpr = 271392341 };
        Person p2 = new Person { name = "John", age = 25, cpr = 275092343 };
        public MainWindow()
        {


            InitializeComponent();
            List<Person> persons = new List<Person>();




            persons.Add(p);
            persons.Add(p1);
            persons.Add(p2);
            personsList.ItemsSource = persons;

            personsList.DisplayMemberPath = "name";
        }


        private void personsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            textBoxName.DataContext = personsList.SelectedValue;
            textBoxAge.DataContext = personsList.SelectedValue;
            textBoxCpr.DataContext = personsList.SelectedValue;
        }
    }
}
