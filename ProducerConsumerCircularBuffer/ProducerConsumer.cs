using System;
using System.Windows.Forms;
using System.Threading;

namespace ProducerConsumerCircularBuffer
{
	public class HoldResourceSynchronized
	{
		#region Private Member Fields
		//each array element is buffer 
		private int[] m_intCircularBuffer = {-1, -1, -1};

		//maintains count of occupied buffers
		private int m_intOccupiedBuffCount = 0;

		//variables that maintain read & write buffer locations
		private int m_intReadLoc = 0;
		private int m_intWriteLoc = 0;

		//GUI component to Display output
		private TextBox m_txtBxOutput;

		public static bool m_Flag = false;

		/*
		 *	private string strOutput = "";
		 * private OutputMessage hMessage;
		 */
		#endregion

		public HoldResourceSynchronized(TextBox ctrlOutput)
		{
			// 
			// TODO: Add constructor logic here
			//
			m_txtBxOutput = ctrlOutput;
		}

		/// <summary>
		/// Gets & Sets Shared buffer
		/// </summary>
		 public int Buffer
		 {
			 get
			 {
				 //Monitor.Enter(this);
				 //lock this Object while getting value from buffers array
				 lock(this)
				 {
					 //if there is no data to read, Then Wait [WaitSleepJoin State]
					 if(m_intOccupiedBuffCount == 0)
					 {
						 m_txtBxOutput.Text += "\r\nAll buffers empty. " + Thread.CurrentThread.Name + " waits.";
						 m_txtBxOutput.ScrollToCaret();
						 Monitor.Wait(this);
					 }

					 //Obtain value at current m_intReadLoc, then add string indicating consumed value to output
					 int intReadValue = m_intCircularBuffer[m_intReadLoc];
					 m_txtBxOutput.Text += "\r\n" + Thread.CurrentThread.Name + " reads " + m_intCircularBuffer[m_intReadLoc] + " from " + m_intReadLoc +" ";

					 //Just consumed a value, so decrement number of occupied buffers
					 --m_intOccupiedBuffCount;

					 //update m_intReadLoc for future read operation, then add current state to output
					 m_intReadLoc = (m_intReadLoc + 1) % m_intCircularBuffer.Length;
					 m_txtBxOutput.Text += CreateStateOutput();
					 m_txtBxOutput.ScrollToCaret();

					 Monitor.Pulse(this);
					
					 return intReadValue;
				 }
				 //Monitor.Exit(this);
			 }
			 set
			 {

				 //Monitor.Enter(this);
				 //Lock this object while setting value in buffers array
				 lock(this)
				 {
					 //if there are no empty location, Then Wait [WaitSleepJoin State]
					 if(m_intOccupiedBuffCount == m_intCircularBuffer.Length)
					 {
						 m_txtBxOutput.Text += "\r\nAll buffers full. " + Thread.CurrentThread.Name + " waits.";
						 m_txtBxOutput.ScrollToCaret();

						 Monitor.Wait(this);
					 }

					 //place value in m_intWriteLoc of Buffers, then add string that indicating produced value output
					 m_intCircularBuffer[m_intWriteLoc] = value;

					 m_txtBxOutput.Text += "\r\n"+Thread.CurrentThread.Name + " writes " + m_intCircularBuffer[m_intWriteLoc] + " in " + m_intWriteLoc + " ";
					 
					 //Just Produced a value, so increment number of occupied buffers
					 ++m_intOccupiedBuffCount;

					//update m_intWriteLoc for future write operation, then add current state to output
					 m_intWriteLoc = (m_intWriteLoc + 1) % m_intCircularBuffer.Length;
					 m_txtBxOutput.Text += CreateStateOutput();
					 m_txtBxOutput.ScrollToCaret();

                     //return waiting thread (if there is one) to Started State
					 Monitor.Pulse(this);
				 }
				 //Monitor.Exit(this);
			 }
		 }
		

		public string CreateStateOutput()
		{
			//Display 1st line of State information
			string strOutput = "(buffers occupied: " + m_intOccupiedBuffCount + ")\r\nbuffers: ";
			for(int i=0; i<m_intCircularBuffer.Length; i++)
				strOutput +=" " + m_intCircularBuffer[ i ] +  "  ";

			strOutput += "\r\n";

			//Display 2nd line of State information
			strOutput += "         ";
			for(int i=0; i<m_intCircularBuffer.Length; i++)
				strOutput += "---- ";

			strOutput += "\r\n";

			//Display 3rd line of State information
			strOutput += "         ";

			//Display Read Location (R) Write Location (W)
			for(int i=0; i<m_intCircularBuffer.Length; i++)

				if(i == m_intWriteLoc && i == m_intReadLoc)
					strOutput += " WR  ";
			else if(i == m_intWriteLoc)
					strOutput += " W   ";
			else if(i == m_intReadLoc)
					strOutput += "  R  ";
			else
					strOutput += "     ";
 
			strOutput += "\r\n";

			return strOutput;
		}
	}


	public class Producer
	{
		private HoldResourceSynchronized m_SharedResource;
		private Random m_randSleepTime;
		private TextBox  m_txtBxOutput;

		public Producer(HoldResourceSynchronized shared, Random rand, TextBox ctrlOutput)
		{
			// 
			// TODO: Add constructor logic here
			//
			m_SharedResource = shared;
			m_randSleepTime = rand;
			m_txtBxOutput = ctrlOutput;
		}


		public void Produce()
		{
			//for(int i=1; i<=4; i++)
			int i = 1;
			while(HoldResourceSynchronized.m_Flag)
			{
				Thread.Sleep(m_randSleepTime.Next(1,3000));
				m_SharedResource.Buffer = i;
				i++;
			}
			string ThreadName = Thread.CurrentThread.Name;
			m_txtBxOutput.Text += "\r\n" + ThreadName + " done producing.\r\n" + ThreadName + " Terminated.\r\n";
			m_txtBxOutput.ScrollToCaret();
		}
	}


	public class Consumer
	{
		private HoldResourceSynchronized m_SharedResource;
		private Random m_randSleepTime;
		private TextBox m_txtBxOutput;

		public Consumer(HoldResourceSynchronized shared, Random rand, TextBox ctrlOutput)
		{
			// 
			// TODO: Add constructor logic here
			//
			m_SharedResource = shared;
			m_randSleepTime = rand;
			m_txtBxOutput = ctrlOutput;
		}

		public void Consume()
		{
			int sum = 0;

			//for(int i=1; i<=4; i++)
			while(HoldResourceSynchronized.m_Flag)
			{
				Thread.Sleep(m_randSleepTime.Next(1,3000));
				sum += m_SharedResource.Buffer;
			}
			string ThreadName = Thread.CurrentThread.Name;
			m_txtBxOutput.Text += "\r\nTotal " + ThreadName + " Consumed: " + sum + ".\r\n" + ThreadName + " terminated\r\n";
			m_txtBxOutput.ScrollToCaret();
		}
	}
}
