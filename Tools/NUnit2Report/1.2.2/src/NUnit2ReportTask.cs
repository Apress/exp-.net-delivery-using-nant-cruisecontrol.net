
// 
// NUnit2ReportTask.cs
//
//	Loosely based on Tomas Restrepo NUnitReport for NAnt.
//	Loosely based on Erik Hatcher JUnitReport for Ant.
//
// Author:
//    Gilles Bayon (gilles.bayon@laposte.net)
//
// Copyright (C) 2003 Gilles Bayon
//

//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//

// TO DO LIST
// Regarder ds Nunit-frame les variables Nant qui sont en commentaires
// Rajouter les commentaires xml
//	Example :
// 		<nunit2report out="NUnitReport(No-Frame).html" >
//			<fileset>
//				<includes name="result.xml" />
//			</fileset>
//			<summaries>
//				<includes name="comment.xml" />
//			</summaries>
//		</nunit2report>
// Clean code

//#define ECHO_MODE

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Mail;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

using NAnt.Core.Attributes;
using NAnt.Core;
using NAnt.Core.Types;

namespace NAnt.NUnit2ReportTasks
{ 

   /// <summary>
   /// A task that generates a summary HTML
   /// from a set of NUnit xml report files.
   /// </summary>
   /// <remarks>
   /// This task can generate a combined HTML report out of a
   /// set of NUnit result files generated using the 
   /// XML Result Formatter.
   /// 
   /// By default, NUnitReport will generate the combined
   /// report using the NUnitSummary.xsl file located at the
   /// assembly's location, but you can specify a different
   /// XSLT template to use with the <code>xslfile</code>
   /// attribute.
   /// 
   /// Also, all the properties defined in the current
   /// project will be passed down to the XSLT file as 
   /// template parameters, so you can access properties
   /// such as nant.project.name, nant.version, etc.
   /// </remarks>
   /// <example>
   ///   <code><![CDATA[
   ///   <nunit2report 
   ///         out="${outputdir}\TestSummary.html"
   ///         >
   ///      <fileset>
   ///         <includes name="${outputdir}\results.xml" />
   ///      </fileset>
   ///   </nunit2report>
   ///   
   ///   ]]></code>
   /// </example>
   [TaskName("nunit2report")]
   public class NUnit2ReportTask : Task
   {
	  private const string XSL_DEF_FILE_NOFRAME = "NUnit-NoFrame.xsl";
	  private const string XSL_DEF_FILE_FRAME = "NUnit-Frame.xsl";
	  private const string XSL_DEF_FILE_SUMMARY = "NReport-Summary.xsl";

	  private const string XSL_I18N_FILE = "i18n.xsl";

	  private string _outFilename="index.htm";
	  private string _todir="";
	  
	  private FileSet _fileset = new FileSet();
	  private XmlDocument _FileSetSummary;

	  private FileSet _summaries = new FileSet();
	  private string _tempXmlFileSummarie = "";

	  private string _xslFile="";
	  private string _i18nXsl="";
	  private string _summaryXsl="";
	  private string _openDescription ="no";
	  private string _language="";
	  private string _format="noframes";
	  private string _nantLocation ="";
	  private XsltArgumentList _xsltArgs;

      /// <summary>
      /// The format of the generated report. 
	  /// Must be "noframes" or "frames". 
	  /// Default to "noframes".
      /// </summary>
      [TaskAttribute("format", Required=false)]
      public string Format { 
         get { return _format; } 
         set { _format = value; } 
      }

      /// <summary>
      /// The output language.
      /// </summary>
      [TaskAttribute("lang", Required=false)]
      public string Language { 
         get { return _language; } 
         set { _language = value; } 
      }

      /// <summary>
      /// Open all description method. Default to "false".
      /// </summary>
      [TaskAttribute("opendesc", Required=false)]
      public string OpenDescription { 
         get { return _openDescription; } 
         set { _openDescription = value; } 
      }

      /// <summary>
      /// The directory where the files resulting from the transformation should be written to.
      /// </summary>
      [TaskAttribute("todir", Required=false)]
      public string Todir { 
         get { return _todir; } 
         set { _todir = value; } 
      }
     
      /// <summary>
      /// Index of the Output HTML file(s).
	  /// Default to "index.htm".
      /// </summary>
      [TaskAttribute("out", Required=false)]
      public string OutFilename { 
         get { return _outFilename; } 
         set { _outFilename = value; } 
      }


