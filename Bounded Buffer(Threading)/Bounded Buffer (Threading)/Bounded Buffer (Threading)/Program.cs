using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bounded_Buffer__Threading_
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Program starts");
            Buffer buf = new Buffer();
            Thread t1 = new Thread(buf.Produce);
            Thread t2 = new Thread(buf.Produce);
            Thread t3 = new Thread(buf.Consume);
            t1.Start();
            t2.Start();
            t3.Start();
            
            Console.WriteLine("Program ends");
            Console.ReadLine();
        }
    }

        public class Buffer
        {
            List<string> buffer= new List<string>();

            public void Put(string s)
            {
                lock (this)
                {
                    while (buffer.Count == 5)
                    {
                        Monitor.Wait(this);
                    }
                    buffer.Add(s);
                    Trace("Put", s);
                    Monitor.PulseAll(this);
                }
            }
            public void Get()
            {
                lock (this)
                {
                    while (buffer.Count == 0)
                    {
                        Trace("Get waits", "");
                        Monitor.Wait(this);
                       
                    }
                    String s = buffer[0];
                    buffer.RemoveAt(0);
                    Trace("Get", s);
                    Monitor.PulseAll(this);
                }
            }
            public void Produce()
            {
                for(int i=0;i<10;i++)
                {
                     string rs=this.RandomString();
                this.Put(rs);
                Thread.Sleep(1000);
                }
            
            }
            public void Consume()
            {
                for (int i = 0; i < 20; i++)
                {
                    this.Get();
                    Thread.Sleep(1000);
                }
            }
            private string RandomString()
            {
                return "a";
            }
            private void Trace(string method, string s)
            {
                Console.WriteLine("Thread {0}: {1} with {2},size is {3}", Thread.CurrentThread.ManagedThreadId, method,s,buffer.Count);
            }
        }
    }




