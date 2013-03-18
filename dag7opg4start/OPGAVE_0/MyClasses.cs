using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace dag7opg4
{
  public class Car
  {
    public string Name { get; set; }
    public string Desc { get; set; }
    public ImageSource Image { get; set; }
  }

  public class Service
  {
    static Service service = new Service();
    List<Car> cars = new List<Car>();

    private Service()
    {
      string path = Directory.GetCurrentDirectory();
      try
      {
        cars.Add(new Car { Name = "BMW", Desc = "An image of sport", Image = new BitmapImage(new Uri(path + @"\bmw3.png")) });
        cars.Add(new Car { Name = "Audi", Desc = "Italian car for enthusiasts", Image = new BitmapImage(new Uri(path + @"\alfa.png")) });
        cars.Add(new Car { Name = "Mercedes", Desc = "Classic German car manufactor", Image = new BitmapImage(new Uri(path + @"\mercedes.png")) });
        cars.Add(new Car { Name = "Aston Martin DB6", Desc = "Bond's car in Goldfinger", Image = new BitmapImage(new Uri(path + @"\db6.png")) });
        cars.Add(new Car { Name = "Jaguar E type", Desc = "A legend from Jaguar", Image = new BitmapImage(new Uri(path + @"\jaguar_e.png")) });
      }
      
      catch (Exception ex)
      {
        System.Windows.MessageBox.Show("FEJL: "+ex.Message);
      }
    }

    public static Service Instance { get { return service; } }

    public List<Car> Cars { get { return cars; } }
  }
}
