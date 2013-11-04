using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tweakker_database_system
{
    public partial class Form1 : Form
    {
        private BusinessService service;


        /// <summary>
        /// Variable to store error message
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// Member id
        /// </summary>
        private int memberId;

        /// <summary>
        /// Initializes a new instance of the Manage class
        /// </summary>
        /// 
        public Form1()
        {
            InitializeComponent();
            service = new BusinessService();
        
            
        }
         //Insert button clicked
        private void bInsert_Click(object sender, EventArgs e)
        {
            service.Insert();
        }

        //Update button is clicked
        private void bUpdate_Click(object sender, EventArgs e)
        {
            service.Update();
        }

        //Delete button is clicked
        private void bDelete_Click(object sender, EventArgs e)
        {
            service.Delete();
        }

        //Select button is clicked
        private void bSelect_Click(object sender, EventArgs e)
        {
            List<string>[] list;
            list = service.Select();

            dgDisplay.Rows.Clear();
            for(int i = 0; i < list[0].Count; i++)
            {
                int number = dgDisplay.Rows.Add();
                dgDisplay.Rows[number].Cells[0].Value = list[0][i];
                dgDisplay.Rows[number].Cells[1].Value = list[1][i];
                dgDisplay.Rows[number].Cells[2].Value = list[2][i];                
            }
        }

        //Count button clicked
        private void bCount_Click(object sender, EventArgs e)
        {
            int Count = service.Count();

            dgDisplay.Rows.Clear();
            int number = dgDisplay.Rows.Add();
            dgDisplay.Rows[number].Cells[0].Value = Count + "";
        }

        //Backup button clicked
        private void bBackup_Click(object sender, EventArgs e)
        {
            service.Backup();
        }

        //Restore button clicked
        private void bRestore_Click(object sender, EventArgs e)
        {
            service.Restore();
        }

        
       

    }
}
