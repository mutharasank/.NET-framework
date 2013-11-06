using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predicate_Delegate
{

    public class Program
    {
       

       
           public   class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Score { get; set; }
            public bool Accepted { get; set; }
            public override string ToString()
            {
                string s = "No";
                if (Accepted)
                    s = "Yes";
                return Name + ", Age=" + Age + ", Score=" + Score + ", accepted=" + s;
            }
        }
        public class Persons
        {
          

             List<Person> PersList = new List<Person>();


            public Persons()
            {
                Init();
            }
            private void Init()
            {

                PersList.Add(new Person { Name = "Bent", Age = 25, Score = 9 });
                PersList.Add(new Person { Name = "Susan", Age = 34, Score = 3 });
                PersList.Add(new Person { Name = "Mikael", Age = 60, Score = 8 });
                PersList.Add(new Person { Name = "Klaus", Age = 44, Score = 7 });
                PersList.Add(new Person { Name = "Birgitte", Age = 18, Score = 10 });
                PersList.Add(new Person { Name = "Liselotte", Age = 16, Score = 9 });
            }


            public void SetAccepted(Predicate<Person>pred)
            {
                foreach (Person person in PersList)
                {
                    if (pred(person))
                        person.Accepted = true;

                }
            }
            public override string ToString()
            {
                string res = "[";
                foreach (Person p in PersList)
                    res += p.ToString() + Environment.NewLine;
                res = res.Remove(res.Length - 2);
                res += "]";
                return res;
            }

        }
       public static void Main(string[] args)
        {
            Persons persons = new Persons();

            Console.WriteLine(persons.ToString());
            Console.WriteLine();
            persons.SetAccepted(p => p.Score >= 6 && p.Age <= 40);

            Console.WriteLine(persons.ToString());
            Console.ReadLine();
        }
    }
    }

  



