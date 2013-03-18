using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStruct
{
    public struct Time
    {
        int minutes;


        public Time(String s)
        {
            minutes = 0;

            int m;
            if (int.TryParse(s.Substring(0, 2), out m))
            {
                Hour = m;
            }
            else
                throw new ArgumentException("Not a number");

            if (int.TryParse(s.Substring(3, 2), out m))
            {
                Minutes = m;
            }
            else
                throw new ArgumentException("Not a number");

        }
        public Time(int hour, int min)
        {
            minutes = 0;
            Hour = hour;
            Minutes = min;
        }
        public override string ToString()
        {

            return string.Format("{0:d2}:{1:d2}", Hour, Minutes);
        }
        public int Hour
        {
            get { return minutes / 60; }
            set
            {
                if (value < 0 || value > 23)
                    throw new ArgumentException("Value is illegal");
                minutes = Minutes + value * 60;
            }
        }

        public int Minutes
        {
            get { return minutes % 60; }
            set
            {
                if (value < 0 || value > 59)
                    throw new ArgumentException("Value is illegal");
                else
                    minutes = Hour * 60 + value;
            }


        }





        static void Main(string[] args)
        {
            Time t1, t2;
            t1 = new Time(15, 30);
            t2 = t1;
            t2.Hour = t2.Hour + 2;
            Console.WriteLine(t1.ToString());
            Console.WriteLine(t2.ToString());
            Console.ReadLine();
        }
    }
}
