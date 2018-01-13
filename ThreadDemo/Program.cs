using System;
using System.Collections.Concurrent;
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
            var threadRipper = new ThreadRipper();
            //threadRipper.ThreadDefault();
            //threadRipper.ThreadForceMainThread();
            //threadRipper.ThreadPriorities();
            //threadRipper.ThreadBackGround();
            threadRipper.ThreadSharingAttribute();

            Console.WriteLine("End");
        }
    }
}
