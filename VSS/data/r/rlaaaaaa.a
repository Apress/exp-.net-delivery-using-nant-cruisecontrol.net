#region References

using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

using Etomic.ShareTransformer.Engine.Core;

using Microsoft.ApplicationBlocks.Data;

#endregion

namespace Etomic.ShareTransformer.Engine.DataAccess
{
	/// <summary>
	/// Provides data access functionality.
	/// </summary>
	internal sealed class TransformationDal
	{
		#region Fields

		private string _dbConnection = string.Empty;

		#endregion

		#region SQL Constants

		const string SAVE_TRANSFORMATION_QUERY = @"
			INSERT INTO Transformations(XmlContent, XslContent, Output, Title) 
			VALUES(@XmlContent, @XslContent, @Output, @Title);
			SELECT @@IDENTITY AS InsertedId";

		const string GET_ALL_TRANSFORMATIONS_QUERY = @"
			SELECT TransformId, XmlContent, XslContent, Output, Title FROM Transformations ORDER BY Title";

		const string GET_TRANSFORMATION_QUERY = @"
			SELECT TransformId, XmlContent, XslContent, Output, Title FROM Transformations WHERE TransformId = @id";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes the type with the necessary SQL Server database connection string.
		/// </summary>
		/// <param name="dbConnection">SQL server database connection string</param>
		internal TransformationDal(string dbConnection)
		{
			if(dbConnection == null || dbConnection.Length == 0)
				throw new ArgumentException("dbConnection must not be null or zero-length", "dbConnection");

			_dbConnection = dbConnection;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves the provided <c>Transformation</c> to the database.
		/// </summary>
		/// <param name="transformation">The <c>Transformation</c> to save</param>
		/// <returns>The ID of the newly inserted transformation</returns>
		internal int SaveTransformation(Transformation transformation)
		{
			SqlParameter xml	= new SqlParameter("@XmlContent", transformation.Xml);
			SqlParameter xsl	= new SqlParameter("@XslContent", transformation.Xslt);
			SqlParameter output = new SqlParameter("@Output", transformation.Output);
			SqlParameter title	= new SqlParameter("@Title", transformation.Title);

			int id = Convert.ToInt32(
						SqlHelper.ExecuteScalar(_dbConnection, 
							CommandType.Text, 
							SAVE_TRANSFORMATION_QUERY, 
							xml, xsl, output, title));

			return id;
		}

		/// <summary>
		/// Gets an IList containing all the transformations stored in the database.
		/// </summary>
		/// <returns>IList containing all transformations stored in the database.</returns>
		internal IList GetAllTransformations()
		{
			ArrayList transformations = new ArrayList();

			using(SqlDataReader dr = SqlHelper.ExecuteReader(_dbConnection, CommandType.Text, GET_ALL_TRANSFORMATIONS_QUERY))
			{
				while(dr.Read())
					transformations.Add(GetTransformationFromReader(dr));
			}

			return transformations;
		}

		/// <summary>
		/// Gets a single Transformation.
		/// </summary>
		/// <param name="transformationId">The Id of the Transformation</param>
		/// <returns>The Transformation represented by Id</returns>
		internal Transformation GetTransformation(int transformationId)
		{
			Transformation t = null;
			SqlParameter id  = new SqlParameter("@id", transformationId);

			using(SqlDataReader dr = SqlHelper.ExecuteReader(_dbConnection, CommandType.Text, GET_TRANSFORMATION_QUERY, id))
			{
				if(dr.Read())
					t = GetTransformationFromReader(dr);
			}

			return t;
		}


		#endregion

		#region Helper Methods

		private Transformation GetTransformationFromReader(SqlDataReader dr)
		{
			Transformation transformation = new Transformation();
			
			transformation.Id	  = dr.GetInt32(0);
			transformation.Xml    = dr.GetString(1);
			transformation.Xslt	  = dr.GetString(2);
			transformation.Output = dr.GetString(3);
			transformation.Title  = dr.GetString(4);

			return transformation;
		}

		#endregion
	}
}
