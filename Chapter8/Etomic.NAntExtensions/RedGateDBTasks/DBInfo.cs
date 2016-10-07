using System;

using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Tasks;
using NAnt.Core.Types;

namespace Etomic.NAntExtensions.RedGateDBTasks
{
	[ElementName("database")]
	public class DBInfo : Element
	{
		private string _server;
		private string _database;
		private string _username;
		private string _password;

		private bool _write;

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

		[TaskAttribute("write", Required=true), BooleanValidator()]
		public bool Write
		{
			get{return _write;}
			set{_write = value;}
		}
	}
}
