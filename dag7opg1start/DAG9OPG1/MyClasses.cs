using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;

namespace Dag7Opg1
{
    public class Person : INotifyPropertyChanged
    {
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify("Name");
            }
        }

        double height;
        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                Notify("Height");
                Notify("BMI");
            }
        }

        int weight;
        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                Notify("Weight");
                Notify("BMI");
            }
        }

        public double BMI
        {
            get
            {
                return weight / height / height * 10;
            }
        }

        public override string ToString()
        {
            string s = String.Format("{0}, h={1}, w={2}: BMI={3:##.#}", name, height, weight, BMI / 10);
            return s;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(String propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }


}
