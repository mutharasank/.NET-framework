using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Data;
using System.ComponentModel;

namespace ListBinding2
{
    public class Person : INotifyPropertyChanged
    {
        // INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name == value) { return; }
                this.name = value;
                Notify("Name");
            }
        }

        int age;
        public int Age
        {
            get { return this.age; }
            set
            {
                if (this.age == value) { return; }
                this.age = value;
                Notify("Age");
            }
        }

        public Person() { }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
    }

    class PersonSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            Person lhs = (Person)x;
            Person rhs = (Person)y;

            // Sort Name ascending and Age descending
            int nameCompare = lhs.Name.CompareTo(rhs.Name);
            if (nameCompare != 0)
                return nameCompare;
            return rhs.Age - lhs.Age;
        }
    }


}
