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

namespace MelicharMys
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.setWindowLocation();
            mouseSpeedValueTextBox.Text = MouseOptions.MouseSpeed.GetMouseSpeed().ToString();
            scrollSpeedValueTextBox.Text = MouseOptions.ScrollSpeed.GetScrollSpeed().ToString();
            doubleClickTimeValueTextBox.Text = MouseOptions.DoubleClickTime.GetDoubleClickTime().ToString();
        }

        private void setWindowLocation()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            System.Drawing.Point mousePoint = MouseOptions.MousePosition.GetMousePosition();
            this.Left = mousePoint.X - this.Width;
            this.Top = mousePoint.Y - this.Height;

            if (this.Top < 0)
            {
                this.Top = 0;
            }

            if (this.Left < 0)
            {
                this.Left = 0;
            }
        }


        /* mouseSpeed */
        private void mouseSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mouseSpeedSlider != null && mouseSpeedValueTextBox != null)
            {
                int newSpeed = (int)mouseSpeedSlider.Value;
                mouseSpeedValueTextBox.Text = newSpeed.ToString();
                MouseOptions.MouseSpeed.SetMouseSpeed(newSpeed);
            }

        }

        private void mouseSpeedValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mouseSpeedSlider != null && mouseSpeedValueTextBox != null)
            {
                int newSpeed;
                bool parse = int.TryParse(mouseSpeedValueTextBox.Text, out newSpeed);

                if (parse)
                {
                    if (newSpeed < 0)
                    {
                        newSpeed = 0;
                        mouseSpeedValueTextBox.Text = newSpeed.ToString();
                    }

                    if (newSpeed > 20)
                    {
                        newSpeed = 20;
                        mouseSpeedValueTextBox.Text = newSpeed.ToString();
                    }

                    mouseSpeedSlider.Value = newSpeed;
                    MouseOptions.MouseSpeed.SetMouseSpeed(newSpeed);
                }
            }

        }

        private void resetMouseSpeed_Click(object sender, RoutedEventArgs e)
        {
            MouseOptions.MouseSpeed.SetDefaultMouseSpeed();
            mouseSpeedValueTextBox.Text = MouseOptions.MouseSpeed.GetMouseSpeed().ToString();
        }

        /* scrollSpeed */
        private void scrollSpeedValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (scrollSpeedValueTextBox != null)
            {
                int newSpeed;
                bool parse = int.TryParse(scrollSpeedValueTextBox.Text, out newSpeed);

                if (parse)
                {
                    if (newSpeed < 0)
                    {
                        newSpeed = 0;
                        scrollSpeedValueTextBox.Text = newSpeed.ToString();
                    }

                    MouseOptions.ScrollSpeed.SetScrollSpeed(newSpeed);
                }
            }

        }

        private void resetScrollSpeed_Click(object sender, RoutedEventArgs e)
        {
            MouseOptions.ScrollSpeed.SetDefaultScrollSpeed();
            scrollSpeedValueTextBox.Text = MouseOptions.ScrollSpeed.GetScrollSpeed().ToString();
        }

        /* doubleClickTime */
        private void doubleClickTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (doubleClickTimeSlider != null && doubleClickTimeValueTextBox != null)
            {
                int newTime = (int)doubleClickTimeSlider.Value;
                doubleClickTimeValueTextBox.Text = newTime.ToString();
                MouseOptions.DoubleClickTime.SetDoubleClickTime(newTime);
            }

        }

        private void doubleClickTimeValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (doubleClickTimeSlider != null && doubleClickTimeValueTextBox != null)
            {
                int newTime;
                bool parse = int.TryParse(doubleClickTimeValueTextBox.Text, out newTime);

                if (parse)
                {
                    if (newTime < 0)
                    {
                        newTime = 0;
                        doubleClickTimeValueTextBox.Text = newTime.ToString();
                    }

                    if (newTime > 5000)
                    {
                        newTime = 5000;
                        doubleClickTimeValueTextBox.Text = newTime.ToString();
                    }

                    doubleClickTimeSlider.Value = newTime;
                    MouseOptions.DoubleClickTime.SetDoubleClickTime(newTime);
                }
            }

        }

        private void resetDoubleClickTime_Click(object sender, RoutedEventArgs e)
        {
            MouseOptions.DoubleClickTime.SetDefaultDoubleClickTime();
            doubleClickTimeValueTextBox.Text = MouseOptions.DoubleClickTime.GetDoubleClickTime().ToString();
        }

    }
}
