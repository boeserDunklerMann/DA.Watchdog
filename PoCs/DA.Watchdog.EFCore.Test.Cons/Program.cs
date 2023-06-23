using DA.Watchdog.EFCore.Test.Cons.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DA.Watchdog.EFCore.Test.Cons
{
	internal class Program
	{
		static async Task Main(string[] args)
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
				// now lets create an observable
				Observable o = new Observable { Name = "test", ObservableId=Guid.NewGuid(), Remarks="Test from EFCore" };
				o.ObservableMeta = new ObservableMeta { CreationDate = DateTime.Now };
				c.Observable.Add(o);
				await c.SaveChangesAsync();
				// now lets modify an observable
				Observable o2 = c.Observable.First<Observable>();
				o2.Name = "xyz";
				c.SaveChanges();
			}
		}
	}
}