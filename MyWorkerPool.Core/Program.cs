using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MyWorkerPool.Core.Models;

namespace MyWorkerPool.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerPool pool = WorkerPool.GetInstance(10);
            for (int i = 0; i < 20; i++)
            {
                Worker worker = pool.GetWorker();
                if (worker != null)
                {
                    worker.Process((o) =>
                    {
                        Console.WriteLine("worker" + o + " is working.");
                        Thread.Sleep(3000);
                        Console.WriteLine("worker" + o + " is done.");
                    }, i);
                }
                else
                {
                    Console.WriteLine("worker" + i + " is not available.");
                }
            }
            /*
            Thread.Sleep(1000);

            for (int i = 20; i < 40; i++)
            {
                Worker worker = pool.GetWorker();
                if (worker != null)
                {
                    worker.Process((o) =>
                    {
                      Console.WriteLine("worker" + o + " is working.");
                      Thread.Sleep(3000);
                      Console.WriteLine("worker" + o + " is done.");
                    }, i);
                }
                else
                {
                    Console.WriteLine("worker" + i + " is not available.");
                }
            }
            //*/
            Console.ReadLine();
        }
    }
}
