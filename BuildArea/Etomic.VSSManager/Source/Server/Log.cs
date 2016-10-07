using System;

namespace Etomic.VSSManager.Server
{
	/// <summary>
	/// Wraps the log4net library offering simple logging functions. 
	/// Use the app.config file to change logging settings.
	/// </summary>
	public class Log
	{
		private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Log));

		/// <summary>
		/// Logs 'Info' level messages.
		/// </summary>
		/// <param name="message">Message to log.</param>
		public static void Info(string message)
		{
			logger.Info(message);
		}

		/// <summary>
		/// Logs 'Debug' level messages.
		/// </summary>
		/// <param name="message">Message to log.</param>
		public static void Debug(string message)
		{
			logger.Debug(message);
		}

		/// <summary>
		/// Logs 'Warn' level messages.
		/// </summary>
		/// <param name="message">Message to log.</param>
		public static void Warn(string message)
		{
			logger.Warn(message);
		}

		/// <summary>
		/// Logs 'Fatal' level messages.
		/// </summary>
		/// <param name="message">Message to log.</param>
		public static void Fatal(string message)
		{
			logger.Fatal(message);
		}

		/// <summary>
		/// Logs 'Error' level messages.
		/// </summary>
		/// <param name="message">Message to log.</param>
		public static void Error(string message)
		{
			logger.Error(message);
		}
	}
}
