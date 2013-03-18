using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindToObject_B
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Person person = new Person("Tom", 11);

        public Window1()
        {
            InitializeComponent();

            this.birthdayButton.Click += birthdayButton_Click;

            // Set DataSourcen in grid'ets DataContext
            // Please note the Grid must be given a name in XAML

            grid.DataContext = person;
            //tBoxAge.DataContext = person;

            // set binding for tBoxName WITHOUT setting source
            Binding binding = new Binding();
            binding.Path = new PropertyPath("Name");
            binding.Mode = BindingMode.TwoWay;
            tBoxName.SetBinding(TextBox.TextProperty, binding);

            // set biding for tBoxAge WITHOUT setting source
            binding = new Binding();
            binding.Path = new PropertyPath("Age");
            binding.Mode = BindingMode.TwoWay;
            tBoxAge.SetBinding(TextBox.TextProperty, binding);
        }

        void birthdayButton_Click(object sender, RoutedEventArgs e)
        {
            person.Age++;
            MessageBox.Show(string.Format("Happy Birthday, {0}, age {1}!",
                            person.Name, person.Age), "Birthday");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            person.Age = Convert.ToInt32(tBoxSetAge.Text);
        }

        private void btnShowObject_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(person.ToString());
        }
    }
}
