using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;   // INottifyPropertyChanged 
using System.Windows.Data;     // IValueConverter
using System.Windows.Controls; // ValidationRule
using System.Windows.Media;    // Brush

namespace BindToObject_B
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

                // OBS!!
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

                // OBS!!
                Notify("Age");
            }
        }

        public Person() { }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public override string ToString()
        {
            return string.Format("Person: {0}, age {1}!",
                            name, age);
        }
    }


    public class PassivPerson
    {
        string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name == value) { return; }
                this.name = value;
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
            }
        }

        public PassivPerson() { }
        public PassivPerson(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public override string ToString()
        {
            return string.Format("Person: {0}, age {1}!",
                            name, age);
        }
    }
}
