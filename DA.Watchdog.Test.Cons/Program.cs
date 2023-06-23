using DA.Watchdog.Commons;
using DA.Watchdog.Contracts;
using DA.Watchdog.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DA.Watchdog.Test.Cons
{
	internal class Program
	{
		static void Main(string[] args)
		{
			IWatcher watcher = ContractBinder.GetObject<IWatcher>();
			using(WatchdogContext ctx = new WatchdogContext())
			{
				var obs = ctx.Observable.Include(x => x.ObservableMeta).ToList();
				obs.ForEach(x =>
				{
					watcher.Init(x, ctx);
					bool? result = watcher.CheckAsync(true).Result;
				});
			}
		}
	}
}
