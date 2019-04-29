using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace MelicharMys
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        //https://www.thomasclaudiushuber.com/2015/08/22/creating-a-background-application-with-wpf/

        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isExit;

        private MainWindow mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.mainWindow = new MainWindow();
            this.setWindowLocation();
            this.mainWindow.Show();
            this.mainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Click += (s, e2) => ShowMainWindow();
            _notifyIcon.Icon = MelicharMys.Properties.Resources.MyIcon;
            _notifyIcon.Visible = true;

            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            //_notifyIcon.ContextMenuStrip.Items.Add("MainWindow...").Click += (s, e) => ShowMainWindow();
            _notifyIcon.ContextMenuStrip.Items.Add("Ukončit").Click += (s, e3) => ExitApplication();
        }

        private void setWindowLocation()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            System.Drawing.Point mousePoint = MouseOptions.MousePosition.GetMousePosition();
            this.mainWindow.Left = mousePoint.X - this.mainWindow.Width;
            this.mainWindow.Top = mousePoint.Y - this.mainWindow.Height;

            if (this.mainWindow.Top < 0)
            {
                this.mainWindow.Top = 0;
            }

            if (this.mainWindow.Left < 0)
            {
                this.mainWindow.Left = 0;
            }
        }

        private void ExitApplication()
        {
            _isExit = true;
            this.mainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        private void ShowMainWindow()
        {
            if (this.mainWindow.IsVisible)
            {
                if (this.mainWindow.WindowState == WindowState.Minimized)
                {
                    this.mainWindow.WindowState = WindowState.Normal;
                }

                this.setWindowLocation();
                this.mainWindow.Activate();
            }
            else
            {
                this.setWindowLocation();
                this.mainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                this.mainWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }
    }
}
