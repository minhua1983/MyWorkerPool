using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkerPool.Core.Models
{
    public class Worker
    {
        /// <summary>
        /// 是否处于空闲状态
        /// </summary>
        public bool IsIdle { get; set; } = true;

        /// <summary>
        /// 执行action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="obj"></param>
        public void Process(Action<object> action,object obj)
        {
            Task t = new Task(action, obj);
            t.ContinueWith(task =>
            {
                IsIdle = true;
            });
            t.Start();

            /*不带参数action
            Task.Run(.ContinueWith(t =>
            {
                IsIdle = true;
            });
            //*/
        }
    }
}
