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
using System.Windows.Shapes;

namespace LayoutPanels
{
  /// <summary>
  /// Interaction logic for WindowStackPanel.xaml
  /// </summary>
  public partial class WindowStackPanel : Window
  {
    // eksperimenter med:
    //
    // textblock.textalignment
    // buttons: sæt width til tal eller auto eller slet width-tag'et helt
    // Button: height
    // Button: margin
    // Button: padding, kræver height, width ændres eller slettes

    public WindowStackPanel()
    {
      InitializeComponent();
    }
  }
}
