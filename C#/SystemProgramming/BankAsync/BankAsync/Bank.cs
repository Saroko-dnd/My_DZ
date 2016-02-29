using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
//for open dialog
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;


namespace BankAsync
{
    public static class Bank
    {
        
        public static object RemoveMoneyGate;
        public static object ChangeMoneyValueGate;
        public static int Money = 15000;
        public static ObservableCollection<string> CollectionsOfActions = new ObservableCollection<string>();

        public static void StartRemoveMoney(int NewSum,string NewUsername)
        {
            Thread CurrentThread = new Thread(() => RemoveMoney(NewSum)); 
            ShowBalance (NewUsername,NewSum);
        }

        public static void ShowBalance (string UserName, int CurrentSum)
        {
            CollectionsOfActions.Add(MyResourses.Texts.Balance + " " + UserName + " " + Money.ToString());
        }

        public static void RemoveMoney(int CurrentSum)
        {
            int CurrentMoney = 0;
            bool result = false;
            while (!result)
            {
                //Здесь уведомляем пользователя о том что на счету нет необходимой суммы
                //и спрашиваем, что делать
                //Делаем это вне лока , так как это может быть долгой операцией
                lock (RemoveMoneyGate)
                {
                    Application.Current.Dispatcher.Invoke(new System.Action(() => CurrentMoney = Money));
                    if (CurrentMoney >= CurrentSum)
                    {
                        Application.Current.Dispatcher.Invoke(new System.Action(() => Money -= CurrentSum));
                        result = true;
                    }
                }
            }
        }
    }
}
