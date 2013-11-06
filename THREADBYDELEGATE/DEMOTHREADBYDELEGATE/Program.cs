using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace DemoThreadByDelegate
{
    delegate int BinaryOp(int x, int y);

    class Program
    {
        static int sum;
        //static ManualResetEvent runSignal = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("Main begins on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            BinaryOp caller = Program.Add;

            //Initiate the asynchronious call of Addn (on a thread from the thread pool).
            AsyncResult asyncResult = (AsyncResult) caller.BeginInvoke(10, 20, AddComplete, null);
            
            int i = 1;
            while (!asyncResult.IsCompleted)
            {
                if (i % 5000000 == 0)
                {
                    Console.WriteLine("From main: " + i);
                }
                i++;
            }

            //runSignal.WaitOne();
            //Console.WriteLine("Sum is " + sum);
            Console.ReadLine();
        }

        // Method to execute asynchroniously.
        public static int Add(int x, int y)
        {
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " in method Add() at start");
            Thread.Sleep(200);
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " in method Add() at end");
            return x + y;
        }

        // Method being called when the asynchronius call of Add() is finished.
        // delegate: void AsyncCallBack(IAsyncResult)
        public static void AddComplete(IAsyncResult aRes)
        {
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " in method AddComplete()");
            AsyncResult asyncResult = (AsyncResult)aRes;
            BinaryOp caller = (BinaryOp) asyncResult.AsyncDelegate;
            sum = caller.EndInvoke(asyncResult);
            Console.WriteLine("Result: " + sum);
            //runSignal.Set();
        }
    }
}
