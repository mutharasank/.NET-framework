using System;
using System.Threading;

namespace ConsumerProducerWithSynch
{

	public delegate void OutputMessage(string strOut);

	public class HoldResourceSynchronized
	{
		private int intBuffer = -1;
		private int intOccupiedBuffCount = 0;

		public static bool m_Flag = false;
		private string strOutput = "";
		private OutputMessage hMessage;
		public HoldResourceSynchronized(OutputMessage OutMessage)
		{
			// 
			// TODO: Add constructor logic here
			//
			hMessage = OutMessage;
		}

		/// <summary>
		/// Gets & Sets Shared buffer
		/// </summary>
		 public int Buffer
		 {
			 get
			 { //lock this Object while getting value from buffer
				 Monitor.Enter(this);

				 //if there is no data to read, Then Wait [WaitSleepJoin State]
				 if(intOccupiedBuffCount == 0)
				 {
					 strOutput =  "\r\n"+ Thread.CurrentThread.Name + " tries to read.\r\n";
					strOutput += DisplayState("Buffer empty. " + Thread.CurrentThread.Name + " waits.") + "\r\n";
					 hMessage(strOutput);

					 Monitor.Wait(this);
				 }

				//Just consumed a value, so decrement number of occupied buffers
				 --intOccupiedBuffCount;

				 strOutput = DisplayState(Thread.CurrentThread.Name + " reads " +intBuffer.ToString() + "\t") ;
				 hMessage(strOutput);

				 Monitor.Pulse(this);

				 /*
				  *	Get Copy of intBuffer releasing lock.
				  * It is possible that the producer could be assigned the processor immediately after the Monitor
				  * is released & before the the return statment executes, In this case the producer would assign a 
				  * new value to the intBuffer before return statement returns the value to the consumer.
				  * Making a copy of the Original Buffer solve this issue.
				  */
				 int intBufferCopy = intBuffer;
				//Release Object 
				 Monitor.Exit(this);
				 
				 return intBufferCopy;
			 }
			 set
			 {
				//Lock this object while setting value in buffer
				 Monitor.Enter(this);

				 //if there are no empty location, Then Wait [WaitSleepJoin State]
				 if(intOccupiedBuffCount == 1)
				 {
					 strOutput = "\r\n"+ Thread.CurrentThread.Name + " tries to write.\r\n";
					 strOutput += DisplayState("Buffer full. "+Thread.CurrentThread.Name + " waits.")  + "\r\n";
					 hMessage(strOutput);
					 
					 Monitor.Wait(this);
				 }
				 intBuffer = value;

				 //Just Produced a value, so increment number of occupied buffers
				 ++intOccupiedBuffCount;

				 strOutput = DisplayState(Thread.CurrentThread.Name + " writes " + intBuffer.ToString() +"\t");
				 hMessage(strOutput);

				 //return waiting thread (if there is one) to Started State
				 Monitor.Pulse(this);

				 Monitor.Exit(this);
			 }
		 }
		
		public string DisplayState(string strOp)
		{
			return String.Format("{0,-50}{1,-20}{2}",strOp, intBuffer.ToString(), intOccupiedBuffCount.ToString()) + "\r\n";
		}

	}


	public class Producer
	{
		private HoldResourceSynchronized m_SharedResource;
		private Random m_randSleepTime;

		private string m_strOutput;

		private OutputMessage hMessage;

		public Producer(HoldResourceSynchronized shared, Random rand, OutputMessage outMessage)
		{
			// 
			// TODO: Add constructor logic here
			//
			m_SharedResource = shared;
			m_randSleepTime = rand;
			hMessage = outMessage;
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
			Message = Thread.CurrentThread.Name + " done producing.\r\nTerminating " + Thread.CurrentThread.Name +".\r\n";
			hMessage(Message);
		}

		public string Message
		{
			get
			{
				return m_strOutput;
			}
			set
			{
				m_strOutput = value;
			}
		}
	}


	public class Consumer
	{
		private HoldResourceSynchronized m_SharedResource;
		private Random m_randSleepTime;

		private string m_strOutput;

		private OutputMessage hMessage;

		public Consumer(HoldResourceSynchronized shared, Random rand, OutputMessage outMessage)
		{
			// 
			// TODO: Add constructor logic here
			//
			m_SharedResource = shared;
			m_randSleepTime = rand;
			hMessage = outMessage;
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
			Message = Thread.CurrentThread.Name + " read values totaling: " + sum.ToString() + "\r\nTerminating " + 
				Thread.CurrentThread.Name+ ".\r\n";
			hMessage(Message);
		}
		public string Message
		{
			get
			{
				return m_strOutput;
			}
			set
			{
				m_strOutput = value;
			}
		}
	}
}
