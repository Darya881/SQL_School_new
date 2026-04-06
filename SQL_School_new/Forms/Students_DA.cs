using SQL_School_new.Classes.DataAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SQL_School_new.Forms
{
    public partial class Students_DA : Form
    {

        // Форму будем использовать только для отображения данных,
        // и передачу действий пользователя (нажатие кнопки добавить студента и пр.)


        /// <summary>
        /// Объект который будет возвращать / изменять  данные для формы 
        /// обрабатывает запросы пользователей
        /// </summary>
        StudentsDA studs = new StudentsDA();
        int _id_stud = -1;  // id студента, которого выбрал пользователь в таблице
        int idGroup = -1;   // id группы, которую выбрал пользователь в комбобоксе

        public Students_DA()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;  // отключаем автоматическое создание столбцов, т.к. мы их задаем в свойствах таблицы
        }

        /// <summary>
        /// Событие перед первым отображением формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Students_DA_Load(object sender, EventArgs e)
        {
            // Т.к. comboBox1 групп будет привязан к данным из класса  StudentsDA таблицы TabGroups
            // то в свойствах  comboBox1 указываем наименование ожидаемых полей из этой таблицы (такие же как в SQL запросе)
            // если наименование полей указать не правильно, то будут отображатся обозначение типа DataRow
            comboBox1.DisplayMember = "group_number";       // Какое свойство класса показывать в выподающем списке
            comboBox1.ValueMember = "id_group";             // Какое свойство использовать как значение (ID) выбранного элемента (comboBox1.SelectedValue)


            // заполняем группы
            showGroups();
        }


        /// <summary>
        /// Получаем данные с studs 
        /// </summary>
        private void showGroups()
        {
            if (!studs.GetTabGroups())
            {
                showSqlError(studs.SqlErrorMessage); return;
            }

            // привязываем комбобокс к данным таблицы
            comboBox1.DataSource = studs.TabGroups;
            // по умолчанию устанавливаем первую запись
            comboBox1.SelectedIndex = 0;

        }


        /// <summary>
        /// Метод выводит на экран сообщение об ошибке 
        /// </summary>
        /// <param name="sql_ErrorMessage"></param>
        private void showSqlError(string sql_ErrorMessage)
        {
            MessageBox.Show(sql_ErrorMessage, "Ошибка SQL ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /// <summary>
        /// Событие возникает при изменении занчения номера группы
        /// программно или пользователем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show("selected index  = " + comboBox1.SelectedIndex);
            // MessageBox.Show("selected id_group = " + comboBox1.SelectedValue);

            idGroup = (int)comboBox1.SelectedValue;   /// (int)-конвертировать в тип int

            // Покажем профессора   
            showGroupProf();

            // Покажем список студентов
            showGroupStuds();


        }

        /// <summary>
        /// Отображает в текстбоксе профессора для конкретной группы 
        /// </summary>
        /// <param name="id_gr"></param>
        private void showGroupProf()
        {
            foreach (DataRow row in studs.TabGroups.Rows)
            {
                if (Convert.ToInt32(row["id_group"]) == idGroup)
                    textBox1.Text = row["professor_name"].ToString();
            }

            // или т. к. порядок и количество выпадающих строк в комбобоксе и таблице совпадают  
            // то можно указать значению по индексу
            //textBox1.Text = studs.TabGroups.Rows[comboBox1.SelectedIndex]["professor_name"].ToString();
        }


        /// <summary>
        /// Заполняем таблицу студентами из указанной группы
        /// </summary>
        /// <param name="id_gr"></param>
        private void showGroupStuds()
        {
            if (!studs.GetTabStuds(idGroup))
            {
                showSqlError(studs.SqlErrorMessage); return;
            }

            // привязываем датагрид к данным таблицы
            dataGridView1.DataSource = studs.TabStuds;

            // где какой столбец отображается задается в свойствах таблицы
        }

        /// <summary>
        /// Handles the RowEnter event for the DataGridView control, allowing custom logic to be executed when a row is
        /// entered.
        /// </summary>
        /// <param name="sender">The source of the event, typically the DataGridView control.</param>
        /// <param name="e">A DataGridViewCellEventArgs that contains the event data, including the row and column indexes of the cell
        /// that was entered.</param>
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.RowCount > 0)
            {
                _id_stud = Convert.ToInt32(dataGridView1["Column_id_stud", e.RowIndex].Value);

                //_id_stud = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Column_id_stud"].Value);

                //MessageBox.Show("Вы выбрали студента с id = " + _id_stud);

                // заполняем имя студента на panel 2
                showStudentName( );

                // заполняем среднюю оценку для этого студента


                // заполняем все оценки этого студента в порядке убывания даты
            }


        }

        /// <summary>
        /// Отображает в текстбоксе 2 студента с указанным ID
        /// </summary>
        private void showStudentName( )
        {
            foreach (DataRow row in studs.TabStuds.Rows)
            {
                if (Convert.ToInt32(row["id_stud"]) == _id_stud)
                    textBox2.Text = row["student_name"].ToString();
            }

        }

        /// <summary>
        /// Calculates and displays the average mark for the specified student.
        /// </summary>
        /// <param name="_id_stud">The unique identifier of the student whose average mark is to be displayed.</param>
        private void showAverageMark(int _id_stud)
        {
            // Получаем среднюю оценку для студента с id = _id_stud
            // и отображаем ее в textBox3
            int months = (int)numericUpDown1.Value;

            //return 0
        }

        /// <summary>
        /// Deletes the selected student from the database and updates the display accordingly when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этого студента?", "Студент удалён", MessageBoxButtons.YesNo) 
                == DialogResult.Yes) 
            {
                if (!studs.DeleteStuds(_id_stud))
                {
                    showSqlError(studs.SqlErrorMessage); return;
                }

                showGroupStuds();

            }
        }

        /// <summary>
        /// Add a new student to the database and update the display accordingly when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["AddNewStudentForm"];

            if (form == null)
            {
                Forms.AddStudentForm addnewstudent = new Forms.AddStudentForm(studs);
                addnewstudent.ShowDialog();
            }
            else
            {
                form.Focus();
            }

            showGroupStuds();
        }
    }


}
