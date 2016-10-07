using System;
using System.Collections;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace Etomic.VSSManager.Service
{
	[RunInstaller(true)]
	public class VssManagerInstaller : System.Configuration.Install.Installer
	{
		private System.ComponentModel.Container components = null;

		private ServiceInstaller _serviceInstaller;
		private ServiceProcessInstaller _processInstaller;

		public VssManagerInstaller()
		{
			InitializeComponent();

			_processInstaller = new ServiceProcessInstaller();
			_serviceInstaller = new ServiceInstaller();

			_processInstaller.Account = ServiceAccount.LocalSystem;
			_serviceInstaller.StartType = ServiceStartMode.Automatic;
			_serviceInstaller.ServiceName = VssManagerService.SERVICE_NAME;

			Installers.Add(_serviceInstaller);
			Installers.Add(_processInstaller);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
