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
using System.Data.Entity.Validation;
using Google.Apis.Services;
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

        public BankBranch SelectedBranchBuf = new BankBranch();
        public bool AddNewBranch= true;
        public bool UseFilters = false;
        List<BreakTime> ItemsSourseBreakTimes = new List<BreakTime>();
        List<Service> ItemsSourseServices = new List<Service>();
        List<Comment> ItemsSourceComments = new List<Comment>();


        public MainWindow()
        {
            InitializeComponent();

            EntityConnector.StaticBanksDBContext = new BanksDBContext(MyResourses.Texts.ConnectionStringName);

            RatesSearchTypeRadioButtons.Add(MinBuyValueRadioButton);
            RatesSearchTypeRadioButtons.Add(MinSellValueRadioButton);
            RatesSearchTypeRadioButtons.Add(MaxBuyValueRadioButton);
            RatesSearchTypeRadioButtons.Add(MaxSellValueRadioButton);

            MoneyTypeRadioButtons.Add(EURORadioButton);
            MoneyTypeRadioButtons.Add(RUBRadioButton);
            MoneyTypeRadioButtons.Add(USDRadioButton);

            BreakTimeBeginHourTextBox.PreviewTextInput += CharsKiller.InputValidation;
            BreakTimeBeginHourTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            BreakTimeBeginMinuteTextBox.PreviewTextInput += CharsKiller.InputValidation;
            BreakTimeBeginMinuteTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            BreakTimeEndHourTextBox.PreviewTextInput += CharsKiller.InputValidation;
            BreakTimeEndHourTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            BreakTimeEndMinuteTextBox.PreviewTextInput += CharsKiller.InputValidation;
            BreakTimeEndMinuteTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;

            WorkHourBeginTextBox.PreviewTextInput += CharsKiller.InputValidation;
            WorkHourBeginTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            WorkMinutesBeginTextBox.PreviewTextInput += CharsKiller.InputValidation;
            WorkMinutesBeginTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            WorkHourEndTextBox.PreviewTextInput += CharsKiller.InputValidation;
            WorkHourEndTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            WorkMinutesEndTextBox.PreviewTextInput += CharsKiller.InputValidation;
            WorkMinutesEndTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;

            USDBuyTextBox.PreviewTextInput += CharsKiller.InputValidationDoubleOnly;
            USDBuyTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            EUROBuyTextBox.PreviewTextInput += CharsKiller.InputValidationDoubleOnly;
            EUROBuyTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            RUBBuyTextBox.PreviewTextInput += CharsKiller.InputValidationDoubleOnly;
            RUBBuyTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            USDSellTextBox.PreviewTextInput += CharsKiller.InputValidationDoubleOnly;
            USDSellTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            EUROSellTextBox.PreviewTextInput += CharsKiller.InputValidationDoubleOnly;
            EUROSellTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            RUBSellTextBox.PreviewTextInput += CharsKiller.InputValidationDoubleOnly;
            RUBSellTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;

            YearTextBox.PreviewTextInput += CharsKiller.InputValidation;
            YearTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            MonthTextBox.PreviewTextInput += CharsKiller.InputValidation;
            MonthTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;
            DayTextBox.PreviewTextInput += CharsKiller.InputValidation;
            DayTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;

            EntityConnector.ConnectionStringName = MyResourses.Texts.ConnectionStringName;
            //конфигурируем карту
            //*****************************************************************************
            //так добавляем маркеры (видимость маркера определяется видимостью его формы "shape")
            LoadObjectsOnMap();
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
            MessageBox.Show(MyResourses.Texts.IMPORTANT, MyResourses.Texts.ImportantWarning,MessageBoxButton.OK,MessageBoxImage.Warning);
        }

        public void LoadObjectsOnMap()
        {
            MainMap.Markers.Clear();
            foreach (BankBranch CurrentBranch in EntityConnector.StaticBanksDBContext.BankBranches)
            {
                bool allowed = true;
                if (UseFilters && OpeningDateFilterCheckBox.IsChecked.Value && CurrentBranch.OpeningDate == null)
                {
                    allowed = false;
                }
                else if (UseFilters && BankNameFilterCheckBox.IsChecked.Value && CurrentBranch.RelatedBank.BankName != BankNameForFilterTextBox.Text)
                {
                    allowed = false;
                }
                if (allowed)
                {
                    Label LabelForMarker = new Label() { FontSize = 12.0, Content = CurrentBranch.BranchName, Foreground = Brushes.Yellow, Background = Brushes.Black };
                    LabelForMarker.MouseLeftButtonDown += ClickMarkerEvent;
                    MainMap.Markers.Add(new GMapMarker(new GMap.NET.PointLatLng((double)CurrentBranch.MapLocation.Latitude, (double)CurrentBranch.MapLocation.Longitude))
                    {
                        Shape = LabelForMarker
                    });
                }
            }
            UseFilters = false;
        }

        public void ClickMarkerEvent(Object Sender, EventArgs CurrentArgs)
        {
            foreach (GMapMarker CurrentMarker in MainMap.Markers)
            {
                if (CurrentMarker != null)
                    (CurrentMarker.Shape as Label).Background = Brushes.Black;
            }
            if ((Sender as Label) != null)
            {
                (Sender as Label).Background = Brushes.Blue;

                SelectedBranchBuf = EntityConnector.LoadSelectedObjectData((Sender as Label).Content.ToString());

                ShowBranchInfo(SelectedBranchBuf);
            }
        }

        public void ChangeColorOfSelectedBranch(string BranchName)
        {
            foreach (GMapMarker CurrentMarker in MainMap.Markers)
            {
                if ((CurrentMarker.Shape as Label).Content.ToString() != BranchName)
                    (CurrentMarker.Shape as Label).Background = Brushes.Black;
                else
                    (CurrentMarker.Shape as Label).Background = Brushes.Blue;
            }
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
            ClearDataFromFields();
            if (SelectedBranch.RelatedBank != null)
                RelatedBankNameTextBox.Text = SelectedBranch.RelatedBank.BankName;
            if (SelectedBranch.BranchName != null)
                BranchNameTextBox.Text = SelectedBranch.BranchName;
            if (SelectedBranch.Address != null)
                AddressTextBox.Text = SelectedBranch.Address;
            if (SelectedBranch.Phone != null)
                PhoneTextBox.Text = SelectedBranch.Phone;
            if (SelectedBranch.RelatedCashier != null)
            {
                FirstNameSelectedTextBox.Text = SelectedBranch.RelatedCashier.FirstName;
                LastNameSelectedTextBox.Text = SelectedBranch.RelatedCashier.LastName;
                PatronymicSelectedTextBox.Text = SelectedBranch.RelatedCashier.Patronymic;
                CashierPhoneTextBox.Text = SelectedBranch.RelatedCashier.Phone;
            }
            if (SelectedBranch.RelatedRates != null)
            {
                USDBuyTextBox.Text = SelectedBranch.RelatedRates.USDBuy.ToString();
                EUROBuyTextBox.Text = SelectedBranch.RelatedRates.EUROBuy.ToString();
                RUBBuyTextBox.Text = SelectedBranch.RelatedRates.RuBuy.ToString();
                USDSellTextBox.Text = SelectedBranch.RelatedRates.USDSell.ToString();
                EUROSellTextBox.Text = SelectedBranch.RelatedRates.EUROSell.ToString();
                RUBSellTextBox.Text = SelectedBranch.RelatedRates.RuSell.ToString();
            }
            if (SelectedBranch.RelatedServices != null)
            {
                ItemsSourseServices = SelectedBranch.RelatedServices.ToList();
                ServicesDataGrid.ItemsSource = ItemsSourseServices;
            }
            if (SelectedBranch.WorkingHours != null)
            {
                WorkHourBeginTextBox.Text = SelectedBranch.WorkingHours.StartHour.ToString();
                WorkMinutesBeginTextBox.Text = SelectedBranch.WorkingHours.StartMinutes.ToString();
                WorkHourEndTextBox.Text = SelectedBranch.WorkingHours.EndHour.ToString();
                WorkMinutesEndTextBox.Text = SelectedBranch.WorkingHours.EndMinutes.ToString();
            }
            if (SelectedBranch.BreakTimes != null)
            {
                ItemsSourseBreakTimes = SelectedBranch.BreakTimes.ToList();
                BreakTimesDataGrid.ItemsSource = ItemsSourseBreakTimes;
            }
            if (SelectedBranch.RelatedComments != null)
            {
                ItemsSourceComments = SelectedBranch.RelatedComments.ToList();
                CommentsDataGrid.ItemsSource = ItemsSourceComments;
            }
            if (SelectedBranch.MapLocation != null)
            {
                LongitudeTextBox.Text = SelectedBranch.MapLocation.Longitude.ToString();
                LatitudeTextBox.Text = SelectedBranch.MapLocation.Latitude.ToString();
            }
            if (SelectedBranch.OpeningDate != null)
            {
                YearTextBox.Text = SelectedBranch.OpeningDate.Value.Year.ToString();
                MonthTextBox.Text = SelectedBranch.OpeningDate.Value.Month.ToString();
                DayTextBox.Text = SelectedBranch.OpeningDate.Value.Day.ToString();
            }
        }

        public void ClearDataFromFields()
        {
            RelatedBankNameTextBox.Text = string.Empty;
            BranchNameTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            FirstNameSelectedTextBox.Text = string.Empty;
            LastNameSelectedTextBox.Text = string.Empty;
            PatronymicSelectedTextBox.Text = string.Empty;
            CashierPhoneTextBox.Text = string.Empty;
            USDBuyTextBox.Text = string.Empty;
            EUROBuyTextBox.Text = string.Empty;
            RUBBuyTextBox.Text = string.Empty;
            USDSellTextBox.Text = string.Empty;
            EUROSellTextBox.Text = string.Empty;
            RUBSellTextBox.Text = string.Empty;
            ServicesDataGrid.ItemsSource = null;
            WorkHourBeginTextBox.Text = string.Empty;
            WorkMinutesBeginTextBox.Text = string.Empty;
            WorkHourEndTextBox.Text = string.Empty;
            WorkMinutesEndTextBox.Text = string.Empty;
            BreakTimesDataGrid.ItemsSource = null;
            CommentsDataGrid.ItemsSource = null;
            LongitudeTextBox.Text = string.Empty;
            LatitudeTextBox.Text = string.Empty;
            YearTextBox.Text = string.Empty;
            MonthTextBox.Text = string.Empty;
            DayTextBox.Text = string.Empty;
        }

        public void ChangeMapCenter(double Latitude, double Longitude)
        {
            MainMap.Position = new GMap.NET.PointLatLng(Latitude, Longitude);
        }

        public void StartSearchRatesButtonClick(Object Sender, EventArgs CurrentArgs)
        {
            try
            {
                if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinBuyValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO,true,true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxBuyValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO, false, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxSellValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO, true, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.EURORadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinSellValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.EURO, false, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinBuyValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, true, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxBuyValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, false, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxSellValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, true, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.RUBRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinSellValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.RUB, false, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinBuyValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, true, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxBuyValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, false, true);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MaxSellValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, true, false);
                }
                else if (MoneyTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.USDRadioButton
                    && RatesSearchTypeRadioButtons.Where(res => res.IsChecked == true).First().
                    Name == MyResourses.Texts.MinSellValueRadioButton)
                {
                    SelectedBranchBuf = EntityConnector.GetNecessaryBankRates(MyResourses.Texts.USD, false, false);
                }
                ShowBranchInfo(SelectedBranchBuf);
                ChangeMapCenter((double)SelectedBranchBuf.MapLocation.Latitude, (double)SelectedBranchBuf.MapLocation.Longitude);
                SelectedTabItem.IsSelected = true;
                ChangeColorOfSelectedBranch(SelectedBranchBuf.BranchName);
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(MyResourses.Texts.CheckRadioButtonsError, MyResourses.Texts.Error, 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void StartSearchMapButtonClick(Object Sender, EventArgs CurrentArgs)
        {
            SelectedBranchBuf = EntityConnector.GetNearestBranch(MainMap.Position.Lat, MainMap.Position.Lng);
            ShowBranchInfo(SelectedBranchBuf);
            ChangeMapCenter((double)SelectedBranchBuf.MapLocation.Latitude, (double)SelectedBranchBuf.MapLocation.Longitude);
            SelectedTabItem.IsSelected = true;
            ChangeColorOfSelectedBranch(SelectedBranchBuf.BranchName);
        }

        public void AddNewBreakButtonClick(Object Sender, EventArgs CurrentArgs)
        {
            try
            {
                if (Byte.Parse(BreakTimeBeginHourTextBox.Text) > 24 || Byte.Parse(BreakTimeEndHourTextBox.Text) > 24 || 
                    Byte.Parse(BreakTimeBeginMinuteTextBox.Text) > 59 || Byte.Parse(BreakTimeEndMinuteTextBox.Text) > 59)
                    throw new InvalidOperationException();
                ItemsSourseBreakTimes.Add(new BreakTime()
                {
                    StartHour = Byte.Parse(BreakTimeBeginHourTextBox.Text),
                    StartMinutes = Byte.Parse(BreakTimeBeginMinuteTextBox.Text),
                    EndHour = Byte.Parse(BreakTimeEndHourTextBox.Text),
                    EndMinutes = Byte.Parse(BreakTimeEndMinuteTextBox.Text)
                });
                BreakTimesDataGrid.ItemsSource = null;
                BreakTimesDataGrid.ItemsSource = ItemsSourseBreakTimes;
            }
            catch
            {
                MessageBox.Show(MyResourses.Texts.CheckCharacharsError,MyResourses.Texts.Error,
                    MessageBoxButton.OK,MessageBoxImage.Error);
            }         
        }

        public void AddNewServiceButtonClick(Object Sender, EventArgs CurrentArgs)
        {
            try
            {
                if (ItemsSourseServices.Any(res => res.Servise == NewServiceTextBox.Text))
                {
                    throw new InvalidOperationException();
                }
                ItemsSourseServices.Add(new Service() { Servise = NewServiceTextBox.Text });
                ServicesDataGrid.ItemsSource = null;
                ServicesDataGrid.ItemsSource = ItemsSourseServices;
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(MyResourses.Texts.CheckCharacharsError, MyResourses.Texts.Error,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BreakTimesDataGridCheckClumns(Object Sender,DataGridAutoGeneratingColumnEventArgs arguments)
        {
            if (arguments.PropertyName == MyResourses.Texts.BreakTimeID || arguments.PropertyName == MyResourses.Texts.RelatedBranches)
            {
                arguments.Cancel = true;
            }
            else
            {
                arguments.Cancel = false;
            }
        }

        public void BreakTimeRemoveButtonClick(Object Sender, EventArgs arguments)
        {
            try
            {
                BreakTime SelectedBreakTime = BreakTimesDataGrid.SelectedItem as BreakTime;
                ItemsSourseBreakTimes.Remove(SelectedBreakTime);
                BreakTimesDataGrid.ItemsSource = null;
                BreakTimesDataGrid.ItemsSource = ItemsSourseBreakTimes;
            }
            catch
            {
                MessageBox.Show(MyResourses.Texts.NoSelectedItems, MyResourses.Texts.Error,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ServicesDataGridCheckColumns(Object Sender, DataGridAutoGeneratingColumnEventArgs arguments)
        {
            if (arguments.PropertyName == MyResourses.Texts.Servise)
            {
                arguments.Cancel = false;
            }
            else
            {
                arguments.Cancel = true;
            }
        }

        public void ServiceRemoveButtonClick(Object Sender, EventArgs arguments)
        {
            try
            {
                Service SelectedService = ServicesDataGrid.SelectedItem as Service;
                ItemsSourseServices.Remove(SelectedService);
                ServicesDataGrid.ItemsSource = null;
                ServicesDataGrid.ItemsSource = ItemsSourseServices;
            }
            catch
            {
                MessageBox.Show(MyResourses.Texts.NoSelectedItems, MyResourses.Texts.Error,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CommentsDataGridCheckColumns(Object Sender, DataGridAutoGeneratingColumnEventArgs arguments)
        {
            if (arguments.PropertyName == MyResourses.Texts.CommentItself)
            {
                arguments.Cancel = false;
            }
            else
            {
                arguments.Cancel = true;
            }
        }

        public void CommentRemoveButtonClick(Object Sender, EventArgs arguments)
        {
            try
            {
                Comment SelectedComment = CommentsDataGrid.SelectedItem as Comment;
                ItemsSourceComments.Remove(SelectedComment);
                CommentsDataGrid.ItemsSource = null;
                CommentsDataGrid.ItemsSource = ItemsSourceComments;
            }
            catch
            {
                MessageBox.Show(MyResourses.Texts.NoSelectedItems, MyResourses.Texts.Error,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddCommentButtonClick(Object Sender, EventArgs arguments)
        {
            ItemsSourceComments.Add(new Comment() { CommentItself = CommentTextBox.Text });
            CommentsDataGrid.ItemsSource = null;
            CommentsDataGrid.ItemsSource = ItemsSourceComments;
        }

        public void SaveChangesButtonClick(Object Sender, EventArgs arguments)
        {
            AddNewBranch = false;
            SaveChanges(SelectedBranchBuf);
        }

        public void AddNewBranchButtonClick(Object Sender, EventArgs arguments)
        {
            AddNewBranch = true;
            SaveChanges(new BankBranch());
        }

        public void SaveChanges(BankBranch BranchToSave)
        {
            try
            {
                string Longitude = string.Empty;
                string Latitude = string.Empty;
                if (UseMapCoordinatesCheckBox.IsChecked.Value)
                {
                    Longitude = MainMap.Position.Lng.ToString().Replace(",", ".");
                    Latitude = MainMap.Position.Lat.ToString().Replace(",", ".");
                    if (AddNewBranch)
                    {
                        BranchToSave.MapLocation = DbGeography.FromText(MyResourses.Texts.POINT + "(" + Longitude +
                            " " + Latitude + ")");
                    }
                    else
                    {
                        EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().
                            MapLocation 
                            = DbGeography.FromText(MyResourses.Texts.POINT + "(" + Longitude +
                            " " + Latitude + ")");
                        EntityConnector.StaticBanksDBContext.SaveChanges();
                    }
                }
                else
                {
                    Longitude = LongitudeTextBox.Text.Replace(",", ".");
                    Latitude = LatitudeTextBox.Text.Replace(",", ".");
                    if (AddNewBranch)
                    {
                        BranchToSave.MapLocation = DbGeography.FromText(MyResourses.Texts.POINT + "(" + Longitude +
                            " " + Latitude + ")");
                    }
                    else
                    {
                        EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().
                            MapLocation
                            = DbGeography.FromText(MyResourses.Texts.POINT + "(" + Longitude +
                            " " + Latitude + ")");
                        EntityConnector.StaticBanksDBContext.SaveChanges();
                    }
                }
                if (YearTextBox.Text != string.Empty && MonthTextBox.Text != string.Empty &&
                    DayTextBox.Text != string.Empty)
                {
                    if (Int32.Parse(MonthTextBox.Text) > 12 || Int32.Parse(DayTextBox.Text) > 31)
                    {
                        throw new InvalidOperationException();
                    }
                    else
                    {
                        if (AddNewBranch)
                        BranchToSave.OpeningDate = new DateTime(Int32.Parse(YearTextBox.Text), 
                            Int32.Parse(MonthTextBox.Text), Int32.Parse(DayTextBox.Text));
                        else
                        {
                            EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().
                                OpeningDate = new DateTime(Int32.Parse(YearTextBox.Text),
                            Int32.Parse(MonthTextBox.Text), Int32.Parse(DayTextBox.Text));
                            EntityConnector.StaticBanksDBContext.SaveChanges();
                        }
                    }
                }
                if (WorkHourBeginTextBox.Text != string.Empty && WorkHourEndTextBox.Text != string.Empty &&
                    WorkMinutesBeginTextBox.Text != string.Empty && WorkMinutesEndTextBox.Text != string.Empty)
                {
                    if (Byte.Parse(WorkHourBeginTextBox.Text) > 24 || Byte.Parse(WorkHourEndTextBox.Text) > 24 ||
                        Byte.Parse(WorkMinutesBeginTextBox.Text) > 59 || Byte.Parse(WorkMinutesEndTextBox.Text) > 59)
                        throw new InvalidOperationException();
                    else
                    {
                        if (AddNewBranch)
                        {
                            BranchToSave.WorkingHours = new WorkingHours()
                            {
                                EndHour = Byte.Parse(WorkHourEndTextBox.Text),
                                StartHour = Byte.Parse(WorkHourBeginTextBox.Text),
                                StartMinutes = Byte.Parse(WorkMinutesBeginTextBox.Text),
                                EndMinutes = Byte.Parse(WorkMinutesEndTextBox.Text)
                            };
                        }
                        else
                        {
                            EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().
                                WorkingHours = new WorkingHours()
                            {
                                EndHour = Byte.Parse(WorkHourEndTextBox.Text),
                                StartHour = Byte.Parse(WorkHourBeginTextBox.Text),
                                StartMinutes = Byte.Parse(WorkMinutesBeginTextBox.Text),
                                EndMinutes = Byte.Parse(WorkMinutesEndTextBox.Text)
                            };
                            EntityConnector.StaticBanksDBContext.SaveChanges();
                        }

                    }
                }
                  
                if (!AddNewBranch && EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BranchName == BranchNameTextBox.Text).
                    Where(res => res.BankBranchID != BranchToSave.BankBranchID).ToList().Count > 0)
                {
                    MessageBox.Show(MyResourses.Texts.BranchAlreadyExists, MyResourses.Texts.Error,
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new InvalidOperationException();
                }
                else if (AddNewBranch && EntityConnector.StaticBanksDBContext.BankBranches.Any(res => res.BranchName == BranchNameTextBox.Text))
                {
                    MessageBox.Show(MyResourses.Texts.BranchAlreadyExists, MyResourses.Texts.Error,
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new InvalidOperationException();
                }
                if (AddNewBranch)
                {
                    BranchToSave.BranchName = BranchNameTextBox.Text;
                    BranchToSave.Address = AddressTextBox.Text;
                    BranchToSave.Phone = PhoneTextBox.Text;
                }
                else
                {
                    EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().BranchName = BranchNameTextBox.Text;
                    EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().Address = AddressTextBox.Text;
                    EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().Phone = PhoneTextBox.Text;
                    EntityConnector.StaticBanksDBContext.SaveChanges();
                }      

                if (FirstNameSelectedTextBox.Text != string.Empty && LastNameSelectedTextBox.Text != string.Empty &&
                    PatronymicSelectedTextBox.Text != string.Empty && CashierPhoneTextBox.Text != string.Empty)
                {
                    if (AddNewBranch)
                    {
                        BranchToSave.RelatedCashier = new Cashier()
                        {
                            FirstName = FirstNameSelectedTextBox.Text,
                            LastName = LastNameSelectedTextBox.Text,
                            Patronymic = PatronymicSelectedTextBox.Text,
                            Phone = CashierPhoneTextBox.Text
                        };
                    }
                    else
                    {
                        EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().RelatedCashier 
                            = new Cashier()
                        {
                            FirstName = FirstNameSelectedTextBox.Text,
                            LastName = LastNameSelectedTextBox.Text,
                            Patronymic = PatronymicSelectedTextBox.Text,
                            Phone = CashierPhoneTextBox.Text
                        };
                        EntityConnector.StaticBanksDBContext.SaveChanges();
                    }
                }

                if (EUROBuyTextBox.Text != string.Empty && EUROSellTextBox.Text != string.Empty &&
                    RUBBuyTextBox.Text != string.Empty && RUBSellTextBox.Text != string.Empty &&
                    USDBuyTextBox.Text != string.Empty && USDSellTextBox.Text != string.Empty)
                {
                    if (AddNewBranch)
                    {
                        BranchToSave.RelatedRates = new ExchangeRates()
                        {
                            EUROBuy = double.Parse(EUROBuyTextBox.Text),
                            EUROSell = double.Parse(EUROSellTextBox.Text),
                            RuBuy = double.Parse(RUBBuyTextBox.Text),
                            RuSell = double.Parse(RUBSellTextBox.Text),
                            USDBuy = double.Parse(USDBuyTextBox.Text),
                            USDSell = double.Parse(USDSellTextBox.Text),
                        };
                    }
                    else
                    {
                        EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().
                            RelatedRates = new ExchangeRates()
                        {
                            EUROBuy = double.Parse(EUROBuyTextBox.Text),
                            EUROSell = double.Parse(EUROSellTextBox.Text),
                            RuBuy = double.Parse(RUBBuyTextBox.Text),
                            RuSell = double.Parse(RUBSellTextBox.Text),
                            USDBuy = double.Parse(USDBuyTextBox.Text),
                            USDSell = double.Parse(USDSellTextBox.Text),
                        };
                        EntityConnector.StaticBanksDBContext.SaveChanges();
                    }
                }

                if (AddNewBranch)
                {
                    foreach (var CurrentItem in BreakTimesDataGrid.Items)
                    {
                        if ((CurrentItem as BreakTime) != null)
                            BranchToSave.BreakTimes.Add(CurrentItem as BreakTime);
                    }
                    foreach (var CurrentItem in ServicesDataGrid.Items)
                    {
                        if ((CurrentItem as Service) != null)
                        {
                            string ServiceName = (CurrentItem as Service).Servise;
                            if (EntityConnector.StaticBanksDBContext.Services.Any(res => res.Servise == ServiceName))
                                BranchToSave.RelatedServices.Add(EntityConnector.StaticBanksDBContext.Services.Where(res => res.Servise == ServiceName).First());
                            else
                                BranchToSave.RelatedServices.Add(CurrentItem as Service);
                        }
                    }
                    foreach (var CurrentItem in CommentsDataGrid.Items)
                    {
                        if ((CurrentItem as Comment) != null)
                            BranchToSave.RelatedComments.Add(new Comment() { CommentItself = (CurrentItem as Comment).CommentItself });
                    }
                }
                else
                {
                    List<BreakTime> NewBreakTimes = new List<BreakTime>();
                    List<Service> NewServices = new List<Service>();
                    List<Comment> NewComments = new List<Comment>();
                    foreach (var CurrentItem in BreakTimesDataGrid.Items)
                    {
                        if ((CurrentItem as BreakTime) != null)
                        {
                            NewBreakTimes.Add(CurrentItem as BreakTime);
                        }
                    }
                    EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().BreakTimes
                         = NewBreakTimes;
                    EntityConnector.StaticBanksDBContext.SaveChanges();
                    foreach (var CurrentItem in ServicesDataGrid.Items)
                    {
                        if ((CurrentItem as Service) != null)
                        {
                            string ServiceName = (CurrentItem as Service).Servise;
                            if (EntityConnector.StaticBanksDBContext.Services.Any(res => res.Servise == ServiceName))
                                NewServices.Add(EntityConnector.StaticBanksDBContext.Services.Where(res => res.Servise == ServiceName).First());
                            else
                                NewServices.Add(CurrentItem as Service);
                        }
                    }
                    EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().
                        RelatedServices = NewServices;
                    EntityConnector.StaticBanksDBContext.SaveChanges();
                    foreach (var CurrentItem in CommentsDataGrid.Items)
                    {
                        if ((CurrentItem as Comment) != null)
                            NewComments.Add(CurrentItem as Comment);
                    }
                    foreach (Comment CurComment in NewComments)
                    {
                        CurComment.RelatedBranch = EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First();
                    }
                        EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().
                        RelatedComments = NewComments;
                        EntityConnector.StaticBanksDBContext.SaveChanges();
                }


                EntityConnector.AddNewBankIfNecessary(RelatedBankNameTextBox.Text);
                BranchToSave.RelatedBank = null;
                if (AddNewBranch)
                {
                    EntityConnector.StaticBanksDBContext.Banks.Where(res => res.BankName == RelatedBankNameTextBox.Text).
                        First().RelatedBranches.Add(BranchToSave);
                }
                else
                {
                    EntityConnector.StaticBanksDBContext.BankBranches.Where(res => res.BankBranchID == BranchToSave.BankBranchID).First().RelatedBank = EntityConnector.StaticBanksDBContext.Banks.
                        Where(res => res.BankName == RelatedBankNameTextBox.Text).First();
                }
                EntityConnector.StaticBanksDBContext.SaveChanges();
                LoadObjectsOnMap();
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(MyResourses.Texts.CheckCharacharsError, MyResourses.Texts.Error,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UseFiltersButtonClick(Object Sender, EventArgs arguments)
        {
            UseFilters = true;
            LoadObjectsOnMap();
        }
    }
}
