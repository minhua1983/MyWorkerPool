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
        /// <summary>
        /// 单例对象
        /// </summary>
        static WorkerPool workPool = null;

        /// <summary>
        /// lock帮助对象
        /// </summary>
        static object lockHelper = new object();

        /// <summary>
        /// 队列对象
        /// </summary>
        ConcurrentQueue<Worker> workerQueue = new ConcurrentQueue<Worker>();

        /// <summary>
        /// 初始化若干个工作者实例，私有构造避免被意外实例化
        /// </summary>
        /// <param name="number"></param>
        WorkerPool(int number)
        {
            number = number > 100 ? 10 : number;
            for (int i = 0; i < number; i++)
            {
                Worker worker = new Worker();
                workerQueue.Enqueue(worker);
            }
        }

        /// <summary>
        /// 获取工作者实例
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 工作者对象队列中获取一个闲置状态的工作者对象
        /// </summary>
        /// <returns></returns>
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
