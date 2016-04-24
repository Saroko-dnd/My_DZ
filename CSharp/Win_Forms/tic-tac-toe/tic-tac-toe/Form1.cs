using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class Form1 : Form
    {
        public static Random main_random = new Random();

        public int counter_of_free_fields = 16;
        public start_form new_game_form = new start_form();
        public string player_char;
        public string computer_char;
        public bool danger_for_computer = false;
        public int count_empty_fields = 0;
        public int count_player_fields = 0;
        public int count_computer_fields = 0;

        public void press_button_event_handler(object sender, MouseEventArgs e)
        {
            if (((Button)sender).Text == "")
            {
                --counter_of_free_fields;
                ((Button)sender).Text = player_char;
                if (!check_victory())
                {
                    int column_point = 0;
                    int row_point = 0;
                    bool computer_did_thing = false;
                    int index_count = 0;
                    int buf_for_column_index = 0;
                    int buf_for_column_count = 0;
                    while (!computer_did_thing)
                    {
                        int count_for_column = 0;
                        danger_for_computer = false;
                        bool computer_close_to_win = false;
                        List<string> buf_for_one_line = new List<string>();
                        for (int counter_2 = 0; counter_2 < 4; ++counter_2)
                        {
                            for (int counter_3 = 0; counter_3 < 4; ++counter_3)
                            {
                                buf_for_one_line.Add(this.Tic_Tac_Toe_table.
                                    GetControlFromPosition(counter_2, counter_3).Text);
                            }
                            index_count = 0;
                            foreach (string current_string in buf_for_one_line)
                            {
                                if (current_string == "")
                                {
                                    ++count_empty_fields;
                                    column_point = counter_2;
                                    row_point = index_count;
                                }
                                else if (current_string == player_char)
                                    ++count_player_fields;
                                else
                                    ++count_computer_fields;
                                ++index_count;
                            }
                            buf_for_one_line.Clear();
                            if (count_empty_fields == 1)
                            {
                                if (count_player_fields == 3 || count_computer_fields == 3)
                                {
                                    danger_for_computer = true;
                                    break;
                                }
                            }
                            counters_to_zero();
                        }
                        if (!danger_for_computer)
                        {
                            for (int counter_2 = 0; counter_2 < 4; ++counter_2)
                            {
                                for (int counter_3 = 0; counter_3 < 4; ++counter_3)
                                {
                                    if (this.Tic_Tac_Toe_table.GetControlFromPosition(counter_3, counter_2).Text == "")
                                        buf_for_column_index = counter_3;
                                    buf_for_one_line.Add(this.Tic_Tac_Toe_table.
                                        GetControlFromPosition(counter_3, counter_2).Text);
                                }
                                index_count = 0;
                                foreach (string current_string in buf_for_one_line)
                                {
                                    if (current_string == "")
                                    {
                                        ++count_empty_fields;
                                        column_point = buf_for_column_index;
                                        row_point = counter_2;
                                    }
                                    else if (current_string == player_char)
                                        ++count_player_fields;
                                    else
                                        ++count_computer_fields;
                                    ++index_count;
                                }
                                buf_for_one_line.Clear();
                                if (count_empty_fields == 1)
                                {
                                    if (count_player_fields == 3 || count_computer_fields == 3)
                                    {
                                        danger_for_computer = true;
                                        break;
                                    }
                                }
                                counters_to_zero();
                            }
                        }
                        if (!danger_for_computer)
                        {
                            for (int counter_2 = 0; counter_2 < 2; ++counter_2)
                            {
                                int column_count = 0;
                                if (counter_2 == 0)
                                    for (int counter_3 = 0; counter_3 < 4; ++counter_3)
                                    {
                                        if (this.Tic_Tac_Toe_table.GetControlFromPosition(counter_3, counter_3).Text == "")
                                            buf_for_column_index = counter_3;
                                        buf_for_one_line.Add(this.Tic_Tac_Toe_table.
                                            GetControlFromPosition(counter_3, counter_3).Text);
                                    }
                                else
                                {
                                    for (int counter_3 = 3; counter_3 >= 0; --counter_3)
                                    {
                                        if (this.Tic_Tac_Toe_table.GetControlFromPosition(column_count, counter_3).
                                            Text == "")
                                        {
                                            buf_for_column_count = column_count;
                                            buf_for_column_index = counter_3;
                                        }
                                        buf_for_one_line.Add(this.Tic_Tac_Toe_table.
                                            GetControlFromPosition(column_count, counter_3).Text);
                                        ++column_count;
                                    }
                                }
                                index_count = 0;
                                foreach (string current_string in buf_for_one_line)
                                {
                                    if (current_string == "")
                                    {
                                        ++count_empty_fields;
                                        if (counter_2 == 0)
                                        {
                                            column_point = buf_for_column_index;
                                            row_point = buf_for_column_index;
                                        }
                                        else
                                        {
                                            column_point = buf_for_column_count;
                                            row_point = buf_for_column_index;
                                        }
                                    }
                                    else if (current_string == player_char)
                                        ++count_player_fields;
                                    else
                                        ++count_computer_fields;
                                    ++index_count;
                                }
                                buf_for_one_line.Clear();
                                if (count_empty_fields == 1)
                                {
                                    if (count_player_fields == 3 || count_computer_fields == 3)
                                    {
                                        danger_for_computer = true;
                                        break;
                                    }
                                }
                                counters_to_zero();
                            }
                        }                   
                        if (!danger_for_computer)
                        {
                            for (int counter = 1; counter < 17; ++counter)
                            {
                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).Text !=
                                    "")
                                {

                                    if (!danger_for_computer)
                                    {
                                        if (count_for_column > 0 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column - 1,
                                            counter % 4).Text))
                                        {
                                            if ((count_for_column - 2) >= 0)
                                            {
                                                column_point = count_for_column - 2;
                                                row_point = counter % 4;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (count_for_column < 3 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column + 1,
                                            counter % 4).Text))
                                        {
                                            if ((count_for_column + 2) <= 3)
                                            {
                                                column_point = count_for_column + 2;
                                                row_point = counter % 4;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (counter % 4 > 0 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                            (counter % 4) - 1).Text))
                                        {
                                            if ((counter % 4) - 2 >= 0)
                                            {
                                                column_point = count_for_column;
                                                row_point = (counter % 4) - 2;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (counter % 4 < 3 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                            (counter % 4) + 1).Text))
                                        {
                                            if ((counter % 4) + 2 <= 3)
                                            {
                                                column_point = count_for_column;
                                                row_point = (counter % 4) + 2;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (counter % 4 < 3 && count_for_column < 3 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column + 1,
                                            (counter % 4) + 1).Text))
                                        {
                                            if ((counter % 4) + 2 <= 3 && (count_for_column + 2) <= 3)
                                            {
                                                column_point = count_for_column + 2;
                                                row_point = (counter % 4) + 2;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (counter % 4 > 0 && count_for_column > 0 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column - 1,
                                            (counter % 4) - 1).Text))
                                        {
                                            if ((counter % 4) - 2 >= 0 && (count_for_column - 2) >= 0)
                                            {
                                                column_point = count_for_column - 2;
                                                row_point = (counter % 4) - 2;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (counter % 4 > 0 && count_for_column < 3 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column + 1,
                                            (counter % 4) - 1).Text))
                                        {
                                            if ((counter % 4) - 2 >= 0 && (count_for_column + 2) <= 3)
                                            {
                                                column_point = count_for_column + 2;
                                                row_point = (counter % 4) - 2;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (counter % 4 < 3 && count_for_column > 0 &&
                                            (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column, counter % 4).
                                            Text == this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column - 1,
                                            (counter % 4) + 1).Text))
                                        {
                                            if ((counter % 4) + 2 <= 3 && (count_for_column - 2) >= 0)
                                            {
                                                column_point = count_for_column - 2;
                                                row_point = (counter % 4) + 2;
                                                if (this.Tic_Tac_Toe_table.GetControlFromPosition(count_for_column,
                                                    counter % 4).Text == player_char)
                                                {
                                                    danger_for_computer = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    computer_close_to_win = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (counter % 4 == 0)
                                {
                                    ++count_for_column;
                                }
                            }
                        }
                        if (danger_for_computer)
                        {
                            if (this.Tic_Tac_Toe_table.GetControlFromPosition(column_point, row_point).Text ==
                                "")
                            {
                                this.Tic_Tac_Toe_table.GetControlFromPosition(column_point, row_point).Text =
                                    computer_char;
                                computer_did_thing = true;
                                --counter_of_free_fields;
                                break;
                            }
                        }
                        else if (computer_close_to_win)
                        {
                            if (this.Tic_Tac_Toe_table.GetControlFromPosition(column_point, row_point).Text ==
                                "")
                            {
                                this.Tic_Tac_Toe_table.GetControlFromPosition(column_point, row_point).Text =
                                    computer_char;
                                computer_did_thing = true;
                                --counter_of_free_fields;
                                break;
                            }
                        }
                        int buf_for_index = main_random.Next(16);
                        if (!computer_did_thing && this.Tic_Tac_Toe_table.Controls[buf_for_index].Text == "")
                        {
                            this.Tic_Tac_Toe_table.Controls[buf_for_index].Text = computer_char;
                            computer_did_thing = true;
                            --counter_of_free_fields;
                        }
                    }
                    check_victory();
                }
            }         
        }

        public void random_start_for_computer_player()
        {
            this.Tic_Tac_Toe_table.GetControlFromPosition(main_random.Next(4), main_random.Next(4)).Text =
                computer_char;
            --counter_of_free_fields;
        }

        public void counters_to_zero()
        {
            count_empty_fields = 0;
            count_player_fields = 0;
            count_computer_fields = 0;
        }

        public bool check_victory()
        {
            int result_of_checking_line = 0;
            List<string> buf_for_results = new List<string>();
            for (int counter = 0; counter < 4; ++counter)
            {
                for (int counter_2 = 0; counter_2 < 4; ++counter_2)
                {
                    buf_for_results.Add(this.Tic_Tac_Toe_table.GetControlFromPosition(counter, counter_2).Text);
                }
                result_of_checking_line = check_line(buf_for_results);
                if (result_of_checking_line > 0)
                    break;
                buf_for_results.Clear();
            }
            if (result_of_checking_line == 0)
            {
                for (int counter = 0; counter < 4; ++counter)
                {
                    for (int counter_2 = 0; counter_2 < 4; ++counter_2)
                    {
                        buf_for_results.Add(this.Tic_Tac_Toe_table.GetControlFromPosition(counter_2, counter).Text);
                    }
                    result_of_checking_line = check_line(buf_for_results);
                    if (result_of_checking_line > 0)
                        break;
                    buf_for_results.Clear();
                }
            }
            if (result_of_checking_line == 0)
            {
                for (int counter = 0; counter < 2; ++counter)
                {
                    if (counter == 0)
                        for (int counter_2 = 0; counter_2 < 4; ++counter_2)
                        {
                            buf_for_results.Add(this.Tic_Tac_Toe_table.GetControlFromPosition(counter_2, counter_2).
                                Text);
                        }
                    else
                    {
                        int columns_counter = 0;
                        for (int counter_2 = 3; counter_2 >= 0; --counter_2)
                        {
                            buf_for_results.Add(this.Tic_Tac_Toe_table.GetControlFromPosition(columns_counter, 
                                counter_2).Text);
                            ++columns_counter;
                        }
                    }
                    result_of_checking_line = check_line(buf_for_results);
                    if (result_of_checking_line > 0)
                        break;
                    buf_for_results.Clear();
                }
            }
            if (result_of_checking_line > 0)
            {
                if (result_of_checking_line == 1)
                    MessageBox.Show("Поздравляем с победой!");
                else
                    MessageBox.Show("Вы проиграли!");
                foreach (Control current_control in this.Tic_Tac_Toe_table.Controls)
                {
                    current_control.Text = "";
                }
                counter_of_free_fields = 16;
                new_game_form.ShowDialog();
                player_char = new_game_form.player_char;
                computer_char = new_game_form.computer_char;
                if (player_char == "0")
                    random_start_for_computer_player();
                return true;
            }
            if (counter_of_free_fields == 0)
            {
                counter_of_free_fields = 16;
                MessageBox.Show("Ничья!");
                foreach (Control current_control in this.Tic_Tac_Toe_table.Controls)
                {
                    current_control.Text = "";
                }
                new_game_form.ShowDialog();
                player_char = new_game_form.player_char;
                computer_char = new_game_form.computer_char;
                if (player_char == "0")
                    random_start_for_computer_player();
                return true;
            }
            return false;
        }
           
        int check_line (List<string> buf_for_results)
        {
            bool player_wins = false;
            bool computer_wins = false;
            player_wins = true;
            foreach (string current_string in buf_for_results)
            {
                if (current_string != player_char)
                {
                    player_wins = false;
                }
            }
            if (player_wins)
                return 1;
            computer_wins = true;
            foreach (string current_string in buf_for_results)
            {
                if (current_string != computer_char)
                {
                    computer_wins = false;
                }
            }
            if (computer_wins)
                return 2;
            return 0;
        }
                   
        public Form1()
        {
            InitializeComponent();
            int size_of_table = this.Tic_Tac_Toe_table.Size.Height;
            int count_for_column = 0;
            for (int counter = 1; counter < 17; ++counter )
            {
                Button buf_for_button = new Button();
                buf_for_button.Text = "";
                buf_for_button.Height = size_of_table / 4;
                buf_for_button.Width = size_of_table / 4;
                this.Tic_Tac_Toe_table.Controls.Add(buf_for_button, count_for_column, counter%4);
                if (counter%4 == 0)
                {
                    ++count_for_column;
                }
            }
            foreach (Control current_control in this.Tic_Tac_Toe_table.Controls)
            {
                current_control.MouseClick += press_button_event_handler;
            }
            new_game_form.ShowDialog();
            player_char = new_game_form.player_char;
            computer_char = new_game_form.computer_char;
            if (player_char == "0")
                random_start_for_computer_player();
        }
    }
}
