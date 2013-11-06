using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Threading;

namespace ProducerConsumerCircularBuffer
{
	/// <summary>
	/// Summary description for frmProducerConsumer.
	/// </summary>
	public class frmProducerConsumer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtBxOutput;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnPauseResume;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Thread m_ProducerThread;
		private Thread m_ConsumerThread;

		private ThreadState m_GeneralState;

		private HoldResourceSynchronized m_SharedRes;

		public frmProducerConsumer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			Random randTime = new Random();

			m_SharedRes = new HoldResourceSynchronized(txtBxOutput);
			txtBxOutput.Text += m_SharedRes.CreateStateOutput();

			Producer producer = new Producer(m_SharedRes, randTime,txtBxOutput);
			Consumer consumer = new Consumer(m_SharedRes, randTime,txtBxOutput);

			m_ProducerThread = new Thread(new ThreadStart(producer.Produce));
			m_ProducerThread.Name = "Producer";

			m_ConsumerThread = new Thread(new ThreadStart(consumer.Consume));
			m_ConsumerThread.Name = "Consumer";

			m_GeneralState = ThreadState.Unstarted;
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
			this.txtBxOutput = new System.Windows.Forms.TextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtBxOutput
			// 
			this.txtBxOutput.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtBxOutput.Location = new System.Drawing.Point(8, 64);
			this.txtBxOutput.Multiline = true;
			this.txtBxOutput.Name = "txtBxOutput";
			this.txtBxOutput.ReadOnly = true;
			this.txtBxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtBxOutput.Size = new System.Drawing.Size(400, 424);
			this.txtBxOutput.TabIndex = 0;
			this.txtBxOutput.Text = "";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(8, 8);
			this.btnStart.Name = "btnStart";
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(336, 8);
			this.btnStop.Name = "btnStop";
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "Stop";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnPauseResume
			// 
			this.btnPauseResume.Location = new System.Drawing.Point(176, 8);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.TabIndex = 3;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
			// 
			// frmProducerConsumer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 501);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnPauseResume,
																		  this.btnStop,
																		  this.btnStart,
																		  this.txtBxOutput});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmProducerConsumer";
			this.Text = "Producer Consumer With Circular Buffer";
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
			HoldResourceSynchronized.m_Flag = true;
			m_ProducerThread.Start();
			m_ConsumerThread.Start();

			btnStart.Enabled = false;
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			HoldResourceSynchronized.m_Flag = false;
		}

		private void btnPauseResume_Click(object sender, System.EventArgs e)
		{
			if(m_GeneralState == ThreadState.Suspended)
			{
				btnPauseResume.Text = "Pause";
				m_ProducerThread.Resume();
				m_ConsumerThread.Resume();
				m_GeneralState = ThreadState.Running;
				return;
			}

			try
			{
				m_ProducerThread.Suspend();
				m_ConsumerThread.Suspend();
				m_GeneralState = ThreadState.Suspended;
				btnPauseResume.Text = "Resume";
			}
			catch(ThreadStateException err)
			{
				MessageBox.Show(err.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
	}
}
