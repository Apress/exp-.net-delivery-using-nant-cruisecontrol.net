using System;
using System.Collections;
using System.IO;

using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.Core.Types;

namespace Etomic.NAntExtensions.ManualDBTasks
{
	[TaskName("dbIntegrate")]
	public class ManualDBTask : Task
	{
		private DirectoryInfo _folder;
		private string _compareOption;

		//Database Info
		private string _server;
		private string _database;
		private string _username;
		private string _password;

		[TaskAttribute("folder", Required=true)]
		public DirectoryInfo Folder
		{
			get{return _folder;}
			set{_folder = value;}
		}
		
		/// <summary>
		/// Available options "Name", "LastWriteTime", "CreationTime"
		/// </summary>
		[TaskAttribute("compare", Required=true)]
		public string CompareOption
		{
			get{return _compareOption;}
			set{_compareOption = value;}
		}

		[TaskAttribute("server", Required=true)]
		public string Server
		{
			get{return _server;}
			set{_server = value;}
		}

		[TaskAttribute("database", Required=true)]
		public string Database
		{
			get{return _database;}
			set{_database = value;}
		}

		[TaskAttribute("uid", Required=true)]
		public string Username
		{
			get{return _username;}
			set{_username = value;}
		}

		[TaskAttribute("pwd", Required=true)]
		public string Password
		{
			get{return _password;}
			set{_password = value;}
		}

		protected override void ExecuteTask()
		{
			//Get and sort the files
			FileInfo[] files = _folder.GetFiles();
			Array.Sort(files, new ObjectComparer(new String[]{_compareOption}));
			
			//Execute the SQL into the database
			foreach(FileInfo fi in files)
			{
				Log(Level.Info, fi.Name);
				ExecTask e = new ExecTask();
				e.Project = this.Project;
				e.FileName = @"osql.exe";
				e.CommandLineArguments = String.Format(@"-U {0} -P {1} -S {2} -d {3} -i {4}", this._username, this._password, this._server, this._database, fi.FullName);
				e.Execute();
			}
		}
	}
}
