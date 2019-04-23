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
        public class MouseSpeed
        {
            [DllImport("user32.dll")]
            public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);
            private const int SPI_GETMOUSESPEED = 112;
            private const int SPI_SETMOUSESPEED = 113;

            public static void SetDefaultMouseSpeed()
            {
                SetMouseSpeed(10);
            }

            public static int GetMouseSpeed()
            {
                int intSpeed = 0;
                IntPtr ptr;
                ptr = Marshal.AllocCoTaskMem(4);
                SystemParametersInfo(SPI_GETMOUSESPEED, 0, ptr, 0);
                intSpeed = Marshal.ReadInt32(ptr);
                Marshal.FreeCoTaskMem(ptr);
                return intSpeed;
            }

            public static void SetMouseSpeed(int intSpeed)
            {
                IntPtr ptr = new IntPtr(intSpeed);
                SystemParametersInfo(SPI_SETMOUSESPEED, 0, ptr, 0);
            }
        }

        public class ScrollSpeed
        {
            [DllImport("user32.dll")]
            public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);
            private const int SPI_GETSCROLLSPEED = 110;//68 
            private const int SPI_SETSCROLLSPEED = 111;//69
            //TODO: SPI_GETSCROLLSPEED a SPI_SETSCROLLSPEED má špatné hodnoty

            public static void SetDefaultScrollSpeed()
            {
                SetScrollSpeed(3);
            }

            public static int GetScrollSpeed()
            {
                int intSpeed = 0;
                IntPtr ptr;
                ptr = Marshal.AllocCoTaskMem(4);
                SystemParametersInfo(SPI_GETSCROLLSPEED, 0, ptr, 0);
                intSpeed = Marshal.ReadInt32(ptr);
                Marshal.FreeCoTaskMem(ptr);
                return intSpeed;
            }

            public static void SetScrollSpeed(int intSpeed)
            {
                IntPtr ptr = new IntPtr(intSpeed);
                SystemParametersInfo(SPI_SETSCROLLSPEED, 0, ptr, 0);
            }
        }
        
    }
}
