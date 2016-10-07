// 
// This framework is based on log4j see http://jakarta.apache.org/log4j
// Copyright (C) The Apache Software Foundation. All rights reserved.
//
// This software is published under the terms of the Apache Software
// License version 1.1, a copy of which has been included with this
// distribution in the LICENSE.txt file.
// 

import log4net;

// We want this assembly to have a seperate logging domain to the 
// rest of the application. We will configure this domain seperatly.
[assembly: log4net.Config.Domain("SimpleModule")]

// Configure logging for this assembly using the 'SimpleModule.dll.log4net' file
[assembly: log4net.Config.DOMConfigurator(ConfigFileExtension="log4net", Watch=true)]

package SimpleModule {
	/// <summary>
	/// Summary description for Math.
	/// </summary>
	public class Math {
		// Create a logger for use in this class
		private static var log : log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public function Math() {
			if (log.IsDebugEnabled) log.Debug("Constructor");
		}

		public function Add(left : int, right : int) : int {
			var result : int = left + right;
			if (log.IsInfoEnabled) log.Info("" + left + " + " + right + " = " + result);
			return result;
		}
	}
}
