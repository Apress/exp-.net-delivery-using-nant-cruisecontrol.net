using System;
using System.Collections;
using System.Configuration;

using SourceSafeTypeLib;

using Etomic.VSSManager.Remote;

namespace Etomic.VSSManager.Server
{
	/// <summary>
	/// A concrete implementation of the IVssDatabase interface.
	/// This class wraps the COM VSSDatabase class and is remotable.
	/// </summary>
	public class VssDatabase : MarshalByRefObject, IVssDatabase
	{
		private static readonly string VSSPATH = ConfigurationSettings.AppSettings["VssDbPath"].ToString();
		private static readonly string VSSUID = ConfigurationSettings.AppSettings["VssDbUid"].ToString();
		private static readonly string VSSPWD = ConfigurationSettings.AppSettings["VssDbPwd"].ToString();

		private VSSDatabaseClass _currentDatabase = new VSSDatabaseClass();
		private string _name;

		/// <summary>
		/// Constructor accepts the database friendly name and connects to the database or reports and logs an application exception during the attempt.
		/// </summary>
		/// <param name="name">The friendly (folder) name of the database.</param>
		public VssDatabase(string name)
		{
			try
			{
				this._name = name;
				string srcSafeIni = String.Format(VSSPATH, name);
				this._currentDatabase.Open(srcSafeIni, VSSUID, VSSPWD);
			}
			catch(Exception e)
			{
				Log.Error(String.Format("Connection to VSS Database [{0}] failed. {1}",name, e.Message));
				throw new ApplicationException("Connection to VSS DB failed! " + e.Message, e);
			}
		}

		#region IVssDatabase Members

		/// <summary>
		/// Returns an IList of string objects representing user names apart from those on the exception list.
		/// </summary>
		public IList Users
		{
			get
			{
				IList exceptionUserNames = GetExceptionUserList();

				ArrayList users = new ArrayList();
		
				foreach(IVSSUser originalUser in this._currentDatabase.Users)
				{
					if(!exceptionUserNames.Contains(originalUser.Name))
					{
						User currentUser = new User(originalUser);
						users.Add(currentUser);
					}
				}
				return users;
			}
		}

		/// <summary>
		/// Updates all users in the database to 'readonly' access apart from those in the exception list.
		/// Cannot be used to update all users to 'readwrite' access.
		/// </summary>
		public void UpdateReadOnlyAll()
		{
			IList exceptionUserNames = GetExceptionUserList();
			
			foreach(IVSSUser user in this._currentDatabase.Users)
			{
				if(!exceptionUserNames.Contains(user.Name) && !user.Name.ToUpper().Equals("GUEST"))
				{
					Log.Info(String.Format("Setting user [{0}] to readonly.", user.Name));
					user.ReadOnly = true; //Cannot use to set all access to write
				}
			}
		}

		/// <summary>
		/// Updates a single user to 'readonly' access if the user is 'readwrite', or vice versa.
		/// </summary>
		/// <param name="user">The IUser object for update</param>
		public void UpdateReadOnly(IUser user)
		{
			UpdateReadOnly(user.Name);
		}

		/// <summary>
		/// Updates a single user to 'readonly' access if the user is 'readwrite', or vice versa.
		/// </summary>
		/// <param name="userName">A string of the user login name for update.</param>
		public void UpdateReadOnly(string userName)
		{
			Log.Info(String.Format("Updating user [{0}]", userName));
			this._currentDatabase.Users[userName].ReadOnly = !this._currentDatabase.Users[userName].ReadOnly;
		}

		/// <summary>
		/// Deletes all current users, apart from those in the exception list, in the database, and replaces with the standard list from the configuration file.
		/// All users have a standard password of p@55word, and are initially set to 'readonly'.
		/// </summary>
		public void SetStandardUsers()
		{
			IList exceptionUserNames = GetExceptionUserList();

			foreach(IVSSUser user in this._currentDatabase.Users)
			{
				if(!exceptionUserNames.Contains(user.Name))
					user.Delete();
			}

			IList standardUserList = GetStandardUserList();
			foreach(string userName in standardUserList)
			{
				this._currentDatabase.AddUser(userName, "p@55word", true);
			}

			Log.Info(String.Format("Setting standard users for VSS DB [{0}].", this.Name));
		}

		/// <summary>
		/// Adds a user to the current sourcesafe database with the default password.
		/// </summary>
		/// <param name="username">The username for the user</param>
		/// <param name="readOnly">Whether the user should have readonly priviliges</param>
		public void AddUser(string username, bool readOnly)
		{
			this._currentDatabase.AddUser(username, "p@55word", readOnly);
		}

		/// <summary>
		/// The friendly (folder) name of the VSS Database.
		/// </summary>
		public string Name
		{
			get{return _name;}
		}

		#endregion

		private IList GetExceptionUserList()
		{
			ArrayList exceptionUserList;
			exceptionUserList = ConfigurationSettings.GetConfig("VSS/exceptionUsers") as ArrayList;

			return exceptionUserList;
		}

		private IList GetStandardUserList()
		{
			ArrayList standardUserList;
			standardUserList = ConfigurationSettings.GetConfig("VSS/standardUsers") as ArrayList;

			return standardUserList;
		}
	}
}
