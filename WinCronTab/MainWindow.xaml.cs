using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinCronTab
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation da = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.4)));
            WinCronTab.BeginAnimation(UIElement.OpacityProperty, da);
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
        }
        private void BtnMin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Timers.Timer time = new System.Timers.Timer(400);
            time.Elapsed += new System.Timers.ElapsedEventHandler(MainClose);
            time.AutoReset = false;
            time.Enabled = true;
            DoubleAnimation da = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.4)));
            WinCronTab.BeginAnimation(UIElement.OpacityProperty, da);
        }
        private void MainClose(object source, System.Timers.ElapsedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
