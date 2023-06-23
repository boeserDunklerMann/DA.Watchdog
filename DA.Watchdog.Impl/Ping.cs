using DA.Watchdog.Contracts;
using DA.Watchdog.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DA.Watchdog.Impl
{
	public class Ping : IWatcher
	{
		//private Observable observable;
		//private WatchdogContext ctx;
		//public void Init(Observable obs, WatchdogContext context)
		//{
		//	ctx = context;
		//	observable = obs;
		//}

		public async Task<bool?> CheckAsync(bool autoSaveResult, Guid observableID)
		{
			Observable observable;
			using (WatchdogContext ctx = new WatchdogContext())
			{
				observable = ctx.Observable.Where(o => o.ObservableId == observableID)
					.Include(x => x.ObservableMeta).First();
				if (observable.ObservableMeta.Active.Value)
				{
					System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
					PingOptions pingOptions = new PingOptions();
					pingOptions.DontFragment = true;

					// Create a buffer of 32 bytes of data to be transmitted.
					string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
					byte[] buffer = Encoding.ASCII.GetBytes(data);
					int timeout = 120;
					PingReply reply = await ping.SendPingAsync(observable.ObservableMeta.Hostname, timeout, buffer, pingOptions);

					Check check = new Check { CheckId = Guid.NewGuid(), TimeStamp = DateTime.Now, Success = reply.Status == IPStatus.Success };
					observable.Check.Add(check);
					if (autoSaveResult)
						await ctx.SaveChangesAsync();
					return check.Success;
				}
			}
			return null;
		}
	}
}