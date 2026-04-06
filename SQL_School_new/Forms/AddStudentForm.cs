using SQL_School_new.Classes.DataAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SQL_School_new.Forms
{
    public partial class AddStudentForm : Form
    {

        private StudentsDA _studs = new StudentsDA();

        public AddStudentForm(StudentsDA studs)
        {
            InitializeComponent();

            _studs = studs;


            this.Load += AddStudentForm_Load;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The source of the event, typically the AddStudentForm instance.</param>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        private void AddStudentForm_Load(object sender, EventArgs e)
        {

            NewStudentGroup.Maximum = _studs.TabGroups.Rows.Count;
            //MessageBox.Show("количество групп: " + _studs.TabGroups.Rows.Count);

        }

        /// <summary>
        /// Метод выводит на экран сообщение об ошибке 
        /// </summary>
        /// <param name="sql_ErrorMessage"></param>
        private void showSqlError(string sql_ErrorMessage)
        {
            MessageBox.Show(sql_ErrorMessage, "Ошибка SQL ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_accept_Click(object sender, EventArgs e)
        {
            string student_name = NewStudentName.Text.Trim();
            if (student_name.Length == 0) { 
                MessageBox.Show("Введите имя студента!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime birthdate = NewStudentBirthdate.Value.Date;
            int group = (int)NewStudentGroup.Value;

            if (!_studs.AddStuds(student_name, birthdate, group))
            {
                showSqlError(_studs.SqlErrorMessage);
                this.Close();
            }
            else
            {
                MessageBox.Show("Студент успешно добавлен!");
            }

            this.Close();
        }
    }
}
