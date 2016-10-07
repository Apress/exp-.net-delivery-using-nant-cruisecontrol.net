#region References

using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

#endregion

namespace Etomic.ShareTransformer.Engine.Core
{
	/// <summary>
	/// Represents an XSL transformation.
	/// </summary>
	public sealed class Transformation
	{
		#region Fields

		private string _xml    = string.Empty;
		private string _xslt   = string.Empty;
		private string _output = string.Empty;
		private string _title  = string.Empty;
		private int _id = 0;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes the type with XML and XSLT.
		/// </summary>
		/// <param name="xml">The XML to be used for the transformation</param>
		/// <param name="xslt">The XSLT to be used for the transformation</param>
		public Transformation(string xml, string xslt)
		{
			if(xml == null || xml.Length == 0)
				throw new ArgumentException("xml");

			if(xslt == null || xslt.Length == 0)
				throw new ArgumentException("xslt");

			_xml  = ReplaceNullString(xml);
			_xslt = ReplaceNullString(xslt);
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Transformation()
		{
		}

		/// <summary>
		/// Initializes this <c>Transformation</c> instance with the data stored in the 
		/// database.
		/// </summary>
		/// <param name="transformId">The ID of the transformation</param>
		public Transformation(int transformId)
		{
			ApplicationEngine engine = new ApplicationEngine();
			Transformation transform = engine.GetTransformation(transformId);

			if(transform == null)
				throw new ArgumentException("No transformation exists for the given ID");

			this.Id		= transform.Id;
			this.Output = transform.Output;
			this.Title  = transform.Title;
			this.Xml    = transform.Xml;
			this.Xslt	= transform.Xslt;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the XML to be used for the transformation.
		/// </summary>
		public string Xml
		{
			get { return _xml; }
			set { _xml = ReplaceNullString(value); }
		}

		/// <summary>
		/// Gets the XSLT to be used for the transformation.
		/// </summary>
		public string Xslt
		{
			get { return _xslt; }
			set { _xslt = ReplaceNullString(value); }
		}

		/// <summary>
		/// Gets any output resulting from a transformation.
		/// </summary>
		public string Output
		{
			get { return _output; }
			set { _output = ReplaceNullString(value); }
		}

		/// <summary>
		/// The title given to the transformation.
		/// </summary>
		public string Title
		{
			get { return _title; }
			set { _title = ReplaceNullString(value); }
		}

		/// <summary>
		/// The ID number of the transformation.
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves the transformation to the database.
		/// </summary>
		public void Save()
		{
			if(Title.Length == 0)
				throw new ArgumentException("A Title must be provided.");

			Etomic.ShareTransformer.Engine.ApplicationEngine engine = new ApplicationEngine();
			this.Id = engine.SaveTransformation(this);
		}

		/// <summary>
		/// Transforms the provided XML data using the provided XSLT string.
		/// </summary>
		public void Transform()
		{
			CheckState();

			XPathDocument doc = new XPathDocument(new XmlTextReader(this.Xml, XmlNodeType.Element, null));
			XslTransform transform = new XslTransform();

			using(MemoryStream ms = new MemoryStream())
			{
				XmlDocument stylesheet = new XmlDocument();
				stylesheet.LoadXml(this.Xslt);

				transform.Load(stylesheet.CreateNavigator(), null, null);
				transform.Transform(doc, null, ms, new XmlUrlResolver());

				ms.Position = 0;

				using(StreamReader sr = new StreamReader(ms))
				{
					_output = sr.ReadToEnd();
				}
			}
		}

		#endregion

		#region Helper Methods

		private void CheckState()
		{
			if(Xml.Length == 0)
				throw new InvalidOperationException("Xml must be provided for the transformation.");

			if(Xslt.Length == 0)
				throw new InvalidOperationException("Xslt must be provided for the transformation.");
		}

		private string ReplaceNullString(string value)
		{
			return (value == null ? string.Empty : value);
		}

		#endregion
	}
}
