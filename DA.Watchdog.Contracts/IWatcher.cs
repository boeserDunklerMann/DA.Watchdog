using DA.Watchdog.Model;
using System;
using System.Threading.Tasks;

namespace DA.Watchdog.Contracts
{
	public interface IWatcher
	{
		//void Init(Observable obs, WatchdogContext context);
		Task<bool?> CheckAsync(bool autoSaveResult, Guid observableID);
	}
}