      /// <summary>
      /// Set of XML files to use as input
      /// </summary>
      [FileSet("fileset")]
      public FileSet XmlFileSet {
         get { return _fileset; }
      }

      /// <summary>
      /// Set of XML files to use as input
      /// </summary>
      [FileSet("summaries")]
      public FileSet XmlSummaries {
         get { return _summaries; }
      }


		///<summary>
		///Initializes task and ensures the supplied attributes are valid.
		///</summary>
		///<param name="taskNode">Xml node used to define this task instance.</param>
		protected override void InitializeTask(System.Xml.XmlNode taskNode) 
		{
			Assembly thisAssm = Assembly.GetExecutingAssembly();

			#if ECHO_MODE
				Console.WriteLine ("Location : "+thisAssm.CodeBase);
			#endif 

			_nantLocation = Path.GetDirectoryName(thisAssm.CodeBase);//(thisAssm.Location


			if (this.Format=="noframes") {
				_xslFile = Path.Combine(_nantLocation, XSL_DEF_FILE_NOFRAME);
			}

			_i18nXsl = Path.Combine(_nantLocation, XSL_I18N_FILE);
			_summaryXsl = Path.Combine(_nantLocation, XSL_DEF_FILE_SUMMARY);

			if (this.XmlFileSet.FileNames.Count == 0) {
				throw new BuildException("NUnitReport fileset cannot be empty!", Location);
			}

			foreach ( string file in this.XmlSummaries.FileNames ) {
				_tempXmlFileSummarie = file;
			}

			// Get the Nant, OS parameters
			_xsltArgs = GetPropertyList();

			//Create directory if ...
			if (this.Todir!="") {
				Directory.CreateDirectory(this.Todir);
			}

		}

