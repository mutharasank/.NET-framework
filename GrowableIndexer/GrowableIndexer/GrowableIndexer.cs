using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowableIndexer
{
    class Program
    {

        static void Main(string[] args)
        {
            GrowableIndexer g = new GrowableIndexer();
            g[0] = 7;
            g[1] = 9;
            g[2] = 13;
            g[3] = 111;
            g[4] = 12;

            g[5] = 233;
            g[7] = 33;
            g[9] = 341;

            for (int a = 0; a < 10; a++)
            {
                Console.WriteLine(g[a]);

            }
            Console.ReadLine();
        }


        class GrowableIndexer
        {


            private int[] items = new int[5];



            public int this[int i]
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
                        int[] temp = new int[i + 1];
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
