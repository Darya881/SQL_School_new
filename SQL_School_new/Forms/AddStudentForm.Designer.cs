namespace SQL_School_new.Forms
{
    partial class AddStudentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NewStudentName = new System.Windows.Forms.TextBox();
            this.NewStudentBirthdate = new System.Windows.Forms.DateTimePicker();
            this.NewStudentGroup = new System.Windows.Forms.NumericUpDown();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_accept = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewStudentGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(221)))), ((int)(((byte)(252)))));
            this.panel1.Controls.Add(this.NewStudentName);
            this.panel1.Controls.Add(this.NewStudentBirthdate);
            this.panel1.Controls.Add(this.NewStudentGroup);
            this.panel1.Controls.Add(this.button_cancel);
            this.panel1.Controls.Add(this.button_accept);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 365);
            this.panel1.TabIndex = 0;
            // 
            // NewStudentName
            // 
            this.NewStudentName.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewStudentName.Location = new System.Drawing.Point(231, 56);
            this.NewStudentName.MaxLength = 30;
            this.NewStudentName.Name = "NewStudentName";
            this.NewStudentName.Size = new System.Drawing.Size(231, 32);
            this.NewStudentName.TabIndex = 7;
            // 
            // NewStudentBirthdate
            // 
            this.NewStudentBirthdate.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewStudentBirthdate.Location = new System.Drawing.Point(231, 120);
            this.NewStudentBirthdate.MaxDate = new System.DateTime(2020, 12, 25, 0, 0, 0, 0);
            this.NewStudentBirthdate.MinDate = new System.DateTime(1965, 4, 6, 0, 0, 0, 0);
            this.NewStudentBirthdate.Name = "NewStudentBirthdate";
            this.NewStudentBirthdate.Size = new System.Drawing.Size(231, 32);
            this.NewStudentBirthdate.TabIndex = 6;
            this.toolTip1.SetToolTip(this.NewStudentBirthdate, "Минимальный возраст 16 лет, максимальный - 60 лет");
            this.NewStudentBirthdate.Value = new System.DateTime(2002, 11, 15, 0, 0, 0, 0);
            // 
            // NewStudentGroup
            // 
            this.NewStudentGroup.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewStudentGroup.Location = new System.Drawing.Point(231, 189);
            this.NewStudentGroup.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NewStudentGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NewStudentGroup.Name = "NewStudentGroup";
            this.NewStudentGroup.Size = new System.Drawing.Size(120, 32);
            this.NewStudentGroup.TabIndex = 5;
            this.NewStudentGroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(188)))), ((int)(((byte)(179)))));
            this.button_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.FlatAppearance.BorderSize = 0;
            this.button_cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(61)))), ((int)(((byte)(33)))));
            this.button_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(126)))), ((int)(((byte)(102)))));
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_cancel.Location = new System.Drawing.Point(290, 278);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(172, 45);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_accept
            // 
            this.button_accept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(227)))), ((int)(((byte)(173)))));
            this.button_accept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_accept.FlatAppearance.BorderSize = 0;
            this.button_accept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(87)))), ((int)(((byte)(46)))));
            this.button_accept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(207)))), ((int)(((byte)(113)))));
            this.button_accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_accept.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_accept.Location = new System.Drawing.Point(49, 278);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(171, 45);
            this.button_accept.TabIndex = 3;
            this.button_accept.Text = "Принять";
            this.button_accept.UseVisualStyleBackColor = false;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(45, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Группа";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(45, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата Рождения";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(45, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия, Имя";
            // 
            // AddStudentForm
            // 
            this.AcceptButton = this.button_accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(521, 365);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddStudentForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление нового студента";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewStudentGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.NumericUpDown NewStudentGroup;
        private System.Windows.Forms.TextBox NewStudentName;
        private System.Windows.Forms.DateTimePicker NewStudentBirthdate;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}