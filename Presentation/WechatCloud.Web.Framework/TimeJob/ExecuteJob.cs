using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatCloud.Web.Framework.TimeJob
{
    public class ExecuteJob:Job
    {
        [Invoke(Begin = "2021-03-20 03:00", Interval = 24*60*60, SkipWhileExecuting = true)]
        public void Run()
        {
            Console.WriteLine(DateTime.Now.ToString() + ",TestJob run...");
        }
    }
}
