using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Console;

namespace ThreadDemo
{
    internal class ThreadRipper
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
            thread.IsBackground = true;
            thread.Start();
        }

        public void ThreadSharingAttribute()
        {
            try
            {
                for (var i = 0; i < 10; i++)
                {
                    var thread = new Thread(() => 
                    {
                        WriteLine(ThreadSharer.Sharer++);
                        var currentThread = Thread.CurrentThread;
                        WriteLine(currentThread.ManagedThreadId);
                        Thread.Sleep(1000);
                        currentThread.Abort();
                    });
                    thread.Start();
                    thread.Abort();
                    thread.Join();
                    //threadSharer.Shar++;
                }
            }
            catch (ThreadAbortException)
            {

            }
            
            WriteLine(ThreadSharer.Sharer);
        }

        #region private methods

        private void WriteHello()
        {
            WriteLine("Hello");
        }

        private static void WritePriority(object threadPriority)
        {
            foreach(var _ in Enumerable.Range(0,10))
            {
                WriteLine("Prioritized: " + threadPriority);
                //Thread.Sleep(100);
            }
        }

        private Thread CreateDefaultThread()
        {
            var threadStart = new ThreadStart(WriteHello);
            var thread = new Thread(threadStart);
            return thread;
        }

        private static Thread CreatePriorityThread(ThreadPriority threadPriority)
        {
            var threadStart = new ParameterizedThreadStart(WritePriority);
            var thread = new Thread(threadStart)
            {
                Priority = threadPriority
            };
            return thread;
        }

        #endregion
    }

    internal class ThreadSharer
    {
        [ThreadStatic]
        private static int _sharer;

        public static int Sharer { get => _sharer; set => _sharer = value; }
    }

    internal class ThreadInitializer
    {
        private static ThreadLocal<int> _field = new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);

        public void MultiThread()
        {
            var waitThreads = new List<WaitHandle>();

            for (var i = 0; i < 10; i++)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);

                var thread = new Thread(() =>
                {
                    Console.WriteLine("Thread id: " + _field.Value);
                    handle.Set();
                });

                waitThreads.Add(handle);
                thread.Start();
            }

            WaitHandle.WaitAll(waitThreads.ToArray());
        }
    }
}
