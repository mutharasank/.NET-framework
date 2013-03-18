using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace delegate_param
{
    public delegate int CompareDelegate(Customer c1, Customer c2);

    public class Customer
    {
        DateTime regDate;
        string name;

        public Customer(DateTime regDate, string name)
        {
            this.regDate = regDate;
            this.name = name;
        }

        public DateTime RegDate
        {
            get { return regDate; }
        }

        public string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return regDate.ToShortDateString() + ", " + name;
        }
    }

    class Program
    {
        // a compare method which matches the CompareDelegate declaration (to compare regdates)
        static int CompRegDate(Customer c1, Customer c2)
        {
            if (c1.RegDate < c2.RegDate)
                return -1;
            else
                if (c1.RegDate == c2.RegDate)
                    return 0;
                else
                    return 1;
        }

        // compare method which matches the CompareDelegate declaration (to compare names)
        static int CompName(Customer c1, Customer c2)
        {
            return c1.Name.CompareTo(c2.Name);
        }


        // general sort method using a CompareDelegate variabel
        static void Sort(Customer[] customers, CompareDelegate comp)
        {
            for (int i = 0; i < customers.Length - 1; i++)
            {
                int mini = i;
                for (int j = i + 1; j < customers.Length; j++)
                    if (comp(customers[mini], customers[j]) > 0) mini = j;

                Customer min = customers[i];
                customers[i] = customers[mini];
                customers[mini] = min;
            }
        }

        static void Main(string[] args)
        {
            Customer[] customers ={ new Customer(new DateTime(2008,2,18),"Mikael"),
                       new Customer(new DateTime(2008,4,2),"Anders"),
                       new Customer(new DateTime(2008,6,25),"Hans"),
                       new Customer(new DateTime(2008,3,8),"Thomas"),
                       new Customer(new DateTime(2008,5,10),"Kim")};

            // 2. parameter is a delegate object cantaining a method
            Sort(customers, new CompareDelegate(CompRegDate)); 

            foreach (Customer k in customers)
                Console.WriteLine(k);

            Console.WriteLine("-----------------------------------");

            // 2. parameter is a method (syntactic sugar for a delegate object)
            Sort(customers, CompName);

            foreach (Customer k in customers)
                Console.WriteLine(k);

            Console.ReadLine();
        }
    }
}
