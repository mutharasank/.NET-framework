using System;
using System.Collections.Generic;
using System.Text;


namespace DelegateEks1
{
    public delegate void TempEventHandler(int temp);
    public delegate void AlarmEventHandler(int temp);

    public delegate void WeatherDataHandler(int temp, int wind, int pressure);

    // Weather station that measures temperature and notifies listeners.
    // Will be subject in the Observer pattern.
    public class WeatherStation
    {
        private int temp = 20;
        private int wind;
        private int pressure;
        private Random rnd = new Random();

        // NewTemp delegate field
        public TempEventHandler NewTemp; //public, Ugly!!
        
       
        //Alarm event
        public event AlarmEventHandler Alarm;
        public event WeatherDataHandler NewTemp3;

        // Measure new temperature and notify listeners.
        public void RaiseNewTempEvent()
        {
            //   temp += rnd.Next(11) - 5;
            temp += 6;
            if (NewTemp != null)
                NewTemp(temp); //uses hidden private TempEventHandler delegate
            if (Alarm != null&& temp >35)
                Alarm(temp);


            //  Console.WriteLine(NewTemp2 + "ssssssssssssssssssssssssssss");
        }

        public void GetWeatherData()
        {
            this.temp = 25;
            this.wind = 40;
            this.pressure = 960;
            if (NewTemp3 != null)
                NewTemp3(temp, wind, pressure);
        }


        public class CombinedDisplay
        {
            private static int nr = 0;
            private int displayNr = 0;
            private int currentTemp;
            private int pressure;
            private int wind;

            public CombinedDisplay()
            {
                nr++;
                displayNr = nr;
            }
            public void Display(int temp, int wind, int press)
            {
                this.currentTemp = temp;
                this.wind = wind;
                this.pressure = press;

                Console.WriteLine("Temperature= " + currentTemp + "" + wind + " " + pressure);
            }
        }


        // MinMaxDisplay gets a new temperature from WeatherStation and prints actual, min and max temperature.
        // Will be observer in the Observer pattern.
        public class MinMaxDisplay
        {
            private static int nr = 0;
            private int displayNr;
            private int currentTemp = 0;
            private int minTemp = int.MaxValue;
            private int maxTemp = int.MinValue;

            public MinMaxDisplay()
            {
                nr++;
                displayNr = nr;
            }

            // Method that matches TempEventHandler delegate.
            public void Display(int temp)
            {
                currentTemp = temp;
                if (temp < minTemp) minTemp = temp;
                if (temp > maxTemp) maxTemp = temp;

                Console.Write("MinMax display no. " + displayNr + ":  ");
                Console.WriteLine("Temperature= {0:D}, Min= {1:F1}, Max= {2:F1}", currentTemp, minTemp, maxTemp);
            }
        }

        // AverageDisplay gets new a temperature from WeatherStation and prints actual and average temperature.
        // Will be observer in the Observer pattern.
        public class AverageDisplay
        {
            private static int nr = 0;
            private int displayNr;
            int currentTemp = 0;
            int antal = 0;
            int sum = 0;

            public AverageDisplay()
            {
                nr++;
                displayNr = nr;
            }

            // Method that matches TempEventHandler delegate.
            public void Display(int temp)
            {
                currentTemp = temp;
                antal++;
                sum += temp;

                Console.Write("Average display no. " + displayNr + ":  ");
                Console.WriteLine("Temperature= {0:D}, Average= {1:F1}", currentTemp, (sum + 0.0) / antal);
            }
        }
        //Observer of alarm events
        public class AlarmDisplay
        {
            
            public void DisplayAlarm(int temp)
            {
                
                {
                    Console.WriteLine("HEAT ALARM !! Temp is now "+ temp);
                }
            }
        }



        class Program
        {
            static void Main(string[] args)
            {
                WeatherStation weatherStation = new WeatherStation();

                Console.Write("Number of MinMax displays: ");
                int n1 = Convert.ToInt32(Console.ReadLine());
                for (int i = 1; i <= n1; i++)
                {
                    MinMaxDisplay mm = new MinMaxDisplay();
                   
                    CombinedDisplay c = new CombinedDisplay();

                    weatherStation.NewTemp3 += c.Display;

                    // Add the mm.Display method to the TempEventHandler delegate in WeatherStation.
                    weatherStation.NewTemp += mm.Display;
                   
                }
                AlarmDisplay ad = new AlarmDisplay();
                weatherStation.Alarm += ad.DisplayAlarm;
                CombinedDisplay c = new CombinedDisplay();
                weatherStation.NewTemp3 += c.Display;
                Console.Write("Number of Average displays: ");
                int n2 = Convert.ToInt32(Console.ReadLine());
                for (int i = 1; i <= n2; i++)
                {
                   
                    AverageDisplay av = new AverageDisplay();
            
                    // Add the av.Display method to the TempEventHandler delegate in WeatherStation.
                    weatherStation.NewTemp += av.Display;
                  
                }
              

                weatherStation.RaiseNewTempEvent(); Console.WriteLine();
                weatherStation.GetWeatherData(); Console.WriteLine();
                  weatherStation.RaiseNewTempEvent(); Console.WriteLine();
                 weatherStation.GetWeatherData(); Console.WriteLine();
                 weatherStation.RaiseNewTempEvent(); Console.WriteLine();
                 weatherStation.GetWeatherData(); Console.WriteLine();
                 weatherStation.RaiseNewTempEvent(); Console.WriteLine();
                weatherStation.GetWeatherData(); Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
