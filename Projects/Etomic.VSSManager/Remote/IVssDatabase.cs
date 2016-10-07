using System;
using System.Collections;

namespace Etomic.VSSManager.Remote
{
	/// <summary>
	/// A wrapper around the regular COM VSS Database class.
	/// Allows the manipulation of user access.
	/// </summary>
	public interface IVssDatabase
	{
		/// <summary>
		/// The friendly name of the DB.
		/// </summary>
		string Name{get;}
		/// <summary>
		/// An IList of string objects representing the user names in the database.
		/// </summary>
		IList Users{get;}

		/// <summary>
		/// Updates a user to readonly or readwrite depending on the current setting.
		/// </summary>
		/// <param name="user">An IUser object.</param>
		void UpdateReadOnly(IUser user);
		/// <summary>
		/// Updates a user to readonly or readwrite depending on the current setting.
		/// </summary>
		/// <param name="userName">The username as a string.</param>
		void UpdateReadOnly(string userName);
		/// <summary>
		/// Updates all users to 'readonly'. Cannot be used to update all to write access.
		/// </summary>
		void UpdateReadOnlyAll();

		/// <summary>
		/// Replaces existing database users with a standard setting.
		/// </summary>
		void SetStandardUsers();

		/// <summary>
		/// Adds a user to the current sourcesafe database with the default password.
		/// </summary>
		/// <param name="username">The username for the user</param>
		/// <param name="readOnly">Whether the user should have readonly priviliges</param>
		void AddUser(string username, bool readOnly);
	}
}
