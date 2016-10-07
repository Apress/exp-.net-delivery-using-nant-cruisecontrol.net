#region References

using System;
using System.Configuration;

#endregion

namespace Etomic.ShareTransformer.Engine
{
	/// <summary>
	/// Configuration constants
	/// </summary>
	internal sealed class ConfigConstants
	{
		#region Constructors

		private ConfigConstants() {}

		#endregion

		#region Static Properties

		internal static string DbConnectionString
		{
			get { return ConfigurationSettings.AppSettings["DbConnectionString"]; }
		}

		#endregion
	}
}
