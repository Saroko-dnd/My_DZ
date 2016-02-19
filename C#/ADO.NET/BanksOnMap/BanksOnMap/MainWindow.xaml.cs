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
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System.Data.Entity.Spatial;
using System.Collections.ObjectModel;
//namespace of my dll
using MapDBContext;


namespace BanksOnMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<RadioButton> RatesSearchTypeRadioButtons = new List<RadioButton>();
        public List<RadioButton> MoneyTypeRadioButtons = new List<RadioButton>();

        //************************************************************
        //валюта для запроса связанного с курсом
        public string Currency = string.Empty;
        //лучший или худший курс
        public bool Best = true;
        //покупка или продажа
        public bool Buy = true;
        //************************************************************

        public MainWindow()
        {
            InitializeComponent();

            RatesSearchTypeRadioButtons.Add(MinBuyValueRadioButton);
            RatesSearchTypeRadioButtons.Add(MinSellValueRadioButton);
            RatesSearchTypeRadioButtons.Add(MaxBuyValueRadioButton);
            RatesSearchTypeRadioButtons.Add(MaxSellValueRadioButton);

            MoneyTypeRadioButtons.Add(EURORadioButton);
            MoneyTypeRadioButtons.Add(RUBRadioButton);
            MoneyTypeRadioButtons.Add(USDRadioButton);

            try
            {
                /*BanksDBContext TestContext = new BanksDBContext(MyResourses.Texts.ConnectionStringName);
                TestContext.Banks.Add(new Bank() { BankName = "TestName_23" });
                TestContext.SaveChanges();*/
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message);
            }
            EntityConnector.ConnectionStringName = MyResourses.Texts.ConnectionStringName;
            //конфигурируем карту
            //*****************************************************************************
            //так добавляем маркеры (видимость маркера определяется видимостью его формы "shape")
            BanksDBContext BanksDatabase = new BanksDBContext(MyResourses.Texts.ConnectionStringName);
            foreach (BankBranch CurrentBranch in BanksDatabase.BankBranches)
            {
                Label LabelForMarker = new Label() { FontSize = 12.0, Content = CurrentBranch.BranchName, Foreground = Brushes.Yellow, Background = Brushes.Black };
                LabelForMarker.MouseLeftButtonDown += ClickMarkerEvent;
                MainMap.Markers.Add(new GMapMarker(new GMap.NET.PointLatLng((double)CurrentBranch.MapLocation.Longitude, (double)CurrentBranch.MapLocation.Latitude ))
                {
                    Tag = "tagddd",
                    Shape = LabelForMarker
                });
            }
            MainMap.Zoom = 16;
            MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            MainMap.MapProvider = GMap.NET.MapProviders.GMapProviders.OpenStreetMap;
            MainMap.CanDragMap = true;
            MainMap.DragButton = MouseButton.Left;
            MainMap.Position = new GMap.NET.PointLatLng(53.902800, 27.561759);
            //разрешаем зум карты когда курсор на маркере
            MainMap.IgnoreMarkerOnMouseWheel = true;
            MapPointTestLabel.Text = "x " + MainMap.MapPoint.X.ToString() + "Y " +  MainMap.MapPoint.Y.ToString();
            MainMap.MouseMove += ShowCoordinatesEvent;
            MainMap.MouseWheel += ShowCoordinatesEvent;
        }

        public void ClickMarkerEvent(Object Sender, EventArgs CurrentArgs)
        {
            foreach (GMapMarker CurrentMarker in MainMap.Markers)
            {
                (CurrentMarker.Shape as Label).Background = Brushes.Black;
            }
            (Sender as Label).Background = Brushes.Blue;

            BankBranch SelectedBranch = EntityConnector.LoadSelectedObjectData((Sender as Label).Content.ToString());

            ShowBranchInfo(SelectedBranch);
        }

        public void ShowCoordinatesEvent (Object Sender,EventArgs CurrentArgs)
        {
            MapPointTestLabel.Text = MainMap.Position.ToString();
        }

        public void SelectRatesSearchType (Object Sender, EventArgs CurrentArgs)
        {
            foreach (RadioButton CurrentRadioButton in RatesSearchTypeRadioButtons)
            {
                CurrentRadioButton.IsChecked = false;
            }
        }

        public void SelectMoneyTypeForSearch(Object Sender, EventArgs CurrentArgs)
        {
            foreach (RadioButton CurrentRadioButton in MoneyTypeRadioButtons)
            {
                CurrentRadioButton.IsChecked = false;
            }
        }

        public void ShowBranchInfo(BankBranch SelectedBranch)
        {
            RelatedBankNameTextBox.Text = SelectedBranch.RelatedBank.BankName;
            BranchNameTextBox.Text = SelectedBranch.BranchName;
            AddressTextBox.Text = SelectedBranch.Address;
            PhoneTextBox.Text = SelectedBranch.Phone;
            FirstNameSelectedTextBox.Text = SelectedBranch.RelatedCashier.FirstName;
            LastNameSelectedTextBox.Text = SelectedBranch.RelatedCashier.LastName;
            PatronymicSelectedTextBox.Text = SelectedBranch.RelatedCashier.Patronymic;
            CashierPhoneTextBox.Text = SelectedBranch.RelatedCashier.Phone;
            USDBuyTextBox.Text = SelectedBranch.RelatedRates.USDBuy.ToString();
            EUROBuyTextBox.Text = SelectedBranch.RelatedRates.EUROBuy.ToString();
            RUBBuyTextBox.Text = SelectedBranch.RelatedRates.RuBuy.ToString();
            USDSellTextBox.Text = SelectedBranch.RelatedRates.USDSell.ToString();
            EUROSellTextBox.Text = SelectedBranch.RelatedRates.EUROSell.ToString();
            RUBSellTextBox.Text = SelectedBranch.RelatedRates.RuSell.ToString();
            ServicesDataGrid.ItemsSource = SelectedBranch.RelatedServices.
                Select(res => new { res.Servise }).ToList();
            WorkHourBeginTextBox.Text = SelectedBranch.WorkingHours.StartHour.ToString();
            WorkMinutesBeginTextBox.Text = SelectedBranch.WorkingHours.StartMinutes.ToString();
            WorkHourEndTextBox.Text = SelectedBranch.WorkingHours.EndHour.ToString();
            WorkMinutesEndTextBox.Text = SelectedBranch.WorkingHours.EndMinutes.ToString();
            BreakTimesDataGrid.ItemsSource = SelectedBranch.BreakTimes.Select(res => new {
                res.StartHour,
                res.StartMinutes,
                res.EndHour,
                res.EndMinutes
            });
            CommentsDataGrid.ItemsSource = SelectedBranch.RelatedComments.
                Select(res => new { res.CommentItself });
            LongitudeTextBox.Text = SelectedBranch.MapLocation.Longitude.ToString();
            LatitudeTextBox.Text = SelectedBranch.MapLocation.Latitude.ToString();

            ListBoxOfServices.ItemsSource = EntityConnector.GetListOfServices();
        }

        public void ChangeMapCenter(double Latitude, double Longitude)
        {
            MainMap.Position = new GMap.NET.PointLatLng(Longitude, Latitude);
        }

        public void StartSearchRatesButtonClick(Object Sender, EventArgs CurrentArgs)
        {
            try
            {
                BankBranch TempBranch = new BankBranch();
                if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinBuyValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO,true,true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxBuyValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO, false, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxSellValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO, true, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinSellValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO, false, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinBuyValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, true, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxBuyValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, false, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxSellValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, true, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinSellValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, false, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinBuyValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, true, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxBuyValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, false, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxSellValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, true, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinSellValueRadioButton)
                {
                    TempBranch = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, false, false);
                }
                ShowBranchInfo(TempBranch);
                ChangeMapCenter((double)TempBranch.MapLocation.Latitude, (double)TempBranch.MapLocation.Longitude);
                SelectedTabItem.IsSelected = true;
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(MyResourses.Texts.CheckRadioButtonsError, MyResourses.Texts.Error, 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
