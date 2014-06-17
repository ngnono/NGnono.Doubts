using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using log4net.Repository.Hierarchy;

namespace NGnono.Doubts.IDisposableDoubts
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            var a = new A();
            a = null;
            Logger.Debug("GC释放前");
            var b = new B();
            Logger.Debug(String.Format("Memory used before collection: {0}", GC.GetTotalMemory(false)));
            GC.Collect();
            Logger.Debug(String.Format("Memory used before collection: {0}", GC.GetTotalMemory(true)));
            Logger.Debug("GC释放后");
            Console.ReadLine();
        }

    }

    public class A : IDisposable
    {
        public A()
        {
            Logger.Debug("创建对象A");
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(A));

        public void Dispose()
        {
            Logger.Debug("a释放了啊");
        }

        //~A()
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(1000);
        //        Logger.Debug("a析构");  
        //    }
            
        //}
    }


    public class B : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(A));
        public void Dispose()
        {
            Logger.Debug("B 释放了啊");
        }
    }
}
