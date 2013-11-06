using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List.FindIndex_LambdaExpression_
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();
            persons.Add(new Person { Name = "Bent", Age = 25 });
            persons.Add(new Person { Name = "Susan", Age = 34 });
            persons.Add(new Person { Name = "Mikael", Age = 60 });
            persons.Add(new Person { Name = "Klaus", Age = 44 });
            persons.Add(new Person { Name = "Birgitte", Age = 17 });
            persons.Add(new Person { Name = "Liselotte", Age = 9 });

            int i1 = persons.FindIndex(p => p.Age==44);
            int i2 = persons.FindIndex(p => p.Name.StartsWith("S"));
            int i3 = persons.FindIndex(p =>p.Name.Contains("i"));
            int i4 = persons.FindIndex(p => p.Name.Length==p.Age);
            Console.WriteLine(i1);
            Console.WriteLine(i2);
            Console.WriteLine(i3);
            Console.WriteLine(i4);

            //int i2 = persons.FindIndex(4);
            //int i3 = persons.FindIndex(5);

            Console.ReadLine();
        }
    }
}
