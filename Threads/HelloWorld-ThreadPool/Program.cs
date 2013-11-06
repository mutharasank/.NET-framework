using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld_ThreadPool
{
    class Program
    {
        static BackgroundWorker worker = new BackgroundWorker() { WorkerReportsProgress = true };

        static void Main(string[] args)
        {
            //Executing SayHello on ThreadPool thread.
            ThreadPool.QueueUserWorkItem(Program.SayHello, "ThreadPool thread");

            //Executing SayHello on programmer created thread.
            Thread t = new Thread(Program.SayHello);
            t.Start("Programmer created thread");

            //Executing SayHello on asynchronous delegate (thread is from ThreadPool)
            Action<string> del = Program.SayHello;
            del.BeginInvoke("Asynchronous delegate thread", null, null);

            //Executing SayHello with BackGroundWorker (threads are from ThreadPool)
            //If Worker is created on UI-thread, RunWorkerCompleted and ProgressChanged will run on UI-thread.
            worker.DoWork += Program.SayHello;
            //worker.RunWorkerCompleted += Program.AfterSayHello;
            //worker.ProgressChanged += Program.InSayHello;
            worker.RunWorkerAsync("BackgroundWorker thread - DoWork event");

            //Executing SayHello with Parallel.Invoke (thread is from ThreadPool)
            //Parallel.Invoke(new Action(Helper));
            Parallel.Invoke(() => Program.SayHello("Parallel.Invoke"));

            //Execute SayHello with Task.Run (thread from ThreadPool).
            //Task.Run(new Action(() => Program.SayHello("Task.Run")));
            Task.Run(() => Program.SayHello("Task.Run"));

            //Executing SayHello with await (thread is from ThreadPool)
            Program.AwaitHelper();

            //Executing SayHello on main thread.
            Program.SayHello("Main thread");

            Console.ReadLine();
        }

        static void SayHello(object s)
        {
            Console.WriteLine("Thread {0,2} says: Hello World ({1})", Thread.CurrentThread.ManagedThreadId, s);
        }

        static void Helper()
        {
            Program.SayHello("Parallel.Invoke");
        }

        //---------------------------------------------------------------------

        static void SayHello(object sender, DoWorkEventArgs args)
        {
            object s = args.Argument;
            Console.WriteLine("Thread {0,2} says: Hello World ({1})", Thread.CurrentThread.ManagedThreadId, s);
            worker.ReportProgress(50, "Dowork");
            args.Result = "DoWork";
        }

        static void AfterSayHello(object sender, RunWorkerCompletedEventArgs args)
        {
            Console.WriteLine("Thread {0,2} says: {1} has finished", Thread.CurrentThread.ManagedThreadId, args.Result);
        }

        static void InSayHello(object sender, ProgressChangedEventArgs args)
        {
            Console.WriteLine("Thread {0,2} says: {1} is reporting", Thread.CurrentThread.ManagedThreadId, args.UserState);
        }

        //-------------------------------------------------------------------

        private static async void AwaitHelper()
        {
            //await Task.Run(new Action(() => Program.SayHello("Async - await")));
            await Task.Run(() => Program.SayHello("Async-await"));
        }
    }
}
