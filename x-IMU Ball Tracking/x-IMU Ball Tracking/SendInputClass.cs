using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace x_IMU_Ball_Tracking
{
    /// <summary>
    /// C# wrapper for SendInput of user32.dll.
    /// </summary>
    /// <remarks>
    /// See: http://damiproductions.darkbb.com/t331-c-sendinput-autoclicker
    /// </remarks>
    public class SendInputClass
    {
        //C# signature for "SendInput()"
        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        static public extern uint SendInput(
           uint nInputs,
           INPUT[] pInputs,
           int cbSize);

        //C# signature for "GetMessageExtraInfo()"
        [DllImport("user32.dll", EntryPoint = "GetMessageExtraInfo", SetLastError = true)]
        static public extern IntPtr GetMessageExtraInfo();

        public enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2,
        }

        [Flags()]
        public enum MOUSEEVENTF
        {
            MOVE = 0x0001,  // mouse move
            LEFTDOWN = 0x0002,  // left button down
            LEFTUP = 0x0004,  // left button up
            RIGHTDOWN = 0x0008,  // right button down
            RIGHTUP = 0x0010,  // right button up
            MIDDLEDOWN = 0x0020,  // middle button down
            MIDDLEUP = 0x0040,  // middle button up
            XDOWN = 0x0080,  // x button down
            XUP = 0x0100,  // x button down
            WHEEL = 0x0800,  // wheel button rolled
            VIRTUALDESK = 0x4000,  // map to entire virtual desktop
            ABSOLUTE = 0x8000,  // absolute move
        }

        [Flags()]
        public enum KEYEVENTF
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            UNICODE = 0x0004,
            SCANCODE = 0x0008,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int dwData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        /// <summary>
        /// Synthesizes mouse motion and button clicks. Function of same name was superseded by SendInput.
        /// Use as described by: http://msdn.microsoft.com/en-us/library/ms646260%28v=vs.85%29.aspx
        /// </summary>
        public static void MouseEvent(int dwFlags, int dx, int dy, int dwData)
        {
            SendInputClass.INPUT input = new SendInputClass.INPUT();
            input.mi.dwFlags = dwFlags; 
            input.mi.dx = dx;
            input.mi.dy = dy;
            input.mi.dwData = dwData;
            SendInputClass.INPUT[] inputArr = { input };
            SendInputClass.SendInput(1, inputArr, Marshal.SizeOf(input));
        } 
    }
}