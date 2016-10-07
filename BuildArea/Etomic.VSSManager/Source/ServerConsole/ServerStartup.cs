using System;
using System.Configuration;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Messaging;

using Etomic.VSSManager.Remote;
using Etomic.VSSManager.Server;

namespace Etomic.VSSManager.ServerConsole
{
	public class ServerStartup
	{
		static void Main(string[] args)
		{
			Log.Info("Server Starting");
			Log.Info("Registering Channel");
			HttpChannel chnl = new HttpChannel(Int32.Parse((ConfigurationSettings.AppSettings["Channel"].ToString())));
			ChannelServices.RegisterChannel(chnl);

			Log.Info("Registering Service");

			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(VssManager),
				"VssManager.soap",
				WellKnownObjectMode.Singleton);

			Log.Info("Service Started");

			Console.ReadLine();
		}
	}
}
