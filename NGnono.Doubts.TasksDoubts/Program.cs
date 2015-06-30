using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace NGnono.Doubts.TasksDoubts
{



    /// <summary>
    /// 看 IL 会把 部分类编译到一起
    /// </summary>

    class Program
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Program));
        private static bool IsCompaled = false;


        static void Main(string[] args)
        {
            _log.Debug("NGnono.Doubts.TasksDoubts.Main:run");

            Run4Common();


            //Console.ReadLine();

            while (!IsCompaled)
            {
                Thread.Sleep(1000);
            }

            _log.Debug("NGnono.Doubts.TasksDoubts.Main:Ok");
        }


        private static void Run4Common()
        {
            var tasksCount = 10;
            _log.Debug("任务调度开始……");

            var taskFactory = new TaskFactory();
            var cts = new CancellationTokenSource();
            var tasks = new Task<string>[10];

            for (var i = 0; i < tasksCount; i++)
            {
                var i1 = i;
                tasks[i] =
                taskFactory.StartNew(() => Handler(new TaskArgs
                {
                    CancellationToken = cts.Token,
                    Name = i1.ToString(CultureInfo.InvariantCulture)
                }), cts.Token);
            }

            //TaskContinuationOptions.None 有异常会 中断。。。。
            taskFactory.ContinueWhenAll(tasks, CompleteCallback, TaskContinuationOptions.None);

            _log.Debug("任务调度完成……");

        }

        /// <summary>
        /// 这种方式 当主程式 结束时 任务自动停止 不管是否执行完
        /// </summary>
        private static void Run()
        {

            var tasksCount = 10;

            var taskFactory = new TaskFactory();
            var tasks = new Task<int>[10];
            var cts = new CancellationTokenSource();
            _log.Debug("任务调度开始……");
            for (var i = 0; i < tasksCount; i++)
            {
                var i1 = i;
                taskFactory.StartNew(() => Handler(new TaskArgs
                    {
                        CancellationToken = cts.Token,
                        Name = i1.ToString(CultureInfo.InvariantCulture)
                    }), cts.Token);
            }

            _log.Debug("任务调度完成……");

        }

        private static string Handler(TaskArgs args)
        {
            var r = new Random(unchecked((int)DateTime.Now.Ticks));
            var s = r.Next(1000, 5000);
            _log.Debug(String.Format("任务,{0}.start", args.Name));
            _log.Debug(String.Format("任务,{0}.sleep,{1}", args.Name, s.ToString()));
            //5秒
            Thread.Sleep(s);
            _log.Debug(String.Format("任务,{0}.end", args.Name));

            //if (args.Name == "4")
            //{
            //    throw new NullReferenceException("错误了抛个异常");
            //}


            return args.Name;
        }

        private static void CompleteCallback(Task<string>[] tasks)
        {
            try
            {
                foreach (var task in tasks)
                {
                    _log.Info(String.Format("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}", task.IsCanceled, task.IsCompleted, task.IsFaulted));

                    _log.Info(String.Format("任务的返回值为：{0}", task.Result));
                }
            }
            catch (AggregateException e)
            {
                e.Handle((err) => err is OperationCanceledException);
            }
            _log.Debug("所有任务已完成！");

            IsCompaled = true;
        }


        private class TaskArgs
        {
            public CancellationToken CancellationToken { get; set; }

            public string Name { get; set; }
        }
    }





}
