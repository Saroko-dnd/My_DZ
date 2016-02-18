﻿using System;
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
using BanksOnMap.DBContext;
using BanksOnMap.Entities;
using System.Data.Entity.Spatial;
using System.Collections.ObjectModel;

namespace BanksOnMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            //конфигурируем карту
            //*****************************************************************************
            //так добавляем маркеры (видимость маркера определяется видимостью его формы "shape")
            BanksDBContext BanksDatabase = new BanksDBContext(MyResourses.Texts.ConnectionStringName);
            foreach ( BankBranch CurrentBranch in BanksDatabase.BankBranches)
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
            BanksDBContext BanksDatabase = new BanksDBContext(MyResourses.Texts.ConnectionStringName);
            //если поместить (Sender as Label).Content.ToString() в запрос то возращает исключение
            string BranchName = (Sender as Label).Content.ToString();
            BankBranch SelectedBranch = BanksDatabase.BankBranches.Where(res => res.BranchName == BranchName).
                First();
            RelatedBankNameTextBox.Text = SelectedBranch.RelatedBank.BankName;
            BranchNameTextBox.Text = SelectedBranch.BranchName;
        }

        public void ShowCoordinatesEvent (Object Sender,EventArgs CurrentArgs)
        {
            MapPointTestLabel.Text = MainMap.Position.ToString();
        }
    }
}
