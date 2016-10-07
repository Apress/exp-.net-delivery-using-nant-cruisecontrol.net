using System;
using System.IO;

using NUnit.Framework;

using Etomic.ShareTransformer.Engine;
using Etomic.ShareTransformer.Engine.Core;

namespace TransformerTests
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
		[ExpectedException(typeof(ArgumentException))]
		public void TestEmptySaveContent()
		{
			_engine.SaveFileContents("", "c:\\file.txt");
		}
	}
}
