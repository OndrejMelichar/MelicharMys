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
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
            _notifyIcon.Icon = MelicharMys.Properties.Resources.MyIcon;
            _notifyIcon.Visible = true;

            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            //_notifyIcon.ContextMenuStrip.Items.Add("MainWindow...").Click += (s, e) => ShowMainWindow();
            _notifyIcon.ContextMenuStrip.Items.Add("Ukončit").Click += (s, e2) => ExitApplication();

            _notifyIcon.Click += _notifyIcon_Click;
        }

        private void _notifyIcon_Click(object sender, EventArgs e)
        {
            if (this.mainWindow == null)
            {
                this.mainWindow = new MainWindow();
                this.mainWindow.Closed += (s, e2) => this.contextWindowClose();
                this.mainWindow.Show();
            }
            
            //TODO: při otevření okna podruhé by se mělo zavřít předchozí (anebo se zamezit otevření dalšího)
        }

        private void contextWindowClose()
        {
            this.mainWindow = null;
        }

        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        private void ShowMainWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }
    }
}
