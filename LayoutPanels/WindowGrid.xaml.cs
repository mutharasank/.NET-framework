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
using System.Windows.Shapes;

namespace LayoutPanels
{
  /// <summary>
  /// Interaction logic for WindowGrid.xaml
  /// </summary>
  public partial class WindowGrid : Window
  {
    // eksperimentér med:
    //
    // * ved Width/Height i ColumnDefinition og RowDefinition angiver 
    // at bredde/højde ændres ved rezise. Prøv at sætte og fjerne stjerner
    // Width og height kan også sætes til Auto

    public WindowGrid()
    {
      InitializeComponent();
    }
  }
}
