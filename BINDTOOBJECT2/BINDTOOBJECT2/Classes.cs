using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;   // INottifyPropertyChanged 
using System.Windows.Data;     // IValueConverter
using System.Windows.Controls; // ValidationRule
using System.Windows.Media;    // Brush

namespace BindToObject2
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



    // The optional ValueConversion attribute is useful for documenting the expected
    // source and target types for developers and tools, but is not enforced by WPF
    [ValueConversion(/* sourceType */ typeof(int), /* targetType */ typeof(Brush))]
    public class AgeToForegroundConverter : IValueConverter
    {
        // Called when converting the Age to a Foreground brush
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Only convert to brushes... (int -> Color  Please see attribute above
            if (targetType != typeof(Brush)) { return null; }

            // DANGER! After 25, it's all down hill...
            int age = (int)value;
            return (age > 25 ? Brushes.Red : Brushes.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // should not be called in our example
            throw new NotImplementedException();
        }
    }

    [ValueConversion(/* sourceType */ typeof(int), /* targetType */ typeof(string))]
    public class Base16Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Convert to base 16 (int -> string)
            return System.Convert.ToString((int)value, 16).ToUpper().PadLeft(2, '0');
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Convert from base 16 (string -> int)
            return System.Convert.ToInt32((string)value, 16);
        }
    }

}
