using System;
using System.IO;

using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;

using NAnt.Core.Tasks;

namespace Etomic.NAntExtenstions.GeneralTasks
{
	/// <summary>
	/// Runs FxCop analysis across a set of files applying a selection of rules.
	/// </summary>
	/// <remarks>
	///   <para>
	///   A <see cref="FileSet" /> can be used to select files to run rules against.
	///   </para>
	///   <para>
	///   A <see cref="FileSet" /> can be used to select rules to run.
	///   </para>
	/// </remarks>
	/// <example>
	///   <para>Run rules across a set of files.</para>
	///   <code>
	///     <![CDATA[
	/// <fxcop executable="fxcopcmd.exe" output="report.xml">
	///		<targets basedir="bin">
	///			<include name="*.dll" />
	///		</targets>
	///     <ruleset basedir="rules">
	///         <include name="*.dll" />
	///     </ruleset>
	/// </fxcop>
	///     ]]>
	///   </code>
	/// </example>
	[TaskName("fxcop")]
	public class FxCopTask : ExternalProgramBase
	{
		#region Private Instance Fields

		private string _executable;
		private FileInfo _report;
		private FileSet _ruleset = new FileSet();
		private FileSet _targets = new FileSet();

		#endregion

		#region Public Instance Properties

		/// <summary>
		/// The location and name of the FxCopCmd utility.
		/// </summary>
		[TaskAttribute("executable")]
		[StringValidator(AllowEmpty=false)]
		public string Executable
		{
			get{return _executable;}
			set{_executable = value;}
		}

		/// <summary>
		/// The location and name of the XML report to be output.
		/// </summary>
		[TaskAttribute("report")]
		[StringValidator(AllowEmpty=false)]
		public FileInfo Report
		{
			get{return _report;}
			set{_report = value;}
		}

		/// <summary>
		/// A <see cref="FileSet" /> of the rules to be applied during the analysis.
		/// </summary>
		[BuildElement("ruleset")]
		public FileSet RuleSet
		{
			get{return _ruleset;}
			set{_ruleset = value;}
		}

		/// <summary>
		/// A <see cref="FileSet" /> of the target assemblies for analysis.
		/// </summary>
		[BuildElement("targets")]
		public FileSet Targets
		{
			get{return _targets;}
			set{_targets = value;}
		}

		#endregion

		#region Override Implementation of ExternalProgramBase

		/// <summary>
		/// The required implementation of the ExecuteTask method.
		/// </summary>
		protected override void ExecuteTask()
		{	
			Log(Level.Debug, "CommandLine is: {0}", this.CommandLine.ToString());
			if(this.Targets.FileNames.Count < 1) 
			{
				string errorMessage = "Task must contain at least one target assembly";
				Log(Level.Error, errorMessage);
				throw new BuildException(errorMessage);
			}
			if(this.RuleSet.FileNames.Count < 1) 
			{
				string errorMessage = "Task must contain at least one valid rule assembly.";
				Log(Level.Error, errorMessage);
				throw new BuildException(errorMessage);
			}
			base.ExecuteTask();
		}

		/// <summary>
		/// Gets the command-line arguments for the external program.
		/// </summary>
		/// <value>
		/// The command-line arguments for the external program.
		/// </value>
		public override string ProgramArguments 
		{
			get 
			{
				string progargs = "";
				
				//Get the targets for the run
				foreach(string file in Targets.FileNames)
					progargs += FormatArgument(file, FxCopArgument.Target);

				//Get the rules for the run
				foreach(string file in RuleSet.FileNames)
					progargs += FormatArgument(file, FxCopArgument.Rule);

				//Get the output for the run
				progargs += FormatArgument(this.Report.FullName, FxCopArgument.Output);

				return progargs;
			}
		}

		private string FormatArgument(string argument, FxCopArgument fxArg)
		{
			string argumentPrefix = "";
			if (fxArg == FxCopArgument.Target) argumentPrefix = "f";
			if (fxArg == FxCopArgument.Rule) argumentPrefix = "r";
			if (fxArg == FxCopArgument.Output) argumentPrefix = "o";

			return String.Format(@" /{0}:""{1}""", argumentPrefix, argument);
		}

		private enum FxCopArgument
		{
			Target,
			Rule,
			Output
		}

		/// <summary>
		/// Gets the filename of the external program to start.
		/// </summary>
		/// <value>
		/// The filename of the external program.
		/// </value>
		public override string ProgramFileName 
		{
			get{return this.Executable;}
		}

		#endregion
	}
}

