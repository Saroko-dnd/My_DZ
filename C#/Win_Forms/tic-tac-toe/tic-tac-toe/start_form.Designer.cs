using System;
using System.Windows.Forms;

namespace tic_tac_toe
{
    partial class start_form
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

        public void MyEventCode(object sender, EventArgs e)
        {
            MessageBox.Show("EVENT FIRED");
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.zero_button = new System.Windows.Forms.Button();
            this.x_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(73, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "крестики-нолики";
            // 
            // zero_button
            // 
            this.zero_button.Location = new System.Drawing.Point(77, 139);
            this.zero_button.Name = "zero_button";
            this.zero_button.Size = new System.Drawing.Size(41, 35);
            this.zero_button.TabIndex = 1;
            this.zero_button.Text = "0";
            this.zero_button.UseVisualStyleBackColor = true;
            this.zero_button.Click += new System.EventHandler(this.zero_button_Click);
            // 
            // x_button
            // 
            this.x_button.Location = new System.Drawing.Point(169, 139);
            this.x_button.Name = "x_button";
            this.x_button.Size = new System.Drawing.Size(41, 35);
            this.x_button.TabIndex = 2;
            this.x_button.Text = "X";
            this.x_button.UseVisualStyleBackColor = true;
            this.x_button.Click += new System.EventHandler(this.x_button_Click);
            // 
            // start_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.x_button);
            this.Controls.Add(this.zero_button);
            this.Controls.Add(this.label1);
            this.Name = "start_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "start_form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button zero_button;
        private System.Windows.Forms.Button x_button;
    }
}