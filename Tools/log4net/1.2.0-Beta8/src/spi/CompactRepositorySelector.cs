#region Copyright
// 
// This framework is based on log4j see http://jakarta.apache.org/log4j
// Copyright (C) The Apache Software Foundation. All rights reserved.
//
// This software is published under the terms of the Apache Software
// License version 1.1, a copy of which has been included with this
// distribution in the LICENSE.txt file.
// 
#endregion

using System;
using System.Collections;

using System.Reflection;

using log4net.Appender;
using log4net.helpers;
using log4net.spi;
using log4net.Repository;


namespace log4net.spi
{
	/// <summary>
	/// The implementation of the <see cref="IRepositorySelector"/> interface suitable
	/// for use with the compact framework
	/// </summary>
	/// <remarks>
	/// </remarks>
	public class CompactRepositorySelector : IRepositorySelector
	{
		#region Member Variables

		private const string DEFAULT_DOMAIN_NAME = "log4net-default-domain";

		private IDictionary m_domain2repositoryMap = new Hashtable();
		private Type m_defaultRepositoryType;

		private event LoggerRepositoryCreationEventHandler m_loggerRepositoryCreatedEvent;

		#endregion

		#region Constructors

		/// <summary>
		/// Create a new repository selector
		/// </summary>
		/// <param name="defaultRepositoryType">the type of the repositories to create, must implement <see cref="ILoggerRepository"/></param>
		/// <exception cref="ArgumentNullException">throw if <paramref name="defaultRepositoryType"/> is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">throw if <paramref name="defaultRepositoryType"/> does not implement <see cref="ILoggerRepository"/></exception>
		public CompactRepositorySelector(Type defaultRepositoryType)
		{
			if (defaultRepositoryType == null)
			{
				throw new ArgumentNullException("defaultRepositoryType");
			}

			// Check that the type is a repository
			if (! (typeof(ILoggerRepository).IsAssignableFrom(defaultRepositoryType)) )
			{
				throw new ArgumentOutOfRangeException("Parameter: defaultRepositoryType, Value: ["+defaultRepositoryType+"] out of range. Argument must implement the ILoggerRepository interface");
			}

			m_defaultRepositoryType = defaultRepositoryType;

			LogLog.Debug("CompactRepositorySelector: defaultRepositoryType ["+m_defaultRepositoryType+"]");
		}

		#endregion

		#region Implementation of IRepositorySelector

		/// <summary>
		/// Get the <see cref="ILoggerRepository"/> for the specified assembly
		/// </summary>
		/// <param name="assembly">the assembly to use to lookup to the <see cref="ILoggerRepository"/></param>
		/// <returns>The <see cref="ILoggerRepository"/> for the assembly</returns>
		/// <remarks>
		/// <para>Assemblies use the default domain.</para>
		/// </remarks>
		/// <exception cref="ArgumentNullException">throw if <paramref name="assembly"/> is null</exception>
		public ILoggerRepository GetRepository(Assembly assembly)
		{
			return CreateRepository(assembly, m_defaultRepositoryType);
		}

		/// <summary>
		/// Get the <see cref="ILoggerRepository"/> for the specified domain
		/// </summary>
		/// <param name="domain">the domain to use to lookup to the <see cref="ILoggerRepository"/></param>
		/// <returns>The <see cref="ILoggerRepository"/> for the domain</returns>
		/// <exception cref="ArgumentNullException">throw if <paramref name="domain"/> is null</exception>
		/// <exception cref="LogException">throw if the <paramref name="domain"/> does not exist</exception>
		public ILoggerRepository GetRepository(string domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}

			lock(this)
			{
				// Lookup in map
				ILoggerRepository rep = m_domain2repositoryMap[domain] as ILoggerRepository;
				if (rep == null)
				{
					throw new LogException("Domain ["+domain+"] is NOT defined.");
				}
				return rep;
			}
		}

