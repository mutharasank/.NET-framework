﻿#pragma checksum "..\..\Window1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CAE92E5F6A4565154B267B3C5C09D32D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DataBinding {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\Window1.xaml"
        internal System.Windows.Controls.Slider slider;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\Window1.xaml"
        internal System.Windows.Controls.TextBlock lblSampleText;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Window1.xaml"
        internal System.Windows.Controls.TextBox tBoxFontSize;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Window1.xaml"
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button btnRemove;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button brnOneWay;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button btnTwoWay;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button btnOneWayToSource;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BindingTest1;component/window1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.slider = ((System.Windows.Controls.Slider)(target));
            return;
            case 2:
            this.lblSampleText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            
            #line 16 "..\..\Window1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.cmd_SetSmall);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\Window1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.cmd_SetNormal);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 18 "..\..\Window1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.cmd_SetLarge);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tBoxFontSize = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnRemove = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\Window1.xaml"
            this.btnRemove.Click += new System.Windows.RoutedEventHandler(this.btnRemove_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.brnOneWay = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\Window1.xaml"
            this.brnOneWay.Click += new System.Windows.RoutedEventHandler(this.btnOneWay_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnTwoWay = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\Window1.xaml"
            this.btnTwoWay.Click += new System.Windows.RoutedEventHandler(this.btnTwoWay_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnOneWayToSource = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\Window1.xaml"
            this.btnOneWayToSource.Click += new System.Windows.RoutedEventHandler(this.btnOneWayToSource_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
