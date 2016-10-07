using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Messaging;

using Etomic.VSSManager.Server;

namespace Etomic.VSSManager.Service
{
	public class VssManagerService : System.ServiceProcess.ServiceBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public static readonly string SERVICE_NAME = "Vss Manager Service";

		public VssManagerService()
		{
			InitializeComponent();
		}

		// The main entry point for the process
		static void Main()
		{
			ServiceBase.Run(new VssManagerService());
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = SERVICE_NAME;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

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
		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			Log.Info("Service Stopped");
		}
	}
}
