using System;
using System.Collections;

namespace Etomic.VSSManager.Remote
{
	/// <summary>
	/// The IVssManager object provides methods to obtain database information and an IVssDatabase object itself.
	/// </summary>
	public interface IVssManager
	{
		/// <summary>
		/// Returns a connected instance of a VSS Database using the friendly name.
		/// </summary>
		/// <param name="name">The friendly (folder) name of the VSS Database required.</param>
		/// <returns>An connected instance of an IVssDatabase</returns>
		IVssDatabase GetDatabase(string name);
		/// <summary>
		/// Returns and IList of string objects representing the friendly names of available VSS Databases.
		/// </summary>
		/// <returns>An IList of string objects.</returns>
		IList GetDatabaseNames();
	}
}
