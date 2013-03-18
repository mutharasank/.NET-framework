using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding_Light2
{
  public class Novel
  {
    public string Author { get; set; }
    public string Title { get; set; }
    public int Rating { get; set; }

    public override string ToString()
    {
      return Author + ": " + Title + " (" + Rating + ")";
    }
  }
}
