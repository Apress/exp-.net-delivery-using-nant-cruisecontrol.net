#region References

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using TransformerEngine;

#endregion

namespace TransformerGui
{
	/// <summary>
	/// The main application form for the Transformer GUI.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		#region Fields

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuSave;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuAbout;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabXml;
		private System.Windows.Forms.TabPage tabXslt;
		private System.Windows.Forms.TabPage tabOutput;
		private System.Windows.Forms.RichTextBox rtbXml;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.TextBox txtXslt;
		private System.Windows.Forms.TextBox txtXml;
		private System.Windows.Forms.MenuItem mnuLoad;
		private System.Windows.Forms.MenuItem mnuLoadXml;
		private System.Windows.Forms.MenuItem mnuLoadXslt;
		private System.Windows.Forms.Button btnTransform;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Entry Point

		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.Run(new MainForm());
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Event Handlers

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			ExitApplication();
		}

		private void mnuSave_Click(object sender, System.EventArgs e)
		{
			SaveOutput();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveOutput();
		}

		private void mnuExit_Click(object sender, System.EventArgs e)
		{
			ExitApplication();
		}

		private void mnuLoadXml_Click(object sender, System.EventArgs e)
		{
			LoadFileContent("XML files (*.xml)|*.xml", "Open XML File", txtXml);
		}

		private void mnuLoadXslt_Click(object sender, System.EventArgs e)
		{
			LoadFileContent("XSLT files (*.xslt)|*.xslt", "Open XSLT File", txtXslt);
		}

		private void btnTransform_Click(object sender, System.EventArgs e)
		{
			if(txtXml.Text.Length > 0 || txtXslt.Text.Length > 0)
			{
				try
				{
					ApplicationEngine engine = new ApplicationEngine();
					txtOutput.Text = engine.TransformXml(txtXml.Text, txtXslt.Text);
				}
				catch(Exception ex)
				{
					txtOutput.Text = ex.ToString();
				}
			}
			else
				DisplayWarning("You must specify a value for both the XML data and XSLT");
		}

		#endregion

		#region Methods

		private void DisplayWarning(string text)
		{
			MessageBox.Show(text, 
				"Warning", 
				MessageBoxButtons.OK, 
				MessageBoxIcon.Warning);
		}

		private void SaveOutput()
		{
			if(txtOutput.Text.Length > 0)
			{
				using(SaveFileDialog saveDialog = new SaveFileDialog())
				{
					saveDialog.Title  = "Save Output Content";
					saveDialog.Filter = "HTML Files (*.htm)|*.htm|XML Files (*.xml)|*.xml|TXT Files (*.txt)|*.txt|All Files (*.*)|*.*";

					if(saveDialog.ShowDialog(this) == DialogResult.OK)
					{
						ApplicationEngine engine = new ApplicationEngine();
						engine.SaveFileContents(txtOutput.Text, saveDialog.FileName);
					}
				}
			}
			else
				DisplayWarning("There is no output to save");
		}

		private void LoadFileContent(string filter, string title, TextBox targetTextBox)
		{
			using(OpenFileDialog openDialog = new OpenFileDialog())
			{
				openDialog.CheckFileExists = true;
				openDialog.AddExtension	   = true;
				openDialog.CheckPathExists = true;
				openDialog.Filter		   = filter;
				openDialog.Title		   = title;

				if(openDialog.ShowDialog(this) == DialogResult.OK)
				{
					ApplicationEngine engine = new ApplicationEngine();
					string text = engine.GetFileContents(openDialog.FileName);

					targetTextBox.Text = text;
				}
			}
		}

		private void ExitApplication()
		{
			Application.Exit();
		}

		#endregion

		#region Designer Code

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuLoad = new System.Windows.Forms.MenuItem();
			this.mnuLoadXml = new System.Windows.Forms.MenuItem();
			this.mnuLoadXslt = new System.Windows.Forms.MenuItem();
			this.mnuSave = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuAbout = new System.Windows.Forms.MenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabXml = new System.Windows.Forms.TabPage();
			this.txtXml = new System.Windows.Forms.TextBox();
			this.tabXslt = new System.Windows.Forms.TabPage();
			this.txtXslt = new System.Windows.Forms.TextBox();
			this.tabOutput = new System.Windows.Forms.TabPage();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.rtbXml = new System.Windows.Forms.RichTextBox();
			this.btnTransform = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabXml.SuspendLayout();
			this.tabXslt.SuspendLayout();
			this.tabOutput.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnTransform);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.btnExit);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(2, 331);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(588, 40);
			this.panel1.TabIndex = 0;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(424, 8);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnExit.Location = new System.Drawing.Point(504, 8);
			this.btnExit.Name = "btnExit";
			this.btnExit.TabIndex = 0;
			this.btnExit.Text = "&Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuFile,
																					  this.mnuHelp});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuLoad,
																					this.mnuSave,
																					this.menuItem2,
																					this.mnuExit});
			this.mnuFile.Text = "&File";
			// 
			// mnuLoad
			// 
			this.mnuLoad.Index = 0;
			this.mnuLoad.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuLoadXml,
																					this.mnuLoadXslt});
			this.mnuLoad.Text = "&Load";
			// 
			// mnuLoadXml
			// 
			this.mnuLoadXml.Index = 0;
			this.mnuLoadXml.Text = "&XML...";
			this.mnuLoadXml.Click += new System.EventHandler(this.mnuLoadXml_Click);
			// 
			// mnuLoadXslt
			// 
			this.mnuLoadXslt.Index = 1;
			this.mnuLoadXslt.Text = "X&SLT...";
			this.mnuLoadXslt.Click += new System.EventHandler(this.mnuLoadXslt_Click);
			// 
			// mnuSave
			// 
			this.mnuSave.Index = 1;
			this.mnuSave.Text = "&Save Output...";
			this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "-";
			// 
			// mnuExit
			// 
			this.mnuExit.Index = 3;
			this.mnuExit.Text = "&Exit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 1;
			this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuAbout});
			this.mnuHelp.Text = "&Help";
			// 
			// mnuAbout
			// 
			this.mnuAbout.Index = 0;
			this.mnuAbout.Text = "&About";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabXml);
			this.tabControl.Controls.Add(this.tabXslt);
			this.tabControl.Controls.Add(this.tabOutput);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(2, 2);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(588, 329);
			this.tabControl.TabIndex = 1;
			// 
			// tabXml
			// 
			this.tabXml.Controls.Add(this.txtXml);
			this.tabXml.Location = new System.Drawing.Point(4, 22);
			this.tabXml.Name = "tabXml";
			this.tabXml.Size = new System.Drawing.Size(580, 303);
			this.tabXml.TabIndex = 0;
			this.tabXml.Text = "XML Content";
			// 
			// txtXml
			// 
			this.txtXml.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtXml.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtXml.Location = new System.Drawing.Point(0, 0);
			this.txtXml.MaxLength = 0;
			this.txtXml.Multiline = true;
			this.txtXml.Name = "txtXml";
			this.txtXml.Size = new System.Drawing.Size(580, 303);
			this.txtXml.TabIndex = 0;
			this.txtXml.Text = "";
			// 
			// tabXslt
			// 
			this.tabXslt.Controls.Add(this.txtXslt);
			this.tabXslt.Location = new System.Drawing.Point(4, 22);
			this.tabXslt.Name = "tabXslt";
			this.tabXslt.Size = new System.Drawing.Size(580, 303);
			this.tabXslt.TabIndex = 1;
			this.tabXslt.Text = "XSL Template";
			// 
			// txtXslt
			// 
			this.txtXslt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtXslt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtXslt.Location = new System.Drawing.Point(0, 0);
			this.txtXslt.MaxLength = 900000;
			this.txtXslt.Multiline = true;
			this.txtXslt.Name = "txtXslt";
			this.txtXslt.Size = new System.Drawing.Size(580, 303);
			this.txtXslt.TabIndex = 0;
			this.txtXslt.Text = "";
			// 
			// tabOutput
			// 
			this.tabOutput.Controls.Add(this.txtOutput);
			this.tabOutput.Location = new System.Drawing.Point(4, 22);
			this.tabOutput.Name = "tabOutput";
			this.tabOutput.Size = new System.Drawing.Size(580, 303);
			this.tabOutput.TabIndex = 2;
			this.tabOutput.Text = "Output";
			// 
			// txtOutput
			// 
			this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutput.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtOutput.Location = new System.Drawing.Point(0, 0);
			this.txtOutput.MaxLength = 900000;
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.Size = new System.Drawing.Size(580, 303);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.Text = "";
			// 
			// rtbXml
			// 
			this.rtbXml.Location = new System.Drawing.Point(248, 168);
			this.rtbXml.Name = "rtbXml";
			this.rtbXml.TabIndex = 0;
			this.rtbXml.Text = "";
			// 
			// btnTransform
			// 
			this.btnTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTransform.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTransform.Location = new System.Drawing.Point(344, 8);
			this.btnTransform.Name = "btnTransform";
			this.btnTransform.TabIndex = 2;
			this.btnTransform.Text = "&Transform";
			this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(592, 373);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panel1);
			this.DockPadding.All = 2;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "Transformer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.panel1.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabXml.ResumeLayout(false);
			this.tabXslt.ResumeLayout(false);
			this.tabOutput.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion
	}
}
