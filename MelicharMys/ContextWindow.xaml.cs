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
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace MelicharMys
{
    /// <summary>
    /// Interakční logika pro ContextWindow.xaml
    /// </summary>
    public partial class ContextWindow : Window
    {
        public ContextWindow()
        {
            InitializeComponent();
            mouseSpeedValueTextBox.Text = MouseOptions.MouseSpeed.GetMouseSpeed().ToString();
            scrollSpeedValueTextBox.Text = MouseOptions.ScrollSpeed.GetScrollSpeed().ToString();
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
                    if (newSpeed < 0) //TODO: může tam být "0"? nenastavit minimum na "1"?
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
        private void doubleClickTimeValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (scrollSpeedValueTextBox != null)
            {
                int newSpeed;
                bool parse = int.TryParse(scrollSpeedValueTextBox.Text, out newSpeed);

                if (parse)
                {
                    if (newSpeed < 0) //TODO: může tam být "0"? nenastavit minimum na "1"?
                    {
                        newSpeed = 0;
                        scrollSpeedValueTextBox.Text = newSpeed.ToString();
                    }

                    MouseOptions.ScrollSpeed.SetScrollSpeed(newSpeed);
                }
            }

        }

        private void resetDoubleClickTime_Click(object sender, RoutedEventArgs e)
        {
            MouseOptions.DoubleClickTime.SetDefaultDoubleClickTime();
            scrollSpeedValueTextBox.Text = MouseOptions.DoubleClickTime.GetDoubleClickTime().ToString();
        }
        
    }
}
