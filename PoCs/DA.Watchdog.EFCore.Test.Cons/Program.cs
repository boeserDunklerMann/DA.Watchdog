using DA.Watchdog.EFCore.Test.Cons.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DA.Watchdog.EFCore.Test.Cons
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			using (WatchdogContext c = new WatchdogContext())
			{
				var checks = c.Check
					//.Where(x => !x.Success)
					.Include(x => x.Observable)
					.ToList();
				//var checks = c.Set<Model.Check>();
				checks.ForEach(check =>
				{
					Console.WriteLine($"Obs: {check.Observable.Name} - when: {check.TimeStamp} - succeeded: {(check.Success ? "yes" : "no")}");
				});
			}
		}
	}
}