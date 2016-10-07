#region References

using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

#endregion

namespace TransformerEngine
{
	/// <summary>
	/// Provides logic and functionality for the Transformer GUI application
	/// </summary>
	public class ApplicationEngine
	{
		#region Fields

		#endregion

		#region Constructors

		/// <summary>
		/// Default parameterless constructor
		/// </summary>
		public ApplicationEngine()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the content of the specified file
		/// </summary>
		/// <param name="path">The path to the specified file</param>
		/// <returns>string containing file contents</returns>
		public string GetFileContents(string path)
		{
			if(!File.Exists(path))
				throw new ArgumentException("File does not exist for the provided path", "path");

			using(StreamReader sr = new StreamReader(path))
			{
				return sr.ReadToEnd();
			}
		}

		/// <summary>
		/// Saves the contents specified in the <see cref="contents">contents</see> parameter 
		/// to the file specified in the <see cref="path">path</see> parameter.
		/// </summary>
		/// <param name="contents">The contents of the file to save</param>
		/// <param name="path">The path where the contents should be written to</param>
		public void SaveFileContents(string contents, string path)
		{
			if(contents == null || contents.Length == 0)
				throw new ArgumentException("The contents of the file are null or zero-length.");

			using(StreamWriter sw = new StreamWriter(path, false))
			{
				sw.Write(contents);
			}
		}

		/// <summary>
		/// Transforms the provided XML data using the provided XSLT string.
		/// </summary>
		/// <param name="xml">The XML data to transform</param>
		/// <param name="xslt">The XSLT template to use for the transformation</param>
		/// <returns>The result of the transformation</returns>
		public string TransformXml(string xml, string xslt)
		{
			XPathDocument doc = new XPathDocument(new XmlTextReader(xml, XmlNodeType.Element, null));
			XslTransform transform = new XslTransform();

			using(MemoryStream ms = new MemoryStream())
			{
				XmlDocument stylesheet = new XmlDocument();
				stylesheet.LoadXml(xslt);

				transform.Load(stylesheet.CreateNavigator(), null, null);
				transform.Transform(doc, null, ms, new XmlUrlResolver());

				ms.Position = 0;

				using(StreamReader sr = new StreamReader(ms))
				{
					return sr.ReadToEnd();
				}
			}
		}

		#endregion

		#region Helper Methods

		#endregion
	}
}
