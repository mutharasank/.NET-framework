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

namespace BindToObject3
{
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

            // Listen for the validation error event on the age text box
            // (you can do this in XAML by handling the Validation.Error
            //  attached event on the ageTextBox)
            //Validation.AddErrorHandler(this.ageTextBox, ageTextBox_ValidationError);

            // set binding for tBoxName
            Binding binding = new Binding();
            binding.Source = person;
            binding.Path = new PropertyPath("Name");
            binding.Mode = BindingMode.TwoWay;
            tBoxName.SetBinding(TextBox.TextProperty, binding);

            // set binding for tBoxAge
            binding = new Binding();
            binding.Source = person;
            binding.Path = new PropertyPath("Age");

            // set validation start:
            binding.NotifyOnValidationError = true;

            // custom validationrule
            ValidationRule rule = new NumberRangeRule(0, 128);
            binding.ValidationRules.Add(rule);

            // see that error is shown
            Validation.AddErrorHandler(tBoxAge, new EventHandler<ValidationErrorEventArgs>(tBoxAge_ValidationError));

            // when to validate
            binding.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus;

            // set validation finished

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


        void tBoxAge_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            // Show the string pulled out of the exception by the
            // ExceptionValidationRule

            MessageBox.Show((string)e.Error.ErrorContent, "Validation Error");

            tBoxAge.ToolTip = (string)e.Error.ErrorContent;
        }

    }
}

