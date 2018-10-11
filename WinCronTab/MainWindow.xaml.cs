using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WinCronTab
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon = null;
        private DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation da = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.4)));
            WinCronTab.BeginAnimation(UIElement.OpacityProperty, da);
            InitialTray(); //最小化至托盘
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (System.DateTime.Now.ToString("t") == "12:20")
            {
                //指定时间执行
            }
        }
        private void InitialTray()
        {
            //隐藏主窗体
            this.Visibility = Visibility.Hidden;
            //设置托盘的各个属性
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "服务运行中...";
            notifyIcon.Text = "WinCronTab";
            notifyIcon.Visible = true;
            notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            //窗体状态改变时触发
            this.StateChanged += MainWindow_StateChanged;
        }
        private void Timedia()
        {
            //创建一个进程
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();//启动程序

            string strCMD = @"D:\UPUPW_AP5.6\PHP5\php.exe D:\UPUPW_AP5.6\htdocs\update.php";
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(strCMD + "&exit");

            p.StandardInput.AutoFlush = true;

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            //s.contains="s的一部分"
            if (output.Contains("success"))
            {
                System.Windows.MessageBox.Show("成功了", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
        /// <summary>
        /// 窗口状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// 托盘图标鼠标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
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
            //因为托盘问题，所以需要这么来操作。
            this.WindowState = System.Windows.WindowState.Normal;
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
