﻿using System;
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

namespace BindToObject2
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

            // set binding for tBoxName
            Binding binding = new Binding();
            binding.Source = person;
            binding.Path = new PropertyPath("Name");
            binding.Mode = BindingMode.TwoWay;
            tBoxName.SetBinding(TextBox.TextProperty, binding);

            // set binding for tBoxAge (bind to person.age)
            binding = new Binding();
            binding.Source = person;
            binding.Path = new PropertyPath("Age");
            binding.Converter = new Base16Converter();
            binding.Mode = BindingMode.TwoWay;
            tBoxAge.SetBinding(TextBox.TextProperty, binding);

            // set binding for ForeGround i tBoxAge (bind to person.age)
            binding = new Binding();
            binding.Source = person;
            binding.Path = new PropertyPath("Age");
            binding.Converter = new AgeToForegroundConverter();
            binding.Mode = BindingMode.OneWay;
            tBoxAge.SetBinding(TextBox.ForegroundProperty, binding);
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

    }
}
