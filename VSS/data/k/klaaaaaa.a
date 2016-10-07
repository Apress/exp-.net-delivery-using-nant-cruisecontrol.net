#region References

using System;
using System.IO;
using System.Collections;

using Etomic.ShareTransformer.Engine.Core;
using Etomic.ShareTransformer.Engine.DataAccess;

#endregion

namespace Etomic.ShareTransformer.Engine
{
	/// <summary>
	/// Provides logic and functionality for the Transformer GUI application
	/// </summary>
	public class ApplicationEngine
	{
		#region Fields

		#endregion

		#region Constructors

		/// <summary>
		/// Default parameterless constructor
		/// </summary>
		public ApplicationEngine()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the content of the specified file
		/// </summary>
		/// <param name="path">The path to the specified file</param>
		/// <returns>string containing file contents</returns>
		public string GetFileContents(string path)
		{
			if(!File.Exists(path))
				throw new ArgumentException("File does not exist for the provided path", "path");

			using(StreamReader sr = new StreamReader(path))
			{
				return sr.ReadToEnd();
			}
		}

		/// <summary>
		/// Saves the contents specified in the <c>contents</c> parameter 
		/// to the file specified in the <c>path</c> parameter.
		/// </summary>
		/// <param name="contents">The contents of the file to save</param>
		/// <param name="path">The path where the contents should be written to</param>
		public void SaveFileContents(string contents, string path)
		{
			if(contents == null || contents.Length == 0)
				throw new ArgumentException("The contents of the file are null or zero-length.");

			using(StreamWriter sw = new StreamWriter(path, false))
			{
				sw.Write(contents);
			}
		}

		/// <summary>
		/// Saves the provided transformation to the database.
		/// </summary>
		/// <param name="transformation">A <c>Transformation</c></param>
		/// <returns>The ID of the transformation</returns>
		internal int SaveTransformation(Transformation transformation)
		{
			TransformationDal dal = new TransformationDal(ConfigConstants.DbConnectionString);
			return dal.SaveTransformation(transformation);
		}

		/// <summary>
		/// Gets a single <c>Transformation</c> from the database.
		/// </summary>
		/// <param name="transformId">The ID of the record</param>
		/// <returns>A <c>Transformation</c> instance</returns>
		internal Transformation GetTransformation(int transformId)
		{
			if(transformId <= 0)
				throw new ArgumentException("transformId must not be less than or equal to zero");

			TransformationDal dal = new TransformationDal(ConfigConstants.DbConnectionString);
			return dal.GetTransformation(transformId);
		}

		/// <summary>
		/// Gets a list of all saved transformations.
		/// </summary>
		/// <returns>ArrayList of transformations</returns>
		public IList GetAllTransformations()
		{
			TransformationDal dal = new TransformationDal(ConfigConstants.DbConnectionString);
			return dal.GetAllTransformations();
		}

		#endregion
	}
}
