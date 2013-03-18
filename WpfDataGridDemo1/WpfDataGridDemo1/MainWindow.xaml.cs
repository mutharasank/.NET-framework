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

namespace WpfDataGridDemo1
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

    private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {
      int r = dataGrid1.SelectedIndex;
      label1.Content = "Row: " + r;
    }

    private void btnBGColor_Click(object sender, RoutedEventArgs e)
    {
      dataGrid1.RowBackground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
      dataGrid1.AlternatingRowBackground = new SolidColorBrush(Color.FromRgb(170, 170, 170));
//
      //dt.DefaultView.RowStateFilter = DataViewRowState.ModifiedCurrent;
    }

    private void btnHideID_Click(object sender, RoutedEventArgs e)
    {
      if (dataGrid1.Columns[0].Visibility != Visibility.Hidden)
        dataGrid1.Columns[0].Visibility = Visibility.Hidden;
      else
        dataGrid1.Columns[0].Visibility = Visibility.Visible;
    }

    private void btnFilter_Click(object sender, RoutedEventArgs e)
    {
      if (dt.DefaultView.RowFilter == "")
        dt.DefaultView.RowFilter = "country='UK'";
      else
        dt.DefaultView.RowFilter = "";
    }

    private void dataGrid1_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
    {
      if (e.Command == DataGrid.DeleteCommand)
      {
        DataRowView rowView=(DataRowView)dataGrid1.SelectedItem;
        string name = (string)rowView.Row["FirstName"] + " " + (string)rowView.Row["LastName"];
         
        MessageBoxResult mbr = MessageBox.Show("Er du sikker på du vil slette \n"+name+"?", "Bekræft", MessageBoxButton.YesNo);
        if (mbr != MessageBoxResult.Yes)
          e.Handled = true;
      }
    }

  }
}
