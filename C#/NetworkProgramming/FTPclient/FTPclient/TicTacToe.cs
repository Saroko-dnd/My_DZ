using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FTPclient
{
    public static class TicTacToe
    {
        public static int LastLine = -1;
        public static int LastColumn = -1;
        public static char SelectedSymbiolForGame;
        public static Random RandomForGame = new Random();
        public static List<char> GameSymbols = new List<char>() {'0','X' };
        public static int GameFieldSize = 40;
        public static char[,] GameField = new char[GameFieldSize, GameFieldSize];
        public static bool FirstTime = true;
        public static StringBuilder StringBuilderForTextBox = new StringBuilder();

        public static void DrawGameField(TextBox ControlForGameField)
        {
            if (FirstTime)
            {
                for (int counter = 0; counter < GameFieldSize; ++counter)
                {
                    for (int counter_2 = 0; counter_2 < GameFieldSize; ++counter_2)
                    {
                        GameField[counter, counter_2] = '#';
                        StringBuilderForTextBox.Append('#');
                    }
                    StringBuilderForTextBox.AppendLine();
                }
                ControlForGameField.Text = StringBuilderForTextBox.ToString();
            }
            else
            {

            }
        }

        public static void Play(int Line, int Column)
        {
            if (GameField[Line, Column] == '#')
            {
                GameField[Line, Column] = SelectedSymbiolForGame;
            }
            else
            {
                bool done = false;
                while (!done)
                {
                    for (int counter = 0; counter < GameFieldSize; ++counter)
                    {
                        for (int counter_2 = 0; counter_2 < GameFieldSize; ++counter_2)
                        {
                            if (GameField[counter, counter_2] == '#' && RandomForGame.Next(11) > 5)
                            {
                                GameField[counter, counter_2] = SelectedSymbiolForGame;
                                LastLine = counter;
                                LastColumn = counter_2;
                                done = true;
                                break;
                            }
                        }
                        if (done)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
