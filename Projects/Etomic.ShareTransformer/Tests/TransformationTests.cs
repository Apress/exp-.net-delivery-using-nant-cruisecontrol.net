using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

using NUnit.Framework;

using Etomic.ShareTransformer.Engine;
using Etomic.ShareTransformer.Engine.Core;

namespace TransformerTests
{
	[TestFixture]
	public class TransformationTests
	{
		string _dbConn = ConfigurationSettings.AppSettings["DbConnectionString"];

		public TransformationTests()
		{

		}

		[SetUp]
		public void Setup()
		{
		}

		[TearDown]
		public void TearDown()
		{
			DeleteAllTransformations();
		}

		[Test]
		public void TestTransformXml()
		{
			const string XML  = @"<testing><test>1</test></testing>";
			const string XSLT = @"
			<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
				<xsl:template match=""testing"">
					<xsl:value-of select=""test"" />
				</xsl:template>
			</xsl:stylesheet>";

			Transformation transform = new Transformation(XML, XSLT);
			transform.Transform();
			
			Console.WriteLine(transform.Output);
			Assert.IsTrue(transform.Output.Equals(@"<?xml version=""1.0"" encoding=""utf-8""?>1"));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestInvalidXmlState()
		{
			Transformation t = new Transformation();
			t.Xslt = "...";
			t.Transform();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestInvalidXsltState()
		{
			Transformation t = new Transformation();
			t.Xml = "...";
			t.Transform();
		}

		[Test]
		public void TestNullPropertySetter()
		{
			Transformation t = new Transformation();
			t.Xml  = null;
			t.Xslt = null;
			t.Output = null;

			Assert.IsNotNull(t.Xml);
			Assert.IsNotNull(t.Xslt);
			Assert.IsNotNull(t.Output);
			Assert.IsNotNull(t.Title);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestBadLoadId()
		{
			Transformation t = new Transformation(-1);
		}

		[Test]
		public void TestSaveAndLoad()
		{
			Transformation t = new Transformation();
			t.Output = "aaa";
			t.Title  = "aaa";
			t.Xml	 = "aaa";
			t.Xslt   = "aaa";
			t.Save();

			Assert.IsTrue(t.Id != 0);

			Transformation loaded = new Transformation(t.Id);

			Assert.IsTrue(t.Output.Equals(loaded.Output));
			Assert.IsTrue(t.Title.Equals(loaded.Title));
			Assert.IsTrue(t.Xml.Equals(loaded.Xml));
			Assert.IsTrue(t.Xslt.Equals(loaded.Xslt));
		}

		[Test]
		public void TestGetAll()
		{
			DeleteAllTransformations();

			Transformation t = new Transformation();
			t.Output = "aaa";
			t.Title  = "aaa";
			t.Xml	 = "aaa";
			t.Xslt   = "aaa";
			t.Save();
			t.Save();
			t.Save();

			ApplicationEngine engine = new ApplicationEngine();
			IList transforms = engine.GetAllTransformations();
		
			Assert.IsTrue(transforms.Count == 3);
		}


		private void DeleteAllTransformations()
		{
			using(SqlCommand cmd = new SqlCommand("delete transformations", new SqlConnection(_dbConn)))
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				cmd.Connection.Close();
			}
		}
	}
}
