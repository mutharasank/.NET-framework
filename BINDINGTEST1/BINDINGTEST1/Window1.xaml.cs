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

namespace DataBinding
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void cmd_SetSmall(object sender, RoutedEventArgs e)
    {
      lblSampleText.FontSize = 6;
    }

    private void cmd_SetNormal(object sender, RoutedEventArgs e)
    {
      lblSampleText.FontSize = 12;
    }

    private void cmd_SetLarge(object sender, RoutedEventArgs e)
    {
      // Only works in two-way mode.
      slider.Value = 30;
    }

    private void btnRemove_Click(object sender, RoutedEventArgs e)
    {
      BindingOperations.ClearBinding(lblSampleText, TextBlock.FontSizeProperty);
//        BindingOperations.ClearAllBindings(lblSampleText);
 //       BindingOperations.ClearAllBindings(tBoxFontSize);
    }

    private void btnOneWay_Click(object sender, RoutedEventArgs e)
    {
      Binding binding = new Binding();
      binding.Source = slider;
      binding.Path = new PropertyPath("Value");
      binding.Mode = BindingMode.OneWay;
      lblSampleText.SetBinding(TextBlock.FontSizeProperty, binding);
    }

    private void btnTwoWay_Click(object sender, RoutedEventArgs e)
    {
      Binding binding = new Binding();
      binding.Source = slider;
      binding.Path = new PropertyPath("Value");
      binding.Mode = BindingMode.TwoWay;
      lblSampleText.SetBinding(TextBlock.FontSizeProperty, binding);
    }

    private void btnOneWayToSource_Click(object sender, RoutedEventArgs e)
    {
        Binding binding = new Binding();
        binding.Source = slider;
        binding.Path = new PropertyPath("Value");
        binding.Mode = BindingMode.OneWayToSource;
        lblSampleText.SetBinding(TextBlock.FontSizeProperty, binding);
    }
  }
}
