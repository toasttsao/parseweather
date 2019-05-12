using System;
using System.Threading;
using System.Threading.Tasks;
using Parseweather.Jobs;
using Quartz;
using Quartz.Impl;

namespace Parseweather
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Console Start");
            var schedulerFactory = new StdSchedulerFactory();
            var schedule = await schedulerFactory.GetScheduler();

            var job = JobBuilder.Create<ParseWeather>()
                .WithIdentity("ParseWeatherJob")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithCronSchedule("0 0/1 * * * ?") // 每一分鐘觸發一次。
                .WithIdentity("ParseWeatherTrigger")
                .Build();

            // 把工作加入排程
            await schedule.ScheduleJob(job, trigger);

            // 啟動排程器
            await schedule.Start();

            SpinWait.SpinUntil(() => false);
        }
    }
}