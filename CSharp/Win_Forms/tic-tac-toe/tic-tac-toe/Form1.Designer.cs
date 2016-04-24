namespace tic_tac_toe
{
    partial class Form1
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
            this.Tic_Tac_Toe_table = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Tic_Tac_Toe_table
            // 
            this.Tic_Tac_Toe_table.AutoSize = true;
            this.Tic_Tac_Toe_table.ColumnCount = 4;
            this.Tic_Tac_Toe_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.Location = new System.Drawing.Point(204, 82);
            this.Tic_Tac_Toe_table.Name = "Tic_Tac_Toe_table";
            this.Tic_Tac_Toe_table.RowCount = 4;
            this.Tic_Tac_Toe_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.Tic_Tac_Toe_table.Size = new System.Drawing.Size(191, 178);
            this.Tic_Tac_Toe_table.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(242, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Игровое поле";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 420);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tic_Tac_Toe_table);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Click += new System.EventHandler(ShowResult);
            this.Click += new System.EventHandler(ShowResult);

        }
        public static void ShowResult(object sender, System.EventArgs e)
        {
            //Код для запуска таблицы результатов
        }
        public static void MainForm_Load(object sender, System.EventArgs e)
        {

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel Tic_Tac_Toe_table;
        private System.Windows.Forms.Label label1;

    }
}

