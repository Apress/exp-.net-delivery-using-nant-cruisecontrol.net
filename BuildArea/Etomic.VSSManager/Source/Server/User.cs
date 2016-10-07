using System;

using SourceSafeTypeLib;

using Etomic.VSSManager.Remote;

namespace Etomic.VSSManager.Server
{
	/// <summary>
	/// A concrete implementation of the IUser interface.
	/// This class wraps the COM IVSSUser interface and is remotable.
	/// </summary>
	public class User : MarshalByRefObject, IUser
	{
		private IVSSUser _vssUser;
		
		/// <summary>
		/// Constructor wraps the IVSSUser implementation.
		/// </summary>
		/// <param name="vssUser">The IVSSUser to wrap.</param>
		public User(IVSSUser vssUser)
		{
			_vssUser = vssUser;
		}
		
		#region IUser Members

		/// <summary>
		/// True if the user has readonly access to a VSS Database, false if the user has readwrite access.
		/// </summary>
		public bool HasReadOnly
		{
			get{return _vssUser.ReadOnly;}
		}

		/// <summary>
		/// The login name for the user.
		/// </summary>
		public string Name
		{
			get{return _vssUser.Name;	}
		}

		#endregion
	}
}
