using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MelicharMys
{
    public class MouseOptions
    {
        public class MousePosition
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetCursorPos(ref Win32Point pt);

            [StructLayout(LayoutKind.Sequential)]
            internal struct Win32Point
            {
                public Int32 X;
                public Int32 Y;
            };
            public static Point GetMousePosition()
            {
                Win32Point w32Mouse = new Win32Point();
                GetCursorPos(ref w32Mouse);
                return new Point(w32Mouse.X, w32Mouse.Y);
            }
        }

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
            private const int SPI_GETSCROLLSPEED = 104;
            private const int SPI_SETSCROLLSPEED = 105;

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
                SystemParametersInfo(SPI_SETSCROLLSPEED, intSpeed, ptr, 0);
            }
        }

        public class DoubleClickTime
        {
            [DllImport("user32.dll")]
            public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);
            private const int SPI_GETDOUBLECLICKTIME = 31;
            private const int SPI_SETDOUBLECLICKTIME = 32;

            public static void SetDefaultDoubleClickTime()
            {
                SetDoubleClickTime(500);
            }

            public static int GetDoubleClickTime()
            {
                return System.Windows.Forms.SystemInformation.DoubleClickTime;
            }

            public static void SetDoubleClickTime(int intSpeed)
            {
                IntPtr ptr = new IntPtr(intSpeed);
                SystemParametersInfo(SPI_SETDOUBLECLICKTIME, intSpeed, ptr, 0);
            }
        }

    }
}
