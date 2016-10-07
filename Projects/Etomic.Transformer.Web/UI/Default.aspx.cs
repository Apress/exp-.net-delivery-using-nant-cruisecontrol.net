#region References

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Etomic.Library.Transformer.Engine;

#endregion

namespace Etomic.Transformer.Web.UI
{
	/// <summary>
	/// Allows the user to specify both a piece of XML data, and an 
	/// XSLT template to transform with.
	/// </summary>
	public class DefaultCodeBehind : System.Web.UI.Page
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
		/// The Button which causes the transformation.
		/// </summary>
		protected Button btnTransform;

		#endregion

		#region Methods

		private void TransformData()
		{
			lblMessages.Text = "";
			string xml = txtXml.Text.Trim();
			string xsl = txtXslt.Text.Trim();

			if(xml.Length > 0 && xsl.Length > 0)
			{
				try
				{
					ApplicationEngine engine = new ApplicationEngine();
					txtOutput.Text = engine.TransformXml(xml, xsl);
				}
				catch(Exception ex)
				{
					lblMessages.Text = "An exception was thrown during transformation!";
					txtOutput.Text = ex.ToString();
				}
			}
			else
			{
				lblMessages.Text = "XML and XSLT are empty.";
			}
		}

		#endregion

		#region Event Handlers

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// The event handler for the "Transform" button.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		protected void btnTransform_Click(object sender, EventArgs e)
		{
			TransformData();
		}

		#endregion

		#region Web Form Designer generated code

		/// <summary>
		/// Invokes the <c>OnInit</c> event.
		/// </summary>
		/// <param name="e">The event arguments</param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
