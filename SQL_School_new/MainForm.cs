using SQL_School_new.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_School_new
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// При клике открывает дочернюю форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataAdapterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            Form form = Application.OpenForms["Students_DA"];

            if (form == null)
            {
                Forms.Students_DA students_DA = new Forms.Students_DA();
                //students_DA.MdiParent = this;
                students_DA.Show();
            }
            else
            {
                form.Focus();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Вы уверены, что хотите выйти?", "Завершение программы", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
