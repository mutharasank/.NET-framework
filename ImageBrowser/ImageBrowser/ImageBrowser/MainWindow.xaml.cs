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
using System.Collections.ObjectModel; // ObserverableCollection<T>
using System.Collections;
using System.Diagnostics;

namespace ImageBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Staff> staff = new ObservableCollection<Staff>();
        ListCollectionView view = null;

        public MainWindow()
        {
            InitializeComponent();

            Staff f = new Staff(Filename: "atm.jpg",Fullname: "Arne", Initials:"ATM");
            
            staff.Add(f);

         staff.Add(new Staff(Filename: "gs.jpg",Fullname:"George",Initials:"GS"));
       //   staff.Add(new Staff(Filename: "haso.jpg"));
            this.prev.Click += prev_Click;
            this.next.Click += next_Click;

            view = (ListCollectionView)CollectionViewSource.GetDefaultView(staff);
            ImageArea.DataContext = staff;
            lbl1.DataContext = staff;
            TextBox1.DataContext = staff;

        }

        void next_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToNext();
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToLast();
            };
        }

        void prev_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToPrevious();
            if (view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToFirst();
            };
        }


    }
    public class MyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Debug.WriteLine("ssssssssssssss");
            //Console.ReadLine();
            //Uri u = new Uri("D:/Downloads/SCHOOL/SEM 4/C# and .net/WPF 3/atm.jpg");

          //  Debug.WriteLine(u.AbsolutePath);
            string filename = (String)value;
            return new BitmapImage(new Uri(@"D:\Downloads\SCHOOL\SEM 4\C# and .net\WPF 3\"+filename));

            //  Console.ReadLine();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var img = value as BitmapImage;
            return img.UriSource.AbsoluteUri;
        }

    }


}
