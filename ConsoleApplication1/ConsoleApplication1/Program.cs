using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    class Person
    {
        static void Main(string[] args)
        {
            Person p = new Person("2710912605", new DateTime(2010, 1, 18));
            Console.WriteLine(p.birthDate);
            Console.ReadLine();
        }


        public DateTime birthDate { get; set; }
        private String cpr;

        public Person(String cpr, DateTime date)
        {

            Cpr = cpr;
            birthDate = date;

        }

        public String Cpr
        {

            get { return cpr; }
            set
            {

                if (value.Length > 10 || value.Length < 0)
                {
                    throw new ArgumentException("Illegal length");
                }

                foreach (char c in value)
                {
                    if (!Char.IsDigit(c))
                    {
                        throw new ArgumentException("not a digit");
                    }


                }
                cpr = value;
            }
        }







    }
}