      /// <summary>
      /// This is where the work is done
      /// </summary>
      protected override void ExecuteTask() 
      {
         _FileSetSummary = CreateSummaryXmlDoc();

         foreach ( string file in this.XmlFileSet.FileNames ) {
            XmlDocument source = new XmlDocument();
            source.Load(file);
            XmlNode node = _FileSetSummary.ImportNode(source.DocumentElement, true);
            _FileSetSummary.DocumentElement.AppendChild(node);
         }

         //
         // prepare properties and transform
         //
		try {
				if (this.Format=="noframes") {

					XslTransform xslTransform = new XslTransform();
					xslTransform.Load(_xslFile);

					// xmlReader hold the first transformation
					XmlReader xmlReader = xslTransform.Transform(_FileSetSummary, _xsltArgs);

					// ---------- i18n --------------------------
					XsltArgumentList xsltI18nArgs = new XsltArgumentList();
					xsltI18nArgs.AddParam("lang", "",this.Language);

					XslTransform xslt = new XslTransform();
					//Load the i18n stylesheet.
					xslt.Load(_i18nXsl);

					XPathDocument xmlDoc;
					xmlDoc = new XPathDocument(xmlReader);

					XmlTextWriter writerFinal = new XmlTextWriter(Path.Combine(this.Todir,this.OutFilename), System.Text.Encoding.GetEncoding("ISO-8859-1"));
					// Apply the second transform to xmlReader to final ouput
					xslt.Transform(xmlDoc, xsltI18nArgs, writerFinal);

					xmlReader.Close();
					writerFinal.Close();
				}
				else {
						StringReader stream;
						XmlTextReader reader = null;

						try
						{
						#if ECHO_MODE
							Console.WriteLine ("Initializing StringReader ...");
						#endif 

							// create the index.html
							stream = new StringReader("<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' >" +
								"<xsl:output method='html' indent='yes' encoding='ISO-8859-1'/>" +
								"<xsl:include href=\""+Path.Combine(_nantLocation,"NUnit-Frame.xsl")+"\"/>" +
								"<xsl:template match=\"test-results\">" +
								"   <xsl:call-template name=\"index.html\"/>" +
								" </xsl:template>" +
								" </xsl:stylesheet>");
							this.Write (stream,Path.Combine(this.Todir,this.OutFilename));

							// create the stylesheet.css
							stream = new StringReader("<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' >" +
								"<xsl:output method='html' indent='yes' encoding='ISO-8859-1'/>" +
								"<xsl:include href=\""+Path.Combine(_nantLocation,"NUnit-Frame.xsl")+"\"/>" +
								"<xsl:template match=\"test-results\">" +
								"   <xsl:call-template name=\"stylesheet.css\"/>" +
								" </xsl:template>" +
								" </xsl:stylesheet>");
							this.Write (stream,Path.Combine(this.Todir,"stylesheet.css"));

							// create the overview-summary.html at the root 
							stream = new StringReader("<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' >" +
								"<xsl:output method='html' indent='yes' encoding='ISO-8859-1'/>" +
								"<xsl:include href=\""+Path.Combine(_nantLocation,"NUnit-Frame.xsl")+"\"/>" +
								"<xsl:template match=\"test-results\">" +
								"    <xsl:call-template name=\"overview.packages\"/>" +
								" </xsl:template>" +
								" </xsl:stylesheet>");
							this.Write (stream,Path.Combine(this.Todir,"overview-summary.html"));
							

							// create the allclasses-frame.html at the root 
							stream = new StringReader("<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' >" +
								"<xsl:output method='html' indent='yes' encoding='ISO-8859-1'/>" +
								"<xsl:include href=\""+Path.Combine(_nantLocation,"NUnit-Frame.xsl")+"\"/>" +
								"<xsl:template match=\"test-results\">" +
								"    <xsl:call-template name=\"all.classes\"/>" +
								" </xsl:template>" +
								" </xsl:stylesheet>");
							this.Write (stream,Path.Combine(this.Todir,"allclasses-frame.html"));

							// create the overview-frame.html at the root
							stream = new StringReader("<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' >" +
								"<xsl:output method='html' indent='yes' encoding='ISO-8859-1'/>" +
								"<xsl:include href=\""+Path.Combine(_nantLocation,"NUnit-Frame.xsl")+"\"/>" +
								"<xsl:template match=\"test-results\">" +
								"    <xsl:call-template name=\"all.packages\"/>" +
								" </xsl:template>" +
								" </xsl:stylesheet>");
							this.Write (stream,Path.Combine(this.Todir,"overview-frame.html"));

							// Create directory
							string path ="";
							
							//--- Change 11/02/2003 -- remove
							//XmlDocument doc = new XmlDocument();
							//doc.Load("result.xml"); _FileSetSummary
							//---
							XPathNavigator xpathNavigator = _FileSetSummary.CreateNavigator(); //doc.CreateNavigator();

							// Get All the test suite containing test-case.
							XPathExpression expr = xpathNavigator.Compile("//test-suite[(child::results/test-case)]");
							
							XPathNodeIterator iterator = xpathNavigator.Select(expr);
							string directory="";
							string testSuiteName = "";

							while (iterator.MoveNext())
							{
								XPathNavigator xpathNavigator2 = iterator.Current;
								testSuiteName = iterator.Current.GetAttribute("name","");

								#if ECHO_MODE
									Console.WriteLine("Test case : "+testSuiteName);   
								#endif 


								// Get get the path for the current test-suite.
								XPathNodeIterator iterator2 = xpathNavigator2.SelectAncestors("", "", true);
								path = "";
								string parent = "";
								int parentIndex = -1;
								
								while (iterator2.MoveNext()) 
								{
									directory = iterator2.Current.GetAttribute("name","");
									if (directory!="" && directory.IndexOf(".dll")<0) 
									{
										path = directory+"/"+path;
									}
									if (parentIndex==1)
										parent = directory;
									parentIndex++;
								}
								Directory.CreateDirectory(Path.Combine(this.Todir,path));// path = xx/yy/zz

								#if ECHO_MODE
									Console.WriteLine("path="+path+"\n");   
								#endif 

								#if ECHO_MODE
									Console.WriteLine("parent="+parent+"\n");   
								#endif 

								// Build the "testSuiteName".html file
								// Correct MockError duplicate testName !
								// test-suite[@name='MockTestFixture' and ancestor::test-suite[@name='Assemblies'][position()=last()]] 

								stream = new StringReader("<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' >" +
									"<xsl:output method='html' indent='yes' encoding='ISO-8859-1'/>" +
									"<xsl:include href=\""+Path.Combine(_nantLocation,"NUnit-Frame.xsl")+"\"/>" +
									"<xsl:template match=\"/\">" +
									"	<xsl:for-each select=\"//test-suite[@name='"+testSuiteName+"' and ancestor::test-suite[@name='"+parent+"'][position()=last()]]\">" +
									"		<xsl:call-template name=\"test-case\">" +
									"			<xsl:with-param name=\"dir.test\">"+String.Join(".", path.Split('/'))+"</xsl:with-param>" +
									"		</xsl:call-template>" +
									"	</xsl:for-each>" +
									" </xsl:template>" +
									" </xsl:stylesheet>");
								this.Write (stream, Path.Combine( Path.Combine(this.Todir,path) ,testSuiteName+".html") );

								#if ECHO_MODE
									Console.WriteLine("dir="+this.Todir+path+" Generate "+testSuiteName+".html\n");   
								#endif 
							}

							#if ECHO_MODE
								Console.WriteLine ("Processing ...");
								Console.WriteLine ();	
							#endif 

						}

						catch (Exception e)
						{
							Console.WriteLine ("Exception: {0}", e.ToString());
						}

						finally
						{
						#if ECHO_MODE
							Console.WriteLine();
							Console.WriteLine("Processing of stream complete.");	
						#endif 

							// Finished with XmlTextReader
							if (reader != null)
								reader.Close();
						}
				}
			}
			catch (Exception e) {
				throw new BuildException(e.Message, Location);
			}
      }


