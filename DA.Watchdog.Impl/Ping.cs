using DA.Watchdog.Contracts;
using DA.Watchdog.Model;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DA.Watchdog.Impl
{
	public class Ping : IWatcher
	{
		private Observable observable;
		private WatchdogContext ctx;
		public void Init(Observable obs, WatchdogContext context)
		{
			ctx = context;
			observable = obs;
		}

		public async Task<bool?> CheckAsync()
		{
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
				return reply.Status == IPStatus.Success;
			}
			return null;
		}
	}
}
