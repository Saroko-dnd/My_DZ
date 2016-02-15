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
            //конфигурируем карту
            //*****************************************************************************
            MainMap.Zoom = 16;
            MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            MainMap.MapProvider = GMap.NET.MapProviders.GMapProviders.OpenStreetMap;
            MainMap.CanDragMap = true;
            MainMap.DragButton = MouseButton.Left;
            MainMap.Position = new GMap.NET.PointLatLng(53.902800, 27.561759);
            //*****************************************************************************
            GMap.NET.WindowsPresentation.GMapMarker markerG = 
                new GMapMarker(new GMap.NET.PointLatLng(53.902542, 27.561781));
            markerG.Tag = "";
            //Текст отображаемый при наведении на маркер.

        }
    }
}
