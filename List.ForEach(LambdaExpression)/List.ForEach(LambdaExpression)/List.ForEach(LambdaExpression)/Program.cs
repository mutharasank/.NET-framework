using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
namespace List.ForEach_LambdaExpression_
{class Program
{
static void Main()
{
List<String> names = new List<String>();
names.Add("Bruce");
names.Add("Alfred");
names.Add("Tim");
names.Add("Richard");
// Display the contents of the list using the lambda expression.

    names.ForEach(n=> Console.WriteLine(n));
Console.ReadLine();
}

}
}