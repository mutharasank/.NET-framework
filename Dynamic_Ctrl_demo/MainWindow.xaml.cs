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

namespace Dynamic_Ctrl_demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDefault_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Add(tBoxDefault.Text);
        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            // find TextBox
            TextBox tBox = this.FindName("tBoxNew") as TextBox;

            listBox1.Items.Add(tBox.Text);
        }

        private void btnAddNewCtrls_Click(object sender, RoutedEventArgs e)
        {
            // add a new horizontal StackPanel to stPnlMain
            StackPanel stPnl = new StackPanel();
            stPnl.Orientation = Orientation.Horizontal;
            stPnl.Margin = new Thickness(0);
            stPnl.Background = new SolidColorBrush(Colors.LightGray);
            StPnlMain.Children.Add(stPnl);

            // add a textbox to new stackpanel
            TextBox tBox = new TextBox();
            tBox.Width = 50;
            tBox.Margin = new Thickness(5);
            tBox.Name = "tBoxNew";
            stPnl.Children.Add(tBox);
            //register name for textBox
            this.RegisterName(tBox.Name, tBox);

            // add a button to new stackpanel
            Button btn = new Button();
            btn.Width = 80;
            btn.Height = 23;
            btn.Margin = new Thickness(5);
            btn.Content = "newButton";
            btn.Name = "btnNew";
            btn.ToolTip = "Add text in textBox to ListBox";
            stPnl.Children.Add(btn);
            //register name for textBox
            this.RegisterName(btn.Name, btn);

            btnAddNewCtrls.IsEnabled = false;
        }

        private void btnAddEventHandler_Click(object sender, RoutedEventArgs e)
        {
            // find new button
            Button btn = this.FindName("btnNew") as Button;
            btn.Click += btnNew_Click;
        }

        private void btnRemoveEventHandler_Click(object sender, RoutedEventArgs e)
        {
            // find new button
            Button btn = this.FindName("btnNew") as Button;
            btn.Click -= btnNew_Click;
        }
    }

}
