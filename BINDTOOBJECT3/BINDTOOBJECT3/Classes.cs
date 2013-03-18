using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;   // INottifyPropertyChanged 
using System.Windows.Data;     // IValueConverter
using System.Windows.Controls; // ValidationRule
using System.Windows.Media;
using System.Diagnostics;    // Brush


namespace BindToObject3
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
            // Only convert to brushes...
            if (targetType != typeof(Brush)) { return null; }

            // DANGER! After 25, it's all down hill...
            int age = int.Parse(value.ToString());
            return (age > 25 ? Brushes.Red : Brushes.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // should not be called in our example
            throw new NotImplementedException();
        }
    }

    public class Base16Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Convert to base 16
            return ((int)value).ToString("x");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Convert from base 16
            return int.Parse((string)value, System.Globalization.NumberStyles.HexNumber);
        }
    }

    public class NumberRangeRule : ValidationRule
    {
        public NumberRangeRule(int min, int max)
        {
            this._min = min; this._max = max;
        }

        int _min;
        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        int _max;
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            Debug.WriteLine("Value=" + value);

            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "Invalid number format");
            }

            if (number < _min || number > _max)
            {
                string s = string.Format("Number: {2} is out of range ({0}-{1})", _min, _max, number);
                return new ValidationResult(false, s);
            }

            //return new ValidationResult(true, null);
            return ValidationResult.ValidResult; // static validation result to save garbage
        }
    }
}
