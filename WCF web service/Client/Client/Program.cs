using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Console.WriteLine(client.WelComeMessage("yordan"));
            Console.WriteLine(client.GetData(44));
            Console.ReadLine();
          
        }
    }
}
