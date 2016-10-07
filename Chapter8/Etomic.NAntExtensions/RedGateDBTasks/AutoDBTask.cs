using System;
using System.Collections;
using System.IO;

using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.Core.Types;

using RedGate.SQL.Shared;
using RedGate.SQLCompare.Engine;

namespace Etomic.NAntExtensions.RedGateDBTasks
{
	[TaskName("dbAutoIntegrate")]
	public class AutoDBTask : Task
	{
		private DirectoryInfo _folder;

		//Database Info
		private string _server;
		private string _database;
		private string _username;
		private string _password;

		private DBInfo[] _dbInfos;

		private bool _write;
		private string _caption;

		private Database _source;

		[TaskAttribute("folder", Required=true)]
		public DirectoryInfo Folder
		{
			get{return _folder;}
			set{_folder = value;}
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

		[BuildElementCollection("databases", "database")]
		public DBInfo[] DBInfos
		{
			get{return _dbInfos;}
			set{_dbInfos = value;}
		}

		[TaskAttribute("write", Required=true), BooleanValidator()]
		public bool Write
		{
			get{return _write;}
			set{_write = value;}
		}

		[TaskAttribute("caption", Required=true)]
		public string Caption
		{
			get{return _caption;}
			set{_caption = value;}
		}

		protected override void ExecuteTask()
		{
			_source = new Database();
			_source.Register(new ConnectionProperties(this._server, 
																			this._database, 
																			this._username, 
																			this._password), 
										Options.Default);

			if(this._write)DoCreateScript();
			DoAlterScripts();

			_source.Dispose();
		}

		private void DoCreateScript()
		{
			Log(Level.Info, "Handling CREATE Script");
			ExecutionBlock createScript = CalculateScript(null);
			WriteScript(String.Format("CREATE-{0}.sql", _caption), createScript.ToString());
			createScript.Dispose();
		}

		private void DoAlterScripts()
		{
			foreach(DBInfo db in _dbInfos)
			{
				Log(Level.Info, String.Format("Handling migration for {0}", db.Database));
				Database target = new Database();
				target.Register(new ConnectionProperties(db.Server, db.Database, db.Username, db.Password), Options.Default);
				ExecutionBlock alterScript = CalculateScript(target);
				ExecuteScript(alterScript, target);
				if(db.Write) WriteScript(String.Format("ALTER-{0}-{1}.sql", db.Database, _caption), alterScript.ToString());
				
				alterScript.Dispose();
				target.Dispose();
			}
		}

		private ExecutionBlock CalculateScript(Database target)
		{
			Differences differences = _source.CompareWith(target, Options.Default);

			foreach (Difference difference in differences)
			{
				difference.Selected=true;
			}

			Work work=new Work();
			work.BuildFromDifferences(differences, Options.Default, true);

			return work.ExecutionBlock;
		}

		private void ExecuteScript(ExecutionBlock script, Database target)
		{
			Utils utils = new Utils();
			utils.ExecuteBlock(script, 
						target.ConnectionProperties.ServerName, 
						target.ConnectionProperties.DatabaseName, 
						target.ConnectionProperties.IntegratedSecurity, 
						target.ConnectionProperties.UserName, 
						target.ConnectionProperties.Password);
		}

		private void WriteScript(string caption, string content)
		{
			Log(Level.Info, String.Format("Writing script {0}", caption));
			FileStream script = new FileStream(Path.Combine(this._folder.FullName, caption), FileMode.Create);
			StreamWriter sw = new StreamWriter(script);
			sw.Write(content);
			sw.Close();
			script.Close();
		}
	}
}
