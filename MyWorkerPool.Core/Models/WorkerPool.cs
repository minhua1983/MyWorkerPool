using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkerPool.Core.Models
{
    public class WorkerPool
    {
        static WorkerPool workPool = null;
        static object lockHelper = new object();
        //ConcurrentQueue<Worker> workingWorkerQueue = new ConcurrentQueue<Worker>();
        //ConcurrentQueue<Worker> idleWorkerQueue = new ConcurrentQueue<Worker>();
        ConcurrentQueue<Worker> workerQueue = new ConcurrentQueue<Worker>();
        WorkerPool(int number)
        {
            number = number > 100 ? 10 : number;
            for (int i = 0; i < number; i++)
            {
                Worker worker = new Worker();
                workerQueue.Enqueue(worker);
            }
        }

        public static WorkerPool GetInstance(int number = 10)
        {
            if (workPool == null)
            {
                lock (lockHelper)
                {
                    if (workPool == null)
                    {
                        workPool = new WorkerPool(number);
                    }
                }
            }
            return workPool;
        }

        public Worker GetWorker()
        {
            Worker worker = null;
            if (workerQueue.Where(w => w.IsIdle).FirstOrDefault() != null)
            {
                lock (lockHelper)
                {
                    if (workerQueue.Where(w => w.IsIdle).FirstOrDefault() != null)
                    {
                        worker = workerQueue.Where(w => w.IsIdle).FirstOrDefault();
                        if (worker != null)
                        {
                            worker.IsIdle = false;
                        }
                    }
                }
            }
            return worker;
        }
    }
}
