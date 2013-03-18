using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opg3Start
{
    public class Car
    {
        string name;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        int km;
        public int Km
        {
            get { return km; }
            set { km = value; }
        }

        int serviceKm;
        public int ServiceKm
        {
            get { return serviceKm; }
            set { serviceKm = value; }
        }

        public override string ToString()
        {
            return name+" "+km+" "+serviceKm;
        }
    }

    public class Service {
    private static Service service = null;

    List<Car> cars = new List<Car>();
    public List<Car> Cars
    {
      get { return cars; }
    }

    private Service() 
    {
      cars.Add(new Car { Name="BMW 320", Km=167345, ServiceKm=165000 });
      cars.Add(new Car { Name = "Mercedes B", Km = 1645, ServiceKm = 30000 });
      cars.Add(new Car { Name = "VW Golf", Km = 82843, ServiceKm = 90000 });
      cars.Add(new Car { Name = "Ford Focus", Km = 63400, ServiceKm = 60000 });
      cars.Add(new Car { Name = "Opel Corsa", Km = 132582, ServiceKm = 150000 });
    }

    public static Service Instance
    {
      get
      {
        if (service == null)
          service = new Service();

        return service;
      }
    }
  }
}
