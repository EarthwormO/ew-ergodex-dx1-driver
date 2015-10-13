using System;
using System.Runtime.InteropServices;

namespace DX1Utility
{
    public static class Globals
    {
        public static string ProfileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DX1Profiles\\";
        public static string macroDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DX1Profiles\\Macros\\";
        public static bool debugLog = false;
        public static string debugLogFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DX1Profiles\\Dx1Debug.txt";
    }

    public static class Bags
    {
        const int INPUT_MOUSE = 0;
        const int INPUT_KEYBOARD = 1;
        const int INPUT_HARDWARE = 2;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_VWHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x1000;
        const ushort VOLUME_MUTE = 0xAD;
        const ushort VOLUME_DOWN = 0xAE;
        const ushort VOLUME_UP = 0xAF;
        const ushort MEDIA_NEXT = 0xB0;
        const ushort MEDIA_PREV = 0xB1;
        const ushort MEDIA_STOP = 0xB2;
        const ushort MEDIA_PLAY_PAUSE = 0xB3;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_SCANCODE = 0x0008;

        //Used to store currently "pressed" Toggle Keys, to ensure they are all released before changing Profiles, or exiting
        static private System.Collections.Concurrent.ConcurrentDictionary<int, int> _toggleKeys = new System.Collections.Concurrent.ConcurrentDictionary<int, int>();

        static public bool CheckForToggle()
        {
            //Return True if any Toggle keys are pressed, otherwise return false
            if (_toggleKeys.Count > 0)
                return true;
            else
                return false;
        }

        static public void AddToggle (int keycode, int value)
        {
            _toggleKeys.TryAdd(keycode, value);
        }

        static public int getValue (int keycode)
        {
            int value;
            if (_toggleKeys.TryGetValue(keycode, out value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }

        static public void DecValue (int keycode)
        {
            _toggleKeys[keycode]--;
        }

        static public void RemoveToggle (int keycode)
        {
            int value;
            _toggleKeys.TryRemove(keycode, out value);
        }


        static public void UpAllKeys()
        {
            //Parse the Dictionary for any keys currently toggled on, and up the keys to ensure keys don't get stuck in a pressed state eternally
            foreach (var toggleKey in _toggleKeys)
            {
                DebugLog.Instance.writeLog("          Upping Key: " + toggleKey.Key);
                INPUT input_down = new INPUT();
                input_down.type = INPUT_KEYBOARD;
                input_down.ki.wVk = (ushort)toggleKey.Key;
                input_down.ki.wScan = 0;
                input_down.ki.dwExtraInfo = IntPtr.Zero;
                input_down.ki.dwFlags = KEYEVENTF_KEYUP;

                INPUT[] input = { input_down };

                SendInput(1, input, Marshal.SizeOf(input_down));

                int value;
                _toggleKeys.TryRemove(toggleKey.Key, out value);

            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(8)] //*
            public MOUSEINPUT mi;
            [FieldOffset(8)] //*
            public KEYBDINPUT ki;
            [FieldOffset(8)] //*
            HARDWAREINPUT hi;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs,
        INPUT[] pInputs,
        int cbSize);

    }
}
