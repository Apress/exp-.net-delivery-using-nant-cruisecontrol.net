using System;
using System.Xml.Serialization;
using System.ComponentModel;

using CodeSmith.CustomProperties;

namespace Etomic.CodeSmithExtensions.BuildAutomation
{
	public class OutputAssembly
	{
		[XmlAttribute]
		public string AssemblyType;
		[XmlAttribute]
		public string Name;
		[XmlAttribute]
		public bool ShouldDocument;
		[XmlAttribute]
		public bool ShouldTest;
	}
}
