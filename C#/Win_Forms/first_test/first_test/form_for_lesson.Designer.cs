namespace first_test
{
    partial class form_for_lesson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_for_lesson));
            this.label3 = new System.Windows.Forms.Label();
            this.current_name_of_group = new System.Windows.Forms.Label();
            this.date_label = new System.Windows.Forms.Label();
            this.journal_table = new System.Windows.Forms.TableLayoutPanel();
            this.save_lesson_button = new System.Windows.Forms.Button();
            this.subject_label = new System.Windows.Forms.Label();
            this.theme_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // current_name_of_group
            // 
            resources.ApplyResources(this.current_name_of_group, "current_name_of_group");
            this.current_name_of_group.Name = "current_name_of_group";
            // 
            // date_label
            // 
            resources.ApplyResources(this.date_label, "date_label");
            this.date_label.Name = "date_label";
            // 
            // journal_table
            // 
            resources.ApplyResources(this.journal_table, "journal_table");
            this.journal_table.Name = "journal_table";
            // 
            // save_lesson_button
            // 
            resources.ApplyResources(this.save_lesson_button, "save_lesson_button");
            this.save_lesson_button.Name = "save_lesson_button";
            this.save_lesson_button.UseVisualStyleBackColor = true;
            this.save_lesson_button.Click += new System.EventHandler(this.save_lesson_button_Click);
            // 
            // subject_label
            // 
            resources.ApplyResources(this.subject_label, "subject_label");
            this.subject_label.Name = "subject_label";
            // 
            // theme_label
            // 
            resources.ApplyResources(this.theme_label, "theme_label");
            this.theme_label.Name = "theme_label";
            // 
            // form_for_lesson
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.theme_label);
            this.Controls.Add(this.subject_label);
            this.Controls.Add(this.save_lesson_button);
            this.Controls.Add(this.journal_table);
            this.Controls.Add(this.date_label);
            this.Controls.Add(this.current_name_of_group);
            this.Controls.Add(this.label3);
            this.Name = "form_for_lesson";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label current_name_of_group;
        private System.Windows.Forms.Label date_label;
        private System.Windows.Forms.TableLayoutPanel journal_table;
        private System.Windows.Forms.Button save_lesson_button;
        private System.Windows.Forms.Label subject_label;
        private System.Windows.Forms.Label theme_label;
    }
}