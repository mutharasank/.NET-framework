using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayFindAll_LambdaExpression_
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = {"Arne", "Mikael", "Erik", "Margrethe", "Stine", 
"Jørn", "Morten", "Søren", "Hanne", "Gert", "Torben", "Karsten", 
"Anne Dorte"};
            Person[] persons ={ new Person { Name = "Bent", Age = 25 },
new Person { Name = "Susan", Age = 34 },
new Person { Name = "Mikael", Age = 60 },
new Person { Name = "Klaus", Age = 44 },
new Person { Name = "Birgitte", Age = 17 },
new Person { Name = "Liselotte", Age = 9 } 
};
            String target = "i";
            string[] matchedNames = Array.FindAll(names, n => n.Contains(target));
            string[] startingWith = Array.FindAll(names, n => n.StartsWith("A"));
            Person[] young = Array.FindAll(persons, p => p.Age < 30);
            Person[] names5 = Array.FindAll(persons, p => p.Name.Length==5);

            foreach (String s in matchedNames)
            {
                Console.WriteLine(s + "" + "Name containing - i"); 

            }
            foreach (String s in startingWith)
            {
                Console.WriteLine(s + "Name starting with  - A");
            }

            foreach (Person s in young)
            {
                Console.WriteLine(s.Name + "Age less than 30");
            }
            foreach (Person s in names5)
            {
                Console.WriteLine(s.Name + " Name lenght = 5");
            }
            Console.ReadLine();
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        

    }
}
