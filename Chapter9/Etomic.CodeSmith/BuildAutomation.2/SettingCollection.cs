using System;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.ComponentModel;

using CodeSmith.CustomProperties;

namespace Etomic.CodeSmithExtensions.BuildAutomation
{
	public class SettingCollection : NameValueCollection, IXmlSerializable
	{
		#region IXmlSerializable Members

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			//Not Required
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			//Not Required
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			while (reader.MoveToNextAttribute())
			{
				this.Add(reader.Name, reader.Value);
			}
			reader.Read();
		}

		#endregion
	}
}
