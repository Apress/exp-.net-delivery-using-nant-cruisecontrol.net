using System;
using System.Collections;
using System.Configuration;

using Etomic.VSSManager.Remote;

namespace Etomic.VSSManager.Server
{
	/// <summary>
	/// A concrete implementation of the IVssManager interface.
	/// This object is remotable.
	/// </summary>
	public class VssManager : MarshalByRefObject, IVssManager
	{
		#region IVssManager Members

		/// <summary>
		/// Returns and IList of string objects representing the friendly names of available VSS Databases.
		/// </summary>
		/// <returns>An IList of string objects.</returns>
		public IList GetDatabaseNames()
		{
			ArrayList dbList;
			dbList = ConfigurationSettings.GetConfig("VSS/dbList") as ArrayList;

			Log.Info("Database name list returned");
			
			return dbList;
		}

		/// <summary>
		/// Returns a connected instance of a VSS Database using the friendly name.
		/// </summary>
		/// <param name="name">The friendly (folder) name of the VSS Database required.</param>
		/// <returns>An connected instance of an IVssDatabase</returns>
		public IVssDatabase GetDatabase(string name)
		{
			Log.Info(String.Format("Database {0} returned.", name));
			return new VssDatabase(name);
		}

		#endregion

	}
}
