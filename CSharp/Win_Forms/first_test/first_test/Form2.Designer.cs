namespace first_test
{
    partial class журнал
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(журнал));
            this.label1 = new System.Windows.Forms.Label();
            this.list_of_groups = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.change_password_button = new System.Windows.Forms.Button();
            this.start_lesson_button = new System.Windows.Forms.Button();
            this.subjects_list = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.themes_list = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.time_list = new System.Windows.Forms.ComboBox();
            this.show_lessons_button = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lesson_data_table = new System.Windows.Forms.TableLayoutPanel();
            this.list_of_lessons = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lessons_of_current_teacher_checkBox = new System.Windows.Forms.CheckBox();
            this.change_user_button = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.name_of_group_for_table = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // list_of_groups
            // 
            this.list_of_groups.FormattingEnabled = true;
            resources.ApplyResources(this.list_of_groups, "list_of_groups");
            this.list_of_groups.Name = "list_of_groups";
            this.list_of_groups.SelectedIndexChanged += new System.EventHandler(this.list_of_groups_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // change_password_button
            // 
            resources.ApplyResources(this.change_password_button, "change_password_button");
            this.change_password_button.Name = "change_password_button";
            this.change_password_button.UseVisualStyleBackColor = true;
            this.change_password_button.Click += new System.EventHandler(this.change_password_button_Click);
            // 
            // start_lesson_button
            // 
            resources.ApplyResources(this.start_lesson_button, "start_lesson_button");
            this.start_lesson_button.Name = "start_lesson_button";
            this.start_lesson_button.UseVisualStyleBackColor = true;
            this.start_lesson_button.Click += new System.EventHandler(this.start_lesson_button_Click);
            // 
            // subjects_list
            // 
            this.subjects_list.FormattingEnabled = true;
            resources.ApplyResources(this.subjects_list, "subjects_list");
            this.subjects_list.Name = "subjects_list";
            this.subjects_list.SelectedIndexChanged += new System.EventHandler(this.subjects_list_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // themes_list
            // 
            this.themes_list.FormattingEnabled = true;
            resources.ApplyResources(this.themes_list, "themes_list");
            this.themes_list.Name = "themes_list";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // time_list
            // 
            this.time_list.FormattingEnabled = true;
            this.time_list.Items.AddRange(new object[] {
            resources.GetString("time_list.Items"),
            resources.GetString("time_list.Items1"),
            resources.GetString("time_list.Items2"),
            resources.GetString("time_list.Items3"),
            resources.GetString("time_list.Items4")});
            resources.ApplyResources(this.time_list, "time_list");
            this.time_list.Name = "time_list";
            // 
            // show_lessons_button
            // 
            resources.ApplyResources(this.show_lessons_button, "show_lessons_button");
            this.show_lessons_button.Name = "show_lessons_button";
            this.show_lessons_button.UseVisualStyleBackColor = true;
            this.show_lessons_button.Click += new System.EventHandler(this.show_lessons_button_Click);
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Name = "dateTimePicker1";
            // 
            // lesson_data_table
            // 
            resources.ApplyResources(this.lesson_data_table, "lesson_data_table");
            this.lesson_data_table.Name = "lesson_data_table";
            // 
            // list_of_lessons
            // 
            this.list_of_lessons.FormattingEnabled = true;
            resources.ApplyResources(this.list_of_lessons, "list_of_lessons");
            this.list_of_lessons.Name = "list_of_lessons";
            this.list_of_lessons.SelectedIndexChanged += new System.EventHandler(this.list_of_lessons_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lessons_of_current_teacher_checkBox
            // 
            resources.ApplyResources(this.lessons_of_current_teacher_checkBox, "lessons_of_current_teacher_checkBox");
            this.lessons_of_current_teacher_checkBox.Name = "lessons_of_current_teacher_checkBox";
            this.lessons_of_current_teacher_checkBox.UseVisualStyleBackColor = true;
            // 
            // change_user_button
            // 
            resources.ApplyResources(this.change_user_button, "change_user_button");
            this.change_user_button.Name = "change_user_button";
            this.change_user_button.UseVisualStyleBackColor = true;
            this.change_user_button.Click += new System.EventHandler(this.change_user_button_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // name_of_group_for_table
            // 
            resources.ApplyResources(this.name_of_group_for_table, "name_of_group_for_table");
            this.name_of_group_for_table.Name = "name_of_group_for_table";
            // 
            // журнал
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.name_of_group_for_table);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.change_user_button);
            this.Controls.Add(this.lessons_of_current_teacher_checkBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.list_of_lessons);
            this.Controls.Add(this.lesson_data_table);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.show_lessons_button);
            this.Controls.Add(this.time_list);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.themes_list);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.subjects_list);
            this.Controls.Add(this.start_lesson_button);
            this.Controls.Add(this.change_password_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.list_of_groups);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "журнал";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.журнал_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox list_of_groups;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button change_password_button;
        private System.Windows.Forms.Button start_lesson_button;
        private System.Windows.Forms.ComboBox subjects_list;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox themes_list;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox time_list;
        private System.Windows.Forms.Button show_lessons_button;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TableLayoutPanel lesson_data_table;
        private System.Windows.Forms.ListBox list_of_lessons;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox lessons_of_current_teacher_checkBox;
        private System.Windows.Forms.Button change_user_button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label name_of_group_for_table;
    }
}