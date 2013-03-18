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
using System.Data;
using System.Data.SqlClient;

namespace WpfDataGridDemo2
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

    DataTable dt = null;

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      string constr = @"data source=localhost\SQLEXPRESS2005; database=northwind; integrated security=true";

      SqlConnection con = new SqlConnection(constr);

      string sql = "select * from employees";
      SqlDataAdapter da = new SqlDataAdapter(sql, con);
      dt = new DataTable();
      da.Fill(dt);

      dataGrid1.DataContext = dt.DefaultView;
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
        //DataRowView row=dataGrid1.SelectedItem as DataRowView;

        dataGrid1.Columns[0].Visibility=Visibility.Hidden;
    }

    private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
    {
        Hyperlink link = e.OriginalSource as Hyperlink;
        System.Diagnostics.Process.Start(link.NavigateUri.AbsoluteUri);
    }
  }
}
