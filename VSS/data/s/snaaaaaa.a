#region References

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Etomic.ShareTransformer.Engine;
using Etomic.ShareTransformer.Engine.Core;

#endregion

namespace Etomic.ShareTransformer.UI.WebControls
{
	/// <summary>
	///	Performs an XSLT transformation via the provided XML data and XSL template. 
	///	Can save the result of a transformation to the database.
	/// </summary>
	public class TransformerControl : System.Web.UI.UserControl
	{
		#region Fields

		/// <summary>
		/// The TextBox with which the user can enter XML data.
		/// </summary>
		protected TextBox txtXml;
		/// <summary>
		/// The TextBox with which the user can enter the XSLT.
		/// </summary>
		protected TextBox txtXslt;
		/// <summary>
		/// The TextBox used to output the result of a transformation.
		/// </summary>
		protected TextBox txtOutput;
		/// <summary>
		/// The Label used to announce any messages to the user.
		/// </summary>
		protected Label lblMessages;
		/// <summary>
		/// The button which saves the XML, XSL and any transformation output.
		/// </summary>
		protected System.Web.UI.WebControls.Button btnSaveOutput;
		/// <summary>
		/// The title given to the transformation.
		/// </summary>
		protected System.Web.UI.WebControls.TextBox txtTitle;
		/// <summary>
		/// Validates the Title field
		/// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator vldTitle;
		/// <summary>
		/// Validates the XML field
		/// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator vldXml;
		/// <summary>
		/// Validates the XSLT field
		/// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator vldXslt;
		/// <summary>
		/// Validates the Output field
		/// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator vldOutput;
		/// <summary>
		/// The Button which causes the transformation.
		/// </summary>
		protected Button btnTransform;

		#endregion

		#region Event Handlers

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				LoadTransformation();
				ToggleSaveButton();
			}
			catch(ArgumentException ex)
			{
				lblMessages.Text = ex.Message;
			}
			catch(FormatException ex)
			{
				lblMessages.Text = ex.Message;
			}
		}

		/// <summary>
		/// Handles the Click event of the "Transform" button.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		protected void btnTransform_Click(object sender, EventArgs e)
		{
			TransformData();
		}

		/// <summary>
		/// Handles the Click event of the "Save Output" button.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		protected void btnSaveOutput_Click(object sender, EventArgs e)
		{
			SaveTransformation();
		}

		#endregion

		#region Properties

		private int TransformationId
		{
			get 
			{ 
				string id = Request.QueryString["id"];
				
				if(id == null || id.Equals(""))
					return 0;
				else
					return Convert.ToInt32(id);
			}
		}

		#endregion

		#region Methods

		private void SaveTransformation()
		{
			try
			{
				Transformation t = new Transformation();
				t.Title		= txtTitle.Text.Trim();
				t.Output	= txtOutput.Text.Trim();
				t.Xml		= txtXml.Text.Trim();
				t.Xslt		= txtXslt.Text.Trim();
				t.Save();

				Server.Transfer("Transformations.aspx");
			}
			catch(Exception ex)
			{
				lblMessages.Text = ex.Message;
			}
		}

		private void LoadTransformation()
		{
			if(TransformationId != 0)
			{
				Transformation t = new Transformation(TransformationId);

				txtOutput.Text = t.Output;
				txtXml.Text    = t.Xml;
				txtXslt.Text   = t.Xslt;
				txtTitle.Text  = t.Title;
			}
		}

		private void ToggleSaveButton()
		{
			if(TransformationId == 0)
			{
				btnSaveOutput.Enabled = 
					((txtOutput.Text.Length > 0) || (txtXml.Text.Length > 0) || (txtXslt.Text.Length > 0));
			}
			else
			{
				btnTransform.Enabled  = false;
				btnSaveOutput.Enabled = false;
			}
		}

		private void TransformData()
		{
			lblMessages.Text = "";
			string xml = txtXml.Text.Trim();
			string xsl = txtXslt.Text.Trim();

			if(xml.Length > 0 && xsl.Length > 0)
			{
				try
				{
					Transformation transform = new Transformation(xml, xsl);
					transform.Transform();

					txtOutput.Text = transform.Output;
				}
				catch(Exception ex)
				{
					lblMessages.Text = "An exception was thrown during transformation!";
					txtOutput.Text = ex.ToString();
				}
				finally
				{
					ToggleSaveButton();
				}
			}
			else
			{
				lblMessages.Text = "XML and XSLT are empty.";
				txtOutput.Text = "";
			}
		}

		#endregion

		#region Web Form Designer generated code

		/// <summary>
		/// Raises the Init event.
		/// </summary>
		/// <param name="e">Event arguments</param>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
