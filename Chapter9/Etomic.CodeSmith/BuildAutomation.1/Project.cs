using System;
using System.Xml.Serialization;
using System.ComponentModel;

using CodeSmith.CustomProperties;

namespace Etomic.CodeSmithExtensions.BuildAutomation
{
	public class Project
	{
		[XmlAttribute]
		public string Name;
	}
}
