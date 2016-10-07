using System;
using System.Xml.Serialization;
using System.ComponentModel;

using CodeSmith.CustomProperties;

namespace Etomic.CodeSmithExtensions.BuildAutomation
{
	[TypeConverter(typeof(XmlSerializedTypeConverter))]
	[Editor(typeof(CodeSmith.CustomProperties.XmlSerializedFilePicker), typeof(System.Drawing.Design.UITypeEditor))]
	[XmlRootAttribute("ProjectSet", Namespace="http://www.etomic.co.uk", IsNullable = false)]
	public class ProjectSet
	{
		[XmlElement]
		public SettingCollection Settings = new SettingCollection();

		[XmlArrayAttribute("Projects")]
		public Project[] Projects;
	}
}
