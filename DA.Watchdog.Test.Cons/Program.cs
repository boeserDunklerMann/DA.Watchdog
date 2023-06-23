using DA.Watchdog.Commons;
using DA.Watchdog.Contracts;
using DA.Watchdog.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Hangfire;
using System;

namespace DA.Watchdog.Test.Cons
{
	internal class Program
	{
		static void Main(string[] args)
		{
			GlobalConfiguration.Configuration
				.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
				.UseSimpleAssemblyNameTypeSerializer()
				.UseRecommendedSerializerSettings()
				.UseColouredConsoleLogProvider()
				.UseSqlServerStorage("Database=Hangfire;Integrated Security=true");

			var hfClient = new BackgroundJobClient();
			using (WatchdogContext ctx = new WatchdogContext())
			{
				var obs = ctx.Observable.Include(x => x.ObservableMeta).ToList();
				obs.ForEach(x =>
				{
					IWatcher watcher = ContractBinder.GetObject<IWatcher>();
					watcher.Init(x, ctx);
					hfClient.Enqueue(() => watcher.CheckAsync(true));
				});
				// all jobs enqueued for one-time-execution, now start JobServer
				using (var hfServer = new BackgroundJobServer())
				{
					Console.ReadLine();
				}
			}
		}
	}
}
