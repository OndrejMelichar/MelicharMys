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

            MouseOptions.InternalSetMouseSpeed(30);
            mouseSpeedValueTextBox.Text = MouseOptions.CurrentSpeed.ToString();
        }

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            IntPtr pvParam,
            UInt32 fWinIni);
        
        

    }
}
