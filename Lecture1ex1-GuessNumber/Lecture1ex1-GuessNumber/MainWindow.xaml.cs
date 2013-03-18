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

namespace Lecture1ex1_GuessNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
        int number;
        int stored;
        public MainWindow()
        {
            InitializeComponent();
            button2.IsEnabled = false; 
        }
        public void MainWindow_Loaded(object sender,RoutedEventArgs a)
        {
          number=r.Next(1, 100);
          button2.IsEnabled = false;
          textBox.Focus();
         
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
           
            Console.WriteLine(number);
          
            
            try
            {
                stored = Int32.Parse(textBox.Text);
            }
            catch
            {
                Console.WriteLine("number error");
            }

            Console.WriteLine(stored);

            if (stored > number)
            {
                lbl3.Content = "too high";
                textBox.Text = "";
             

            }
            if (stored < number)
            {
                lbl3.Content = "too low";
                textBox.Text = "";
              
            }
            if (stored == number)
            {
                lbl3.Content = "Correct,Congratulation!";
                textBox.Text = "";
                button1.IsEnabled = false;
                button2.IsEnabled = true;
            }
            


        }

        private void TryAgain_click(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = true;
            button2.IsEnabled = false;
            number = r.Next(1, 100);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }


        }
  

   private void MainWindow_ClosingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
   {

       MessageBoxResult mr = MessageBox.Show("Do you want to stop guessing?",
"Stopping?", MessageBoxButton.YesNo);
       if (mr == MessageBoxResult.Yes)
       {
           Console.WriteLine("quit");
       }
       if(mr==MessageBoxResult.No)
       {
           e.Cancel = true;
           

       }
   }
    }
}
