using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class  MonthlySales
    {

        int[] sales = new int[12];

        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 1: return sales[0];
                    case 2: return sales[1];
                    case 3: return sales[2];
                    case 4: return sales[3];
                    case 5: return sales[4];
                    case 6: return sales[5];
                    case 7: return sales[6];
                    case 8: return sales[7];
                    case 9: return sales[8];
                    case 10: return sales[9];
                    case 11: return sales[10];
                    case 12: return sales[11];

                    default:
                        throw new IndexOutOfRangeException("get Montly Sales " + i);
                }
            }

            set
            {
                switch (i)
                {
                    case 1: sales[0] = value; break;
                    case 2: sales[1] = value; break;
                    case 3: sales[2] = value; break;
                    case 4: sales[3] = value; break;
                    case 5: sales[4] = value; break;
                    case 6: sales[5] = value; break;
                    case 7: sales[6] = value; break;
                    case 8: sales[7] = value; break;
                    case 9: sales[8] = value; break;
                    case 10: sales[9] = value; break;
                    case 11: sales[10] = value; break;
                    case 12: sales[11] = value; break;
                    default:
                        throw new IndexOutOfRangeException("set Montly Sales " + i);
                }
            }


        }
        public int this[string i]
        {
            get
            {
                switch (i)
                {
                    case "Jan": return sales[0];
                    case "Feb": return sales[1];
                    case "Mar": return sales[2];
                    case "Apr": return sales[3];
                    case "May": return sales[4];
                    case "Jun": return sales[5];
                    case "Jul": return sales[6];
                    case "Aug": return sales[7];
                    case "Sep": return sales[8];
                    case "Oct": return sales[9];
                    case "Noe": return sales[10];
                    case "Dec": return sales[11];

                    default:
                        throw new IndexOutOfRangeException("get Monthly Sales " + i);
                }
            }

            set
            {
                switch (i)
                {
                    case "Jan": sales[0] = value; break;
                    case "Feb": sales[1] = value; break;
                    case "Mar": sales[2] = value; break;
                    case "Apr": sales[3] = value; break;
                    case "May": sales[4] = value; break;
                    case "Jun": sales[5] = value; break;
                    case "Jul": sales[6] = value; break;
                    case "Aug": sales[7] = value; break;
                    case "Sep": sales[8] = value; break;
                    case "Oct": sales[9] = value; break;
                    case "Noe": sales[10] = value; break;
                    case "Dec": sales[11] = value; break;

    
                    default:
                        throw new IndexOutOfRangeException("set Monthly Sales " + i);
                }
            }
        }

        static void Main(string[] args)
        {
            MonthlySales m = new MonthlySales();
            m[12] = 24300;
            m["Aug"] = 2400;


            Console.WriteLine(m[12]);
            Console.WriteLine(m["Aug"]);

            Console.ReadLine();
        }
    }
}

 