      /// <summary>
      /// Initializes the XmlDocument instance
      /// used to summarize the test results
      /// </summary>
      /// <returns></returns>
      private XmlDocument CreateSummaryXmlDoc()
      {
		XmlDocument doc = new XmlDocument();
		XmlElement root = doc.CreateElement("testsummary");
		root.SetAttribute("created", DateTime.Now.ToString());
		doc.AppendChild(root);

         return doc;
      }

      /// <summary>
      /// Builds an XsltArgumentList with all
      /// the properties defined in the 
      /// current project as XSLT parameters.
      /// </summary>
      /// <returns></returns>
      private XsltArgumentList GetPropertyList()
      {
         XsltArgumentList args = new XsltArgumentList();

	#if ECHO_MODE
		Console.WriteLine();
		Console.WriteLine("XsltArgumentList");	
	#endif 

         foreach ( DictionaryEntry entry in Project.Properties )
         {
		 	#if ECHO_MODE
				Console.WriteLine();
				Console.WriteLine("Project.Properties :"+(string)entry.Key+"="+(string)entry.Value);	
			#endif 
			
			if ((string)entry.Value!=null)
			{
			//Patch from Christoph Walcher 
				try{
					args.AddParam((string)entry.Key, "", (string)entry.Value);
					}
              catch(ArgumentException aex){
	              Console.WriteLine("Invalid Xslt parameter {0}", aex);
				  }
			}
         }
		 
		 // Add argument to the C# XML comment file
		 args.AddParam("summary.xml", "", _tempXmlFileSummarie);
   		 // Add open.description argument 
		 args.AddParam("open.description", "", this.OpenDescription);
      
		 return args;
      }


	private void Write(StringReader stream, string fileName) 
	{
		XmlTextReader reader = null;

		// Load the XmlTextReader from the stream
		reader = new XmlTextReader (stream);
		XslTransform xslTransform = new XslTransform();
		//Load the stylesheet from the stream.
		xslTransform.Load(reader);

		XPathDocument xmlDoc;
		//xmlDoc = new XPathDocument("result.xml");

		// xmlReader hold the first transformation 
		XmlReader xmlReader = xslTransform.Transform(_FileSetSummary, _xsltArgs);//(xmlDoc, _xsltArgs);

		// ---------- i18n --------------------------
		XsltArgumentList xsltI18nArgs = new XsltArgumentList();
		xsltI18nArgs.AddParam("lang", "",this.Language);

		XslTransform xslt = new XslTransform();

		//Load the stylesheet.
		xslt.Load(_i18nXsl);

		xmlDoc = new XPathDocument(xmlReader);

		XmlTextWriter writerFinal = new XmlTextWriter(fileName, System.Text.Encoding.GetEncoding("ISO-8859-1"));
		// Apply the second transform to xmlReader to final ouput
		xslt.Transform(xmlDoc, xsltI18nArgs, writerFinal);

		xmlReader.Close();
		writerFinal.Close();

	}

   } // class NUnit2ReportTask

} // namespace  NAnt.NUnit2ReportTasks