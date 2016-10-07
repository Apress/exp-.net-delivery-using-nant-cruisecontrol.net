using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace BuildScripts
{
	public sealed class EntryPoint
	{
		static void Main(string[] args)
		{
			if(args.Length == 0)
			{
				ShowUsage();
				return;
			}

			string projectSetFile = args[0];
			ProduceFiles(projectSetFile);
		}

		static void ProduceFiles(string projectSetFile)
		{
			string rootDir	= Path.GetDirectoryName(projectSetFile);
			XmlDocument dom = new XmlDocument();
			dom.Load(projectSetFile);

			ProduceCcnetConfigFile(rootDir, dom);
			ProduceBuildAndDeployFiles(rootDir, dom);
		}

		static void ProduceCcnetConfigFile(string outputDirectory, XmlDocument dom)
		{
			string outputPath = Path.Combine(outputDirectory, "ccnet.config");
			string xsltFile	  = Path.Combine(outputDirectory, "Ccnet.xsl");
			TransformXml(dom, xsltFile, outputPath, null);
			Console.WriteLine("Produced file: " + outputPath);
		}

		static void ProduceBuildAndDeployFiles(string outputDirectory, XmlDocument dom)
		{
			string companyName = dom.DocumentElement.SelectSingleNode("Settings").Attributes["CompanyName"].InnerText;

			int index = 1;
			foreach(XmlNode projectNode in dom.DocumentElement.SelectNodes("Projects/Project"))
			{
				string projectName	= projectNode.Attributes["Name"].InnerText;
				string solutionName = string.Join(".", new string[] { companyName, projectName });
				
				ProduceBuildAndDeployFile("Build", outputDirectory, solutionName, projectNode, dom, index);
				ProduceBuildAndDeployFile("Deploy", outputDirectory, solutionName, projectNode, dom, index);
				index++;
			}
		}

		static void ProduceBuildAndDeployFile(string type, string outputDirectory, string solutionName, XmlNode projectNode, XmlDocument dom, int index)
		{
			string xslt = Path.Combine(outputDirectory, type + ".xsl");
			string outputFile = GetOutputFileName(outputDirectory, solutionName, type);

			XPathNodeIterator iterator = projectNode.ParentNode.CreateNavigator().Select("Project[" + index + "]");
			XsltArgumentList arguments = new XsltArgumentList();
			arguments.AddParam("ProjectData", "", iterator);

			TransformXml(dom, xslt, outputFile, arguments);
			Console.WriteLine("Produced file: " + outputFile);
		}

		static string GetOutputFileName(string outputDirectory, string solutionName, string type)
		{
			return Path.Combine(outputDirectory, string.Join(".", new string[] { solutionName, type, "xml" }));
		}

		static void TransformXml(XmlDocument document, string xsltFile, string outputPath, XsltArgumentList arguments)
		{
			string output = "";
			XmlUrlResolver resolver	  = new XmlUrlResolver();
			XslTransform stylesheet   = new XslTransform();
			stylesheet.Load(xsltFile);

			using(MemoryStream stream = new MemoryStream())
			{
				stylesheet.Transform(document.DocumentElement.CreateNavigator(), arguments, stream, resolver);
				stream.Position = 0;

				using(StreamReader sr = new StreamReader(stream))
				{
					output = sr.ReadToEnd();
				}
			}

			using(StreamWriter sw = new StreamWriter(outputPath))
			{
				sw.WriteLine(output);
			}
		}

		static string GetAttribute(string name, XmlNode node)
		{
			XmlAttribute attribute = node.Attributes[name];

			if(attribute != null)
				return attribute.InnerText;
			else
				return string.Empty;
		}

		static void ShowUsage()
		{
			Console.WriteLine("You must specify the path to the Project Set xml file");
		}
	}
}
