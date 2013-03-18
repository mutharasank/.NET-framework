using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel; // INotifyPropertyChanged
using System.Collections.ObjectModel; // ObserverableCollection<T>
using System.Collections;


namespace ListBinding2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ObservableCollection<Person> persons = new ObservableCollection<Person>();

        ListCollectionView view = null;

        public Window1()
        {
            InitializeComponent();

            this.birthdayButton.Click += birthdayButton_Click;
            this.backButton.Click += backButton_Click;
            this.forwardButton.Click += forwardButton_Click;
            this.addButton.Click += addButton_Click;
            this.sortButton.Click += sortButton_Click;
            this.filterButton.Click += filterButton_Click;

            // initialse the Observable collection
            persons.Add(new Person { Name = "Tom", Age = 18 });
            persons.Add(new Person { Name = "John", Age = 21 });
            persons.Add(new Person { Name = "Melissa", Age = 34 });

            // get DefaultView for people
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(persons);
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            grid1.DataContext = persons;
        }

        void birthdayButton_Click(object sender, RoutedEventArgs e)
        {
            // get the currently selected Person
            Person person = (Person)view.CurrentItem;

            if (person != null)
            {
                person.Age++;
                MessageBox.Show(
                  string.Format("Happy Birthday, {0}, age {1}!", person.Name, person.Age), "Birthday");
            }
        }

        void backButton_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToPrevious();
            if (view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToFirst();
            }
        }

        void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToNext();
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToLast();
            }
        }

        void addButton_Click(object sender, RoutedEventArgs e)
        {
            persons.Add(new Person("Chris", 37));
        }

        void sortButton_Click(object sender, RoutedEventArgs e)
        {
            if (view.CustomSort == null)
            {
                view.CustomSort = new PersonSorter();
            }
            else
            {
                view.CustomSort = null;
            }
        }

        void filterButton_Click(object sender, RoutedEventArgs e)
        {
            if (view.Filter == null)
            {
                view.Filter = delegate(object item)
                {
                    // Just show the over 25-year olds
                    return ((Person)item).Age >= 25;
                };
            }
            else
            {
                view.Filter = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("" + lBoxPerson.SelectedIndex);
        }

    }

}
