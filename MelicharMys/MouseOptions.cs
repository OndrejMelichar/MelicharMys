using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MelicharMys
{
    public class MouseOptions
    {
        [DllImport("user32.dll")]
        public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);
        [DllImport("kernel32.dll")]
        public static extern int GetLastError();
        private const int SPI_GETMOUSESPEED = 112;
        private const int SPI_SETMOUSESPEED = 113;
        public static int CurrentSpeed;
        
        public static void SetDefaults()
        {
            InternalSetMouseSpeed(5); // bylo zde 10
        }

        public static int InternalGetMouseSpeed()
        {
            int intSpeed = 0;
            IntPtr ptr;
            ptr = Marshal.AllocCoTaskMem(4);
            SystemParametersInfo(SPI_GETMOUSESPEED, 0, ptr, 0);
            intSpeed = Marshal.ReadInt32(ptr);
            Marshal.FreeCoTaskMem(ptr);

            return intSpeed;
        }

        public static void InternalSetMouseSpeed(int intSpeed)
        {
            IntPtr ptr = new IntPtr(intSpeed);

            int b = SystemParametersInfo(SPI_SETMOUSESPEED, 0, ptr, 0);

            if (b == 0)
            {
                Console.WriteLine("Not able to set speed");
            }
            else if (b == 1)
            {
                Console.WriteLine("Successfully done");
            }

        }
    }
}
