using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class ThreadRipper
    {
        public void ThreadDefault()
        {            
            //Create threadstart delegate
            var thread = CreateDefaultThread();
            thread.Start();
        }

        public void ThreadForceMainThread()
        {
            var thread = CreateDefaultThread();

            //Force main thread to wait for child thread
            thread.Join();
        }

        public void ThreadPriorities()
        {
            var lowPriorityThread = CreatePriorityThread(ThreadPriority.Lowest);
            var highPriorityThread = CreatePriorityThread(ThreadPriority.Highest);
            lowPriorityThread.Start(ThreadPriority.Lowest);
            highPriorityThread.Start(ThreadPriority.Highest);
            lowPriorityThread.Join();
            highPriorityThread.Join();
        }

        public void ThreadBackGround()
        {
            var thread = CreateDefaultThread();
            //thread.IsBackground = true;
            thread.Start();
        }

        #region private methods

        private void WriteHello()
        {
            Console.WriteLine("Hello");
        }

        private void WritePriority(object threadPriority)
        {
            foreach(var i in Enumerable.Range(0,10))
            {
                Console.WriteLine("Prioritized: " + threadPriority.ToString());
                //Thread.Sleep(100);
            }
        }

        private Thread CreateDefaultThread()
        {
            var threadStart = new ThreadStart(WriteHello);
            var thread = new Thread(threadStart);
            return thread;
        }

        private Thread CreatePriorityThread(ThreadPriority threadPriority)
        {
            var threadStart = new ParameterizedThreadStart(WritePriority);
            var thread = new Thread(threadStart);
            thread.Priority = threadPriority;
            return thread;
        }

        #endregion
    }
}
