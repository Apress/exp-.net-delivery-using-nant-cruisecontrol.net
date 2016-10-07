using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace Etomic.VSSManager.Server
{
	/// <summary>
	/// Used to handle the exceptionUsers and standardUsers elements of the app.config file.
	/// </summary>
	public class VssDatabaseConfigHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler Members

		/// <summary>
		/// Returns an ArrayList of configuration information.
		/// </summary>
		/// <param name="parent">Standard param</param>
		/// <param name="configContext">Standard param</param>
		/// <param name="section">Standard param</param>
		/// <returns>ArrayList of string objects representing user names.</returns>
		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			ArrayList dbList = new ArrayList();

			foreach(XmlNode node in section.ChildNodes)
				if (node.NodeType == System.Xml.XmlNodeType.Element) 
				{
					dbList.Add(node.Attributes["name"].Value);
				}

			return dbList;
		}

		#endregion
	}
}
