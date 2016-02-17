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
using BanksOnMap.DBContext;
using BanksOnMap.Entities;
using System.Data.Entity.Spatial;

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
                BanksDBContext TestContext = new BanksDBContext(MyResourses.Texts.ConnectionStringName);
                /*TestContext.Banks.Add(new Bank() { BankName = "UltraBank" });
                TestContext.Banks.Add(new Bank() { BankName = "NativeBank" });
                TestContext.Banks.Add(new Bank() { BankName = "TestBank" });*/
                /*Bank SingleBank = TestContext.Banks.Where(res => res.BankName == "NativeBank").First();
                SingleBank.BankName = "CommunistBank";*/
                //TestContext
                /*TestContext.BankBranches.
                    Add(new BankBranch() {  BranchName = "Office 120 NativeBank",
                                            Address = "Moon street",
                                            MapLocation = DbGeography.FromText("POINT(50.861328 34.089061)"),
                                            Phone = "3447689",
                                            RelatedBank = 
                });*/
                TestContext.SaveChanges();
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message);
            }
            //конфигурируем карту
            //*****************************************************************************
            //так добавляем маркеры (видимость маркера определяется видимостью его формы "shape")
            MainMap.Markers.Add(new GMapMarker(new GMap.NET.PointLatLng(53.902542, 27.561781)) { Tag = "tagddd", Shape = new Label()
                { FontSize = 12.0, Content = "Я маркер!!!", Foreground = Brushes.Yellow, Background = Brushes.Black } });

            MainMap.Zoom = 16;
            MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            MainMap.MapProvider = GMap.NET.MapProviders.GMapProviders.OpenStreetMap;
            MainMap.CanDragMap = true;
            MainMap.DragButton = MouseButton.Left;
            MainMap.Position = new GMap.NET.PointLatLng(53.902800, 27.561759);
            //*****************************************************************************
            //Текст отображаемый при наведении на маркер.

        }
    }
}
