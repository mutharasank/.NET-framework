using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Threading;

namespace ConsumerProducerWithSynch
{
	/// <summary>
	/// Summary description for frmProducerConsumer.
	/// </summary>
	public class frmProducerConsumer : System.Windows.Forms.Form
	{
		public  System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.Button btnStart;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private HoldResourceSynchronized m_SharedRes;
		private Thread m_ProducerThread;
		private System.Windows.Forms.Button btnStop;
		private Thread m_ConsumerThread;

		public frmProducerConsumer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			Random rand = new Random();
			m_SharedRes = new HoldResourceSynchronized( new OutputMessage(OutMessageHandle));
			Producer producer = new Producer(m_SharedRes,rand, new OutputMessage(OutMessageHandle));
			Consumer consumer = new Consumer(m_SharedRes,rand, new OutputMessage(OutMessageHandle));

			m_ProducerThread = new Thread(new ThreadStart(producer.Produce));
			m_ProducerThread.Name = "Producer";

			m_ConsumerThread = new Thread(new ThreadStart(consumer.Consume));
			m_ConsumerThread.Name = "Consumer";

			txtOutput.Text =String.Format("{0,-50}{1,-9}{2}\r\n","Operation","Buffer","Occupied Count");
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtOutput
			// 
			this.txtOutput.Location = new System.Drawing.Point(0, 48);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutput.Size = new System.Drawing.Size(360, 304);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.Text = "";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(88, 8);
			this.btnStart.Name = "btnStart";
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(192, 8);
			this.btnStop.Name = "btnStop";
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "Stop";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// frmProducerConsumer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 357);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnStop,
																		  this.btnStart,
																		  this.txtOutput});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmProducerConsumer";
			this.Text = "Producer Consumer Without Synch";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmProducerConsumer());
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			//m_SharedRes.DisplayState()
			
			HoldResourceSynchronized.m_Flag = true;
			m_ProducerThread.Start();
			m_ConsumerThread.Start();
			//btnStart.Enabled = false;
		}

		private void OutMessageHandle(string strOut)
		{
			txtOutput.Text += strOut;
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			HoldResourceSynchronized.m_Flag = false;
		}
	}
}
