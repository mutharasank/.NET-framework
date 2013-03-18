using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericGrowableArray
{
    class Program
    {
        static void Main(string[] args)
        {
            GrowableIndexer<String> g = new GrowableIndexer<String>();
            g[0] = "d";
            g[1] = "a";
            g[2] = "g";
            g[3] = "q";
            g[4] = "hh";
            g[5] = "dd";
            g[7] = "aa";
            g[9] = "hhh";
            g[8] = "element at index 8";
            for (int a = 0; a < 10; a++)
            {
                Console.WriteLine(g[a]);
            }
            Console.ReadLine();
        }
        class GrowableIndexer<T>
        {
            private T[] items = new T[5];
            public T this[int i]
            {
                get
                {
                    if (i < items.Length && i >= 0)
                    {
                        return items[i];
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("get  items " + i);
                    }
                }
                set
                {
                    if (i < items.Length)
                    {
                        items[i] = value;
                    }
                    else if (i >= items.Length)
                    {
                        T[] temp = new T[i + 1];
                        temp[i] = value;
                        items.CopyTo(temp, 0);
                        items = temp;
                    }
                    if (i < 0)
                    {
                        throw new IndexOutOfRangeException("set  items " + i);
                    }

                }
            }
        }
    }
}
