using DA.Watchdog.Contracts;
using DA.Watchdog.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DA.Watchdog.Commons
{
	/// <ChangeLog>
	/// <Create Datum="04.08.2021" Entwickler="DA" />
	/// </ChangeLog>
	public static class ContractBinder
	{
		private static HashSet<Tuple<Type, Type>> _container;
		private static int _nCounter = 0;

		static ContractBinder()
		{
			_container = new HashSet<Tuple<Type, Type>>();
		}

		private static void Bind(Type InterfaceType, Type ImplementationType)
		{
			Tuple<Type, Type> item = new Tuple<Type, Type>(InterfaceType, ImplementationType);
			_container.Add(item);
			_nCounter++;
		}

		public static void CreateBindings()
		{
			Bind(typeof(IWatcher), typeof(Ping));
		}

		public static TInterface GetObject<TInterface>()
		{
			if (0 == _nCounter)
				CreateBindings();
			foreach (Tuple<Type, Type> bindings in _container)
			{
				if (bindings.Item1.Equals(typeof(TInterface)))
				{
					return (TInterface)Activator.CreateInstance(bindings.Item2);
				}
			}
			return default(TInterface);
		}
	}
}
