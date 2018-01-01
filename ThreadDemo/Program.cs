using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // default thread
            //Create threadstart delegate
            var threadStart = new ThreadStart(WriteHello);
            var thread = new Thread(threadStart);
            thread.Start();

            //Force main thread to wait for child thread
            //thread.Join();

            Console.WriteLine("End");
         }

        public static void WriteHello()
        {
            Console.WriteLine("Hello");
        }
    }
}
