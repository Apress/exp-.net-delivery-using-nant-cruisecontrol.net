using System;
using System.IO;

using NUnit.Framework;

using Etomic.Library.Transformer.Engine;

namespace Etomic.Library.Transformer.Tests
{
	[TestFixture]
	public class EngineTests
	{
		private ApplicationEngine _engine = new ApplicationEngine();

		public EngineTests()
		{
		}

		[Test]
		public void TestReadWriteContents()
		{
			const string PATH	  = "c:\\file.txt";
			const string CONTENTS = "Testing...";

			_engine.SaveFileContents(CONTENTS, PATH);

			Assert.IsTrue(File.Exists(PATH));
			Assert.AreEqual(CONTENTS, _engine.GetFileContents(PATH));

			File.Delete(PATH);
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

			string output = _engine.TransformXml(XML, XSLT);
			
			Console.WriteLine(output);
			Assert.IsTrue(output.Equals(@"<?xml version=""1.0"" encoding=""utf-8""?>1"));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestEmptySaveContent()
		{
			_engine.SaveFileContents("", "c:\\file.txt");
		}
	}
}