		/// <summary>
		/// Create a new repository for the assembly specified 
		/// </summary>
		/// <param name="assembly">the assembly to use to create the domain to associate with the <see cref="ILoggerRepository"/></param>
		/// <param name="repositoryType">the type of repository to create, must implement <see cref="ILoggerRepository"/></param>
		/// <returns>the repository created</returns>
		/// <remarks>
		/// <para>The <see cref="ILoggerRepository"/> created will be associated with the domain
		/// specified such that a call to <see cref="GetRepository(Assembly)"/> with the
		/// same assembly specified will return the same repository instance.</para>
		/// 
		/// <para>Assemblies use the default domain.</para>
		/// </remarks>
		/// <exception cref="ArgumentNullException">throw if <paramref name="assembly"/> is null</exception>
		public ILoggerRepository CreateRepository(Assembly assembly, Type repositoryType)
		{
			// If the type is not set then use the default type
			if (repositoryType == null)
			{
				repositoryType = m_defaultRepositoryType;
			}

			lock(this)
			{
				// First check that the domain does not exist
				ILoggerRepository rep = m_domain2repositoryMap[DEFAULT_DOMAIN_NAME] as ILoggerRepository;
				if (rep == null)
				{
					// Must create the domain
					rep = CreateRepository(DEFAULT_DOMAIN_NAME, repositoryType);
				}

				return rep;
			}		
		}

		/// <summary>
		/// Create a new repository for the domain specified
		/// </summary>
		/// <param name="domain">the domain to associate with the <see cref="ILoggerRepository"/></param>
		/// <param name="repositoryType">the type of repository to create, must implement <see cref="ILoggerRepository"/>.
		/// If this param is null then the default repository type is used.</param>
		/// <returns>the repository created</returns>
		/// <remarks>
		/// The <see cref="ILoggerRepository"/> created will be associated with the domain
		/// specified such that a call to <see cref="GetRepository(string)"/> with the
		/// same domain specified will return the same repository instance.
		/// </remarks>
		/// <exception cref="ArgumentNullException">throw if <paramref name="domain"/> is null</exception>
		/// <exception cref="LogException">throw if the <paramref name="domain"/> already exists</exception>
		public ILoggerRepository CreateRepository(string domain, Type repositoryType)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}

			// If the type is not set then use the default type
			if (repositoryType == null)
			{
				repositoryType = m_defaultRepositoryType;
			}

			lock(this)
			{
				ILoggerRepository rep = null;

				// First check that the domain does not exist
				rep = m_domain2repositoryMap[domain] as ILoggerRepository;
				if (rep != null)
				{
					throw new LogException("Domain ["+domain+"] is already defined. Domains cannot be redefined.");
				}
				else
				{
					LogLog.Debug("DefaultRepositorySelector: Creating repository for domain ["+domain+"] using type ["+repositoryType+"]");

					// Call the no arg constructor for the repositoryType
					rep = (ILoggerRepository)repositoryType.GetConstructor(SystemInfo.EmptyTypes).Invoke(null);

					// Set the name of the repository
					rep.Name = domain;

					// Store in map
					m_domain2repositoryMap[domain] = rep;

					// Notify listeners that the repository has been created
					FireLoggerRepositoryCreatedEvent(rep);
				}

				return rep;
			}
		}

		/// <summary>
		/// Copy the list of <see cref="ILoggerRepository"/> objects
		/// </summary>
		/// <returns>an array of all known <see cref="ILoggerRepository"/> objects</returns>
		public ILoggerRepository[] GetAllRepositories()
		{
			lock(this)
			{
				ICollection reps = m_domain2repositoryMap.Values;
				ILoggerRepository[] all = new ILoggerRepository[reps.Count];
				reps.CopyTo(all, 0);
				return all;
			}
		}

		#endregion

		/// <summary>
		/// Event to notify that a logger repository has been created.
		/// </summary>
		/// <value>
		/// Event to notify that a logger repository has been created.
		/// </value>
		public event LoggerRepositoryCreationEventHandler LoggerRepositoryCreatedEvent
		{
			add { m_loggerRepositoryCreatedEvent += value; }
			remove { m_loggerRepositoryCreatedEvent -= value; }
		}

		/// <summary>
		/// Notify the registered listeners that the repository has been created
		/// </summary>
		/// <param name="repository">The repository that has been created</param>
		protected void FireLoggerRepositoryCreatedEvent(ILoggerRepository repository)
		{
			if (m_loggerRepositoryCreatedEvent != null)
			{
				m_loggerRepositoryCreatedEvent(this, new LoggerRepositoryCreationEventArgs(repository));
			}
		}
	}
}