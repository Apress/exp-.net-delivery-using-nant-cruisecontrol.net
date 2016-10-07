#region references

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
	///	Displays a list of all saved transformations.
	/// </summary>
	public class TransformationList : System.Web.UI.UserControl
	{
		#region Fields

		/// <summary>
		/// The Repeater which displays the list of transformations.
		/// </summary>
		protected System.Web.UI.WebControls.Repeater rptTransformations;

		#endregion

		#region Event Handlers

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				BindTransformations();
		}

		#endregion

		#region Methods

		private void BindTransformations()
		{
			ApplicationEngine engine = new ApplicationEngine();
			rptTransformations.DataSource = engine.GetAllTransformations();
			rptTransformations.DataBind();
		}

		#endregion

		#region Web Form Designer generated code

		/// <summary>
		/// Init Event
		/// </summary>
		/// <param name="e">event arguments</param>
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
