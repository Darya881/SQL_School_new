namespace SQL_School_new
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.вариантыРаботыСSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataAdapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oRMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.окнаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вариантыРаботыСSQLToolStripMenuItem,
            this.выходToolStripMenuItem,
            this.окнаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.окнаToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(900, 31);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // вариантыРаботыСSQLToolStripMenuItem
            // 
            this.вариантыРаботыСSQLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataAdapterToolStripMenuItem,
            this.dataSetToolStripMenuItem,
            this.oRMToolStripMenuItem});
            this.вариантыРаботыСSQLToolStripMenuItem.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.вариантыРаботыСSQLToolStripMenuItem.Name = "вариантыРаботыСSQLToolStripMenuItem";
            this.вариантыРаботыСSQLToolStripMenuItem.Size = new System.Drawing.Size(257, 27);
            this.вариантыРаботыСSQLToolStripMenuItem.Text = "Варианты работы с SQL";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(85, 27);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // dataAdapterToolStripMenuItem
            // 
            this.dataAdapterToolStripMenuItem.Name = "dataAdapterToolStripMenuItem";
            this.dataAdapterToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.dataAdapterToolStripMenuItem.Text = "DataAdapter";
            this.dataAdapterToolStripMenuItem.Click += new System.EventHandler(this.dataAdapterToolStripMenuItem_Click);
            // 
            // dataSetToolStripMenuItem
            // 
            this.dataSetToolStripMenuItem.Name = "dataSetToolStripMenuItem";
            this.dataSetToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.dataSetToolStripMenuItem.Text = "DataSet";
            // 
            // oRMToolStripMenuItem
            // 
            this.oRMToolStripMenuItem.Name = "oRMToolStripMenuItem";
            this.oRMToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.oRMToolStripMenuItem.Text = "ORM Dapper";
            // 
            // окнаToolStripMenuItem
            // 
            this.окнаToolStripMenuItem.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.окнаToolStripMenuItem.Name = "окнаToolStripMenuItem";
            this.окнаToolStripMenuItem.Size = new System.Drawing.Size(76, 27);
            this.окнаToolStripMenuItem.Text = "Окна";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 506);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "School";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem вариантыРаботыСSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataAdapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oRMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem окнаToolStripMenuItem;
    }